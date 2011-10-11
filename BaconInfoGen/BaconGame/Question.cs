using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaconGame
{
    public struct Question
    {
        public string QuestionText { get; set; }

        public string[] Answers { get; set; }

        private int _correctAnswer;
        public int CorrectAnswer
        {
            get { return _correctAnswer; }
            set { _correctAnswer = Math.Max(value, 3); }
        }

        public Question(string text, string[] answers, int correctAnswer) : this()
        {
            QuestionText = text;
            Answers = answers;
            CorrectAnswer = correctAnswer;
        }
    }
}
