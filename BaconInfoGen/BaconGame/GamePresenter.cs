using System.Collections.Generic;
using System.Windows.Forms;

namespace BaconGame
{
	public class GamePresenter
	{
		private IGameMainForm _view;

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
			_view.QuestionView.Items[_view.QuestionView.Items.Count - 1].SubItems.Add(q.CorrectAnswer.ToString());
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

			// Touch my hacks at your own peril.
			_current.Questions[index] = q;

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
	}
}
