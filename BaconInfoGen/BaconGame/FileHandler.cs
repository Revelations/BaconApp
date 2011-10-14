using System;
using System.Collections.Generic;
using System.IO;

namespace BaconGame
{
    public class FileHandler
    {
		/// <summary>
		/// 
		/// </summary>
        private const string _questionExtension = ".ques";

		/// <summary>
		/// 
		/// </summary>
		private static string QuestionDirectory { get { return Common.Resources.GameDirectory; } }

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
        public static IEnumerable<string> GetQuestionFileList()
        {
            List<string> result = new List<string>();

            DirectoryInfo d = new DirectoryInfo(QuestionDirectory);
            foreach (FileInfo f in d.GetFiles())
                if(f.Extension.Equals(_questionExtension))
                    result.Add(f.Name.Substring(0, f.Name.Length - _questionExtension.Length));

            return result;
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="path"></param>
		/// <param name="content"></param>
        public static void CreateFileFromQuestions(string path, string[] content)
        {
            File.WriteAllLines(QuestionDirectory + path + _questionExtension, content);
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="questions"></param>
		/// <returns></returns>
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

		/// <summary>
		/// 
		/// </summary>
		public static void CreateNeededQuestionFiles()
		{
			List<string> needed = GetNeededQuestionFiles();
			foreach (string s in needed)
			{
				if (!File.Exists(Common.Resources.GameDirectory + s + _questionExtension))
				{
					Stream stream = File.Create(Common.Resources.GameDirectory + s + _questionExtension);
					stream.Close();
				}
			}

			DeleteUnneededQuestionFiles(needed);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private static List<string> GetNeededQuestionFiles()
		{
			List<string> result = new List<string>();

			List<string> files = Common.SyncHelper.GetRemoteDirectoryListing("/Content");
			foreach (string s in files)
			{
				string[] split = s.Split('.');
				if (split[split.Length - 1].Equals("html"))
					result.Add(s.Substring(0, s.Length - 5));
			}

			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="needed"></param>
		private static void DeleteUnneededQuestionFiles(List<string> needed)
		{
			DirectoryInfo d = new DirectoryInfo(Common.Resources.GameDirectory);
			foreach (FileInfo f in d.GetFiles())
			{
				if (f.Extension.Equals(_questionExtension) && !needed.Contains(f.Name.Substring(0, f.Name.Length - 5)))
					File.Delete(Common.Resources.GameDirectory + f.Name);
			}
		}
    }
}
