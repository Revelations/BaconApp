using System.Collections.Generic;
using System.Drawing;

namespace BaconBuilder.Model
{
	public interface IModel
	{
		Dictionary<string, string>.KeyCollection FileNames { get; }

		string CurrentFileNameWithExtension { get; set; }
		string CurrentFileName { get; set; }
		string CurrentContents { get; set; }
		void LoadFiles();
		void RemoveFile(string fileName);
		void CreateNewFile(string fileName);
		void RenameFile(string oldName, string newName);
		void SaveFile(string fileName);
		Image QrCode(string file);
	}
}