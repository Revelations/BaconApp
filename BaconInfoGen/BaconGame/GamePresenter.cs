using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common;

namespace BaconGame
{
	public class GamePresenter
	{
		private readonly IGameMainForm _view;

		private QuestionFile _current;

		public GamePresenter(IGameMainForm view)
		{
			_view = view;
		}

		public void PopulateFileView()
		{
			_view.FileView.Items.Clear();
			_view.FileView.Refresh();

			foreach (string file in FileHandler.GetQuestionFileList())
			{
				_view.FileView.Items.Add(file, 0);
			}
		}

		public void PopulateQuestionView(string questionFile)
		{
			_current = FileHandler.CreateQuestionsFromFile(questionFile);

			foreach (Question q in _current.Questions)
			{
				AddQuestionToView(q);
			}
		}

		private void AddQuestionToView(Question q)
		{
			_view.QuestionView.Items.Add((_view.QuestionView.Items.Count + 1).ToString(), 0);
			_view.QuestionView.Items[_view.QuestionView.Items.Count - 1].SubItems.Add(q.QuestionText);
			_view.QuestionView.Items[_view.QuestionView.Items.Count - 1].SubItems.Add(q.Answers[q.CorrectAnswer]);
		}

		public void DepopulateQuestionView()
		{
			_view.QuestionView.Items.Clear();
		}

		public void LoadTextFields()
		{
			Question q = _current.Questions[_view.QuestionView.SelectedIndices[0]];
			_view.QuestionText = q.QuestionText;
			_view.Answer1Text = q.Answers[0];
			_view.Answer2Text = q.Answers[1];
			_view.Answer3Text = q.Answers[2];
			_view.Answer4Text = q.Answers[3];

			// TODO: Add support for correct answer.
			InitComboBox(q);
			
		}

		private void InitComboBox(Question q)
		{
			_view.CorrectAnswer.Items.Clear();
			for (int i = 0; i < 4; i++)
			{
				_view.CorrectAnswer.Items.Add(q.Answers[i]);
			}
			_view.CorrectAnswer.SelectedIndex = q.CorrectAnswer;
		}

		public void ClearTextFields()
		{
			_view.QuestionText = string.Empty;
			_view.Answer1Text = string.Empty;
			_view.Answer2Text = string.Empty;
			_view.Answer3Text = string.Empty;
			_view.Answer4Text = string.Empty;
			_view.CorrectAnswer.SelectedText = string.Empty;
		}

		public void SaveQuestionFile(string fileName)
		{
			FileHandler.CreateFileFromQuestions(fileName, FileHandler.CreateFileContentFromQuestions(_current.Questions));
		}

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

			// TODO: Add support for correct answer.
		}

		public void ShowError(string error)
		{
			MessageBox.Show(error, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		public void AddQuestion()
		{
			Question q = Question.InitBlankQuestion();
			_current.Questions.Add(Question.InitBlankQuestion());
			AddQuestionToView(q);
		}

		public void RemoveQuestion()
		{
			int index = _view.QuestionView.SelectedIndices[0];

			_view.QuestionView.Items.RemoveAt(index);
			_current.Questions.RemoveAt(index);

			SaveQuestionFile(_view.FileView.SelectedItems[0].Text);
			DepopulateQuestionView();
			PopulateQuestionView(_view.FileView.SelectedItems[0].Text);
		}

		public void UpdateAnswer()
		{
			_view.QuestionView.SelectedItems[0].SubItems[2].Text = _view.CorrectAnswer.Text;
		}

		public bool ConfirmDelete()
		{
			return MessageBox.Show(@"Are you sure you wish to delete this question?", @"Confirm.", MessageBoxButtons.OKCancel,
			                       MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK;
		}

		public void DownloadSync()
		{
			SyncDialog dialog = new SyncDialog(new SyncInfo(Resources.GameDirectory, "Game/", SyncJobType.Download));
			dialog.ShowDialog();
		}

		public void UploadSync()
		{
			SyncDialog dialog = new SyncDialog(new SyncInfo(Resources.GameDirectory, "Game/", SyncJobType.Upload));
			dialog.ShowDialog();
		}

		public void UploadSync(FormClosingEventArgs e)
		{
			DialogResult result = MessageBox.Show(@"Would you like to synchronise your content with the distribution server before exiting?",
					@"Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);

			if (result == DialogResult.Cancel)
				e.Cancel = true;
			else if (result == DialogResult.Yes)
				UploadSync();
		}

		public void SaveOpen()
		{
			if (_view.QuestionView.SelectedItems.Count > 0)
				SaveQuestion(_view.QuestionView.SelectedIndices[0]);
			if (_view.FileView.SelectedItems.Count > 0)
				SaveQuestionFile(_view.FileView.SelectedItems[0].Text);
		}
	}
}
