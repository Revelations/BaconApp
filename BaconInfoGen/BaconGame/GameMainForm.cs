using System.Windows.Forms;
using BaconBuilder.Model;

namespace BaconGame
{
	/// <summary>
	/// Main form for the creation of trivia questions.
	/// </summary>
	public partial class GameMainForm : Form, IGameMainForm
	{
		/// <summary>
		/// Presenter to control this form's layout.
		/// </summary>
	    private GamePresenter _presenter;

		/// <summary>
		/// Constructor for the form.
		/// </summary>
		public GameMainForm()
		{
			InitializeComponent();

			// Instantiate a presenter.
			_presenter = new GamePresenter(this);

			// Temporarily disable textfields.
			EnableTextFields();
		}

		/// <summary>
		/// Called when the form is first shown. Initialises a sync dialog.
		/// </summary>
		private void GameMainForm_Shown(object sender, System.EventArgs e)
		{
			_presenter.DownloadSync();
			_presenter.CreateNeededQuestionFiles();

			_presenter.PopulateFileView();
		}

		/// <summary>
		/// Occurs when the form is closing. Asks for sync confirmation and syncs with the server if it gets it.
		/// </summary>
		private void GameMainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			_presenter.SaveOpen();

			_presenter.UploadSync(e);
		}

        #region Interface Members

        public ListView FileView
        {
            get { return fileView; }
        }

        public ListView QuestionView
        {
            get { return questionView; }
        }

	    public ComboBox CorrectAnswer
        {
            get { return comboBoxCorrectAnswer; }
        }

        public string QuestionText
        {
            get { return textBoxQuestion.Text; }
            set { textBoxQuestion.Text = value; }
        }

        public string Answer1Text
        {
            get { return textBoxAnswer1.Text; }
            set { textBoxAnswer1.Text = value; }
        }

        public string Answer2Text
        {
            get { return textBoxAnswer2.Text; }
            set { textBoxAnswer2.Text = value; }
        }

        public string Answer3Text
        {
            get { return textBoxAnswer3.Text; }
            set { textBoxAnswer3.Text = value; }
        }

        public string Answer4Text
        {
            get { return textBoxAnswer4.Text; }
            set { textBoxAnswer4.Text = value; }
        }

        #endregion

		#region Toolbar Button Events

		/// <summary>
		/// Called when the user clicks the add question button. Adds a new question to the currently selected file.
		/// </summary>
		private void toolStripAdd_Click(object sender, System.EventArgs e)
		{
			// Can't add a question if no file selected to put it in.
			if (fileView.SelectedItems.Count == 0)
				_presenter.ShowError("No question file selected. Cannot create new question.");
			else
				_presenter.AddQuestion();
		}

		/// <summary>
		/// Called when the user clicks the delete question button. Deletes a question from the UI and current question file.
		/// </summary>
		private void toolStripDelete_Click(object sender, System.EventArgs e)
		{
			// Can't delete a question if it isn't selected.
			if (questionView.SelectedItems.Count == 0)
				_presenter.ShowError("No question selected to delete!");
			else
			{
				// Confirm before deletion.
				if (_presenter.ConfirmDelete())
					_presenter.RemoveQuestion();
			}
		}

		/// <summary>
		/// Called when the user clicks the server sync button. Uploads necessary files to the remote server.
		/// </summary>
		private void toolStripSync_Click(object sender, System.EventArgs e)
		{
			_presenter.SaveOpen();
			_presenter.UploadSync();
		}

		#endregion

		#region Menustrip Button Events

		/// <summary>
		/// Called when user clicks the exit button in the file menu. Closes the form.
		/// </summary>
		private void toolStripExit_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		#endregion

		#region Listview Events

		/// <summary>
		/// Occurs for each change of selection when a user selects a new file.
		/// 
		/// Saves the deselected question file. Populates the question view with questions from the newly selected question file.
		/// Updates UI as appropriate.
		/// </summary>
		private void fileView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			if (e.IsSelected)
			{
				_presenter.PopulateQuestionView(fileView.SelectedItems[0].Text);

				e.Item.ImageIndex = 1;
			}
			else
			{
				if (questionView.SelectedItems.Count > 0)
					_presenter.SaveQuestion(questionView.SelectedIndices[0]);
				_presenter.SaveQuestionFile(e.Item.Text);
				_presenter.DepopulateQuestionView();
				_presenter.ClearTextFields();

				e.Item.ImageIndex = 0;
			}

			EnableTextFields();
		}

		/// <summary>
		/// Occurs for each change of selection when a user selects a new question.
		/// 
		/// Disables the edit controls for a deselect, re-enables them for a select.
		/// Updates UI as necessary.
		/// </summary>
		private void questionView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			if (e.IsSelected)
			{
				_presenter.LoadTextFields();

				e.Item.ImageIndex = 1;
			}
			else
			{
				_presenter.SaveQuestion(e.ItemIndex);
				_presenter.ClearTextFields();

				e.Item.ImageIndex = 0;
			}

			EnableTextFields();
		}

		#endregion

		#region Textbox Events

		/// <summary>
		/// Occurs when the user types in any textbox. Suppresses parsing enter key.
		/// 
		/// Also saves question on pressing enter.
		/// </summary>
		private void textBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				e.SuppressKeyPress = true;
				if (questionView.SelectedItems.Count > 0)
				{
					_presenter.SaveQuestion(questionView.SelectedIndices[0]);
				}
			}
		}

		/// <summary>
		/// Occurs when any of the textboxes lose focus. Saves the current question.
		/// </summary>
		private void textBoxQuestion_Leave(object sender, System.EventArgs e)
		{
			if (questionView.SelectedItems.Count > 0)
				_presenter.SaveQuestion(questionView.SelectedIndices[0]);
		}

		#endregion

		/// <summary>
		/// Occurs when the main splitter gets moved. Adjusts file view column size.
		/// </summary>
		private void splitter_SplitterMoved(object sender, SplitterEventArgs e)
		{
			fileHeader.Width = fileView.Width;
		}

		/// <summary>
		/// Enables or disables the edit controls based on whether or not a file is selected.
		/// </summary>
		private void EnableTextFields()
		{
			bool b = questionView.SelectedItems.Count > 0;
			textBoxQuestion.Enabled = b;
			textBoxAnswer1.Enabled = b;
			textBoxAnswer2.Enabled = b;
			textBoxAnswer3.Enabled = b;
			textBoxAnswer4.Enabled = b;

			if(!b) comboBoxCorrectAnswer.Items.Clear();

			comboBoxCorrectAnswer.Enabled = b;
		}

		/// <summary>
		/// Occurs when the combo box's index gets changes. Updates the answer in the question view.
		/// </summary>
		private void comboBoxCorrectAnswer_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (questionView.SelectedItems.Count > 0)
				_presenter.UpdateAnswer();
		}
	}
}