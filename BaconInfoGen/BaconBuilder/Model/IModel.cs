using System;
using System.Collections.Generic;
using System.Drawing;

namespace BaconBuilder.Model
{
	public interface IModel
	{
		/// <summary>
		/// Get the collection of filenames.
		/// </summary>
		Dictionary<string, string>.KeyCollection FileNames { get; }

		/// <summary>
		/// Currently loaded filename INcluding extension.
		/// </summary>
		string CurrentFileNameWithExtension { get; set; }

		/// <summary>
		/// Currently loaded filename EXcluding extension.
		/// </summary>
		string CurrentFileName { get; set; }

		/// <summary>
		/// Contents of currently loaded file.
		/// </summary>
		string CurrentContents { get; set; }

		string AudioUrl { get; set; }

		/// <summary>
		/// Change the working directory.
		/// </summary>
		/// <param name="newDir">The name of the new working directory.</param>
		void ChangeDirectory(string newDir);

		/// <summary>
		/// Loads files in the current working directory.
		/// </summary>
		void LoadFiles();

		/// <summary>
		/// Remove the file from the wroking directory.
		/// </summary>
		/// <param name="fileName"></param>
		void RemoveFile(string fileName);

		/// <summary>
		/// Creates a new file with the name.
		/// </summary>
		/// <param name="fileName">Name of file</param>
		void CreateNewFile(string fileName);

		/// <summary>
		/// Renames the file to a new name.
		/// </summary>
		/// <param name="oldName">The current name.</param>
		/// <param name="newName">The new name.</param>
		void RenameFile(string oldName, string newName);

		/// <summary>
		/// Saves the file.
		/// </summary>
		/// <param name="fileName">The filename of the file to save.</param>
		void SaveFile(string fileName);

		Image QrCode(string file);

		//string CurrentParsedContents { get; }
		Uri GetCurrentFileUri();
	}
}