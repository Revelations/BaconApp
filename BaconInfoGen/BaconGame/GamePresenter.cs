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

            foreach(string file in FileHandler.GetQuestionFileList())
            {
                _view.FileView.Items.Add(file, 0);
            }
        }

        public void PopulateQuestionView(string questionFile)
        {
            _current = FileHandler.CreateQuestionsFromFile(questionFile);

            foreach(Question q in _current.Questions)
            {
                _view.QuestionView.Items.Add((_view.QuestionView.Items.Count + 1).ToString(), 0);
                _view.QuestionView.Items[_view.QuestionView.Items.Count - 1].SubItems.Add(q.QuestionText);
                _view.QuestionView.Items[_view.QuestionView.Items.Count - 1].SubItems.Add(q.CorrectAnswer.ToString());
            }
        }

        public void DepopulateQuestionView()
        {
            _view.QuestionView.Items.Clear();
        }

        public void LoadTextFields()
        {
            int selectedIndex = _view.QuestionView.SelectedIndices[0];
            _view.QuestionText = _current.Questions[selectedIndex].QuestionText;
            _view.Answer1Text = _current.Questions[selectedIndex].Answers[0];
            _view.Answer2Text = _current.Questions[selectedIndex].Answers[1];
            _view.Answer3Text = _current.Questions[selectedIndex].Answers[2];
            _view.Answer4Text = _current.Questions[selectedIndex].Answers[3];
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
    }
}
