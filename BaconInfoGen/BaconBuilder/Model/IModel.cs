using System.Collections.Generic;
using System.Drawing;

namespace BaconBuilder.Model
{
	public interface IModel
	{
		Dictionary<string, string> LoadFiles();
		void RemoveFile(string fileName);
		void CreateNewFile(string fileName);
		void RenameFile(string oldName, string newName);
		string ReadFile(string p);
		Dictionary<string, string>.KeyCollection FileNames { get; }

		string CurrentFileWithExtension { get; set; }
		string CurrentFile { get; }
		string CurrentContents { get; set; }

		Image QrCode(string file);
	}
}