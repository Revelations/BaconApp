using System;

namespace BaconGame
{
    public struct Question
    {
        public string QuestionText { get; set; }

        private string[] _answers;
        public string[] Answers
        {
            get { return _answers; }
            set { _answers = value; }
        }

        private int _correctAnswer;
        public int CorrectAnswer
        {
            get { return _correctAnswer; }
            set { _correctAnswer = Math.Min(value, 3); }
        }

        public Question(string text, string[] answers, int correctAnswer):this()
        {
            QuestionText = text;
            _answers = answers;
            _correctAnswer = correctAnswer;
        }

        public static Question InitBlankQuestion()
        {
            return new Question(string.Empty, new []{string.Empty, string.Empty, string.Empty, string.Empty}, 0);
        }
    }
}
