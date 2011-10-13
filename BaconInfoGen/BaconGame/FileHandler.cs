using System;
using System.Collections.Generic;
using System.IO;

namespace BaconGame
{
    public class FileHandler
    {
        private const string _questionExtension = ".ques";

		private static string QuestionDirectory { get { return Common.Resources.GameDirectory; } }

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
                Question q = new Question(content[i],
                                          new[] {content[i + 1], content[i + 2], content[i + 3], content[i + 4]},
                                          Convert.ToInt32(content[i + 5]));
                
                result.Questions.Add(q);
            }

            return result;
        }

        public static void CreateFileFromQuestions(string path, string[] content)
        {
            File.WriteAllLines(QuestionDirectory + path + _questionExtension, content);
        }

        public static string[] CreateFileContentFromQuestions(List<Question> questions)
        {
            List<string> result = new List<string>();

            foreach (Question q in questions)
            {
                result.Add(q.QuestionText);

                foreach (string s in q.Answers)
                    result.Add(s);

                result.Add(q.CorrectAnswer.ToString());
            }

            return result.ToArray();
        }
    }
}
