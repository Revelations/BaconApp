using System.Drawing;
using System.Windows.Forms;

namespace BaconGame
{
	public partial class GameMainForm : Form, IGameMainForm
	{
	    private GamePresenter _presenter;

		public GameMainForm()
		{
			InitializeComponent();

		    _presenter = new GamePresenter(this);

		    _presenter.PopulateFileView();

			EnableTextFields();
		}

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

        private void fileView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
                _presenter.PopulateQuestionView(fileView.SelectedItems[0].Text);
            else
            {
				if(questionView.SelectedItems.Count > 0)
					_presenter.SaveQuestion(questionView.SelectedIndices[0]);
                _presenter.SaveQuestionFile(e.Item.Text);
                _presenter.DepopulateQuestionView();
				_presenter.ClearTextFields();
            }

			EnableTextFields();
        }

        private void questionView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
                _presenter.LoadTextFields();
            else
            {
                _presenter.SaveQuestion(e.ItemIndex);
                _presenter.ClearTextFields();
            }

			EnableTextFields();
        }

        private void GameMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(questionView.SelectedItems.Count > 0)
                _presenter.SaveQuestion(questionView.SelectedIndices[0]);
            if(fileView.SelectedItems.Count > 0)
                _presenter.SaveQuestionFile(fileView.SelectedItems[0].Text);
        }

        private void toolStripAdd_Click(object sender, System.EventArgs e)
        {
            if (fileView.SelectedItems.Count == 0)
                _presenter.ShowError("No question file selected. Cannot create new question.");
            else
            {
                _presenter.AddQuestion();
            }
        }

        private void toolStripDelete_Click(object sender, System.EventArgs e)
        {
            if (questionView.SelectedItems.Count == 0)
                _presenter.ShowError("No question selected to delete!");
            else
            {
				if(_presenter.ConfirmDelete())
					_presenter.RemoveQuestion();
            }
        }

		private void splitter_SplitterMoved(object sender, SplitterEventArgs e)
		{
			fileHeader.Width = fileView.Width;
		}

		public void EnableTextFields()
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

		private void comboBoxCorrectAnswer_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(questionView.SelectedItems.Count > 0)
				_presenter.UpdateAnswer();
		}

		private void textBoxQuestion_Leave(object sender, System.EventArgs e)
		{
			_presenter.SaveQuestion(questionView.SelectedIndices[0]);
		}
    }
}