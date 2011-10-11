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
		}

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                e.SuppressKeyPress = true;
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
    }
}