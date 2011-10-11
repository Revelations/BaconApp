using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaconGame
{
    public struct QuestionFile
    {
        private readonly string _path;

        public List<Question> Questions { get; set; }

        public QuestionFile(string path) : this()
        {
            _path = path;

            Questions = new List<Question>();
        }
    }
}
