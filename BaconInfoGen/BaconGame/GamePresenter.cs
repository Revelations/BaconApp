using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BaconBuilder.Model;
using Common;

namespace BaconGame
{
	/// <summary>
	/// Controls presentation and layout of an IGameMainForm
	/// </summary>
	public class GamePresenter
	{
		/// <summary>
		/// The form that this controls presentation for.
		/// </summary>
		private readonly IGameMainForm _view;

		/// <summary>
		/// The currently selected set of questions.
		/// </summary>
		private QuestionFile _current;

		/// <summary>
		/// Constructor accepting a single argument.
		/// </summary>
		/// <param name="view">The form that this presenter controls the layout for.</param>
		public GamePresenter(IGameMainForm view)
		{
			_view = view;
		}

		// Contains methods for modifying the contents and presentation of the two listboxes in the view.
		#region Listview Presentation

		/// <summary>
		/// Populates the file view with any question files stored in the local directory.
		/// </summary>
		public void PopulateFileView()
		{
			_view.FileView.Items.Clear();
			_view.FileView.Refresh();

			foreach (string file in FileHandler.GetQuestionFileList())
			{
				_view.FileView.Items.Add(file, 0);
			}
		}

		/// <summary>
		/// Populates the question view with questions, based on the selected question file.
		/// </summary>
		/// <param name="questionFile">The currently open question file.</param>
		public void PopulateQuestionView(string questionFile)
		{
			_current = FileHandler.CreateQuestionsFromFile(questionFile);

			foreach (Question q in _current.Questions)
			{
				AddQuestionToView(q);
			}
		}

		/// <summary>
		/// Clears the question view.
		/// </summary>
		public void DepopulateQuestionView()
		{
			_view.QuestionView.Items.Clear();
		}

		/// <summary>
		/// Adds a single question to the question view.
		/// </summary>
		/// <param name="q">The question object to add.</param>
		private void AddQuestionToView(Question q)
		{
			_view.QuestionView.Items.Add((_view.QuestionView.Items.Count + 1).ToString(), 0);
			_view.QuestionView.Items[_view.QuestionView.Items.Count - 1].SubItems.Add(q.QuestionText);
			_view.QuestionView.Items[_view.QuestionView.Items.Count - 1].SubItems.Add(q.Answers[q.CorrectAnswer]);
		}

		/// <summary>
		/// Updates the modified answer to a question in the question view.
		/// </summary>
		public void UpdateAnswer()
		{
			_view.QuestionView.SelectedItems[0].SubItems[2].Text = _view.CorrectAnswer.Text;
		}
		
		#endregion

		// Contains methods for modifying the contents and presentation of the view's textboxes.
		#region Textbox Presentation

		/// <summary>
		/// Clears each of the text and combo boxes in the view.
		/// </summary>
		public void ClearTextFields()
		{
			_view.QuestionText = string.Empty;
			_view.Answer1Text = string.Empty;
			_view.Answer2Text = string.Empty;
			_view.Answer3Text = string.Empty;
			_view.Answer4Text = string.Empty;
			_view.CorrectAnswer.SelectedText = string.Empty;
		}

		/// <summary>
		/// Loads the contents of the currently selected question into the text and combo boxes.
		/// </summary>
		public void LoadTextFields()
		{
			Question q = _current.Questions[_view.QuestionView.SelectedIndices[0]];
			_view.QuestionText = q.QuestionText;
			_view.Answer1Text = q.Answers[0];
			_view.Answer2Text = q.Answers[1];
			_view.Answer3Text = q.Answers[2];
			_view.Answer4Text = q.Answers[3];

			InitComboBox(q);
		}

		#endregion

		// Contains methods related to the manipulation of questions and question files.
		#region Question Manipulation

		/// <summary>
		/// Creates a new blank question and appends it to the question view.
		/// </summary>
		public void AddQuestion()
		{
			Question q = Question.InitBlankQuestion();
			_current.Questions.Add(Question.InitBlankQuestion());
			AddQuestionToView(q);
		}

		/// <summary>
		/// Deletes a selected question and removes it from the question box.
		/// </summary>
		public void RemoveQuestion()
		{
			int index = _view.QuestionView.SelectedIndices[0];

			_view.QuestionView.Items.RemoveAt(index);
			_current.Questions.RemoveAt(index);

			SaveQuestionFile(_view.FileView.SelectedItems[0].Text);
		}

		/// <summary>
		/// Brings up a confirmation of question deletion dialog.
		/// </summary>
		/// <returns>True if the user confirms deletion, false otherwise.</returns>
		public bool ConfirmDelete()
		{
			return MessageBox.Show(@"Are you sure you wish to delete this question?", @"Confirm.", MessageBoxButtons.OKCancel,
								   MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK;
		}

		/// <summary>
		/// Saves the question at a given index of the question view.
		/// </summary>
		/// <param name="index">Listview item index of the question to save.</param>
		public void SaveQuestion(int index)
		{
			Question q = _current.Questions[index];

			q.QuestionText = _view.QuestionText;
			q.Answers[0] = _view.Answer1Text;
			q.Answers[1] = _view.Answer2Text;
			q.Answers[2] = _view.Answer3Text;
			q.Answers[3] = _view.Answer4Text;

			_view.QuestionView.Items[index].SubItems[1].Text = q.QuestionText;

			q.CorrectAnswer = _view.CorrectAnswer.SelectedIndex;

			// Touch my hacks at your own peril.
			_current.Questions[index] = q;

			InitComboBox(q);
		}

		/// <summary>
		/// Saves the set of currently selected questions to a file.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		public void SaveQuestionFile(string fileName)
		{
			FileHandler.CreateFileFromQuestions(fileName, FileHandler.CreateFileContentFromQuestions(_current.Questions));
		}

		/// <summary>
		/// Saves any open questions and question files.
		/// </summary>
		public void SaveOpen()
		{
			if (_view.QuestionView.SelectedItems.Count > 0)
				SaveQuestion(_view.QuestionView.SelectedIndices[0]);
			if (_view.FileView.SelectedItems.Count > 0)
				SaveQuestionFile(_view.FileView.SelectedItems[0].Text);
		}

		#endregion

		// Contains methods related to uploading and downloading files from a server.
		#region Synchronisation

		/// <summary>
		/// Opens a sync dialog to download necessary files.
		/// </summary>
		public void DownloadSync()
		{
			SyncDialog dialog = new SyncDialog(new SyncInfo(Resources.GameDirectory, "Game/", SyncJobType.Download));
			dialog.ShowDialog();
		}

		/// <summary>
		/// Opens a sync dialog to upload necessary files.
		/// </summary>
		public void UploadSync()
		{
			LogGenerator.CreateGameLog();

			SyncDialog dialog = new SyncDialog(new SyncInfo(Resources.GameDirectory, "Game/", SyncJobType.Upload));
			dialog.ShowDialog();	
		}

		/// <summary>
		/// Opens a sync dialog to upload necessary files on form exit.
		/// 
		/// First displays a confirmation to the user.
		/// </summary>
		/// <param name="e">Form closing event args.</param>
		public void UploadSync(FormClosingEventArgs e)
		{
			DialogResult result = MessageBox.Show(@"Would you like to synchronise your content with the distribution server before exiting?",
					@"Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);

			if (result == DialogResult.Cancel)
				e.Cancel = true;
			else if (result == DialogResult.Yes)
				UploadSync();
		}

		/// <summary>
		/// Creates missing question files from the html files present on the server.
		/// </summary>
		public void CreateNeededQuestionFiles()
		{
			FileHandler.CreateNeededQuestionFiles();
		}

		#endregion

		/// <summary>
		/// Populates the combo box with a list of possible answers to select from.
		/// </summary>
		/// <param name="q">The question to use for population.</param>
		private void InitComboBox(Question q)
		{
			_view.CorrectAnswer.Items.Clear();
			for (int i = 0; i < 4; i++)
			{
				_view.CorrectAnswer.Items.Add(q.Answers[i]);
			}
			_view.CorrectAnswer.SelectedIndex = q.CorrectAnswer;
		}

		/// <summary>
		/// Shows an error dialog to the user.
		/// </summary>
		/// <param name="error">String to display.</param>
		public void ShowError(string error)
		{
			MessageBox.Show(error, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}
}
