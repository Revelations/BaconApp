using System;
using System.Collections.Generic;
using System.IO;

namespace BaconGame
{
    public class FileHandler
    {
        // We all recognize this, amirite?
        private static readonly string Dir = "C:/Users/" + Environment.UserName + "/QuestionTest/";

        private const string _questionExtension = ".ques";

        private static string QuestionDirectory
        {
            get
            {
                if (!Directory.Exists(Dir))
                    Directory.CreateDirectory(Dir);
                return Dir;
            }
        }

        public static IEnumerable<string> GetQuestionFileList()
        {
            List<string> result = new List<string>();

            DirectoryInfo d = new DirectoryInfo(QuestionDirectory);
            foreach (FileInfo f in d.GetFiles())
                if(f.Extension.Equals(_questionExtension))
                    result.Add(f.Name.Substring(0, f.Name.Length - _questionExtension.Length));

            return result;
        }

        public static QuestionFile CreateQuestionsFromFile(string path)
        {
            QuestionFile result = new QuestionFile(path);

            string[] content = File.ReadAllLines(QuestionDirectory + path + _questionExtension);

            for(int i = 0; i < content.Length; i += 6)
            {
                Question q = new Question
                {
                    QuestionText = content[i],
                    Answers = new[] {content[i + 1], content[i + 2], content[i + 3], content[i + 4]},
                    CorrectAnswer = Convert.ToInt32(content[i + 5])
                };
                
                result.Questions.Add(q);
            }

            return result;
        }
    }
}
