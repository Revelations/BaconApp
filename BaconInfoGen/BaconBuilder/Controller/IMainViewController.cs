namespace BaconBuilder.Controller
{
	public interface IMainViewController
	{
		/// <summary>
		/// Gets the text content of an HTML file.
		/// 
		/// Uses an HtmlToTextParser to parse its content to plain text.
		/// </summary>
		/// <returns>String content (plain text) of the file.</returns>
		string LoadHtmlToText();

		/// <summary>
		/// Gets the html parsed verision of plain text content and saves it to a file.
		/// </summary>
		/// <param name="filename">The filename of the file to save to.</param>
		/// <param name="text">The input string to parse and write to file.</param>
		void SaveTextToHtml(string filename, string text);

		/// <summary>
		/// Creates a new Html file by cloning the existing blank one.
		/// </summary>
		void CreateNewFile();

		/// <summary>
		/// Removes the current file.
		/// </summary>
		void RemoveCurrentFile();

		/// <summary>
		/// Remove the file
		/// </summary>
		/// <param name="fileName"></param>
		void RemoveFile(string fileName);
		/// <summary>
		/// Rename the old file name to the new file name.
		/// </summary>
		/// <param name="oldName"></param>
		/// <param name="newName"></param>

		void RenameFile(string oldName, string newName);
		/// <summary>
		/// Initialises and populates a listview with the html files in a directory.
		/// </summary>
		void InitialiseListView();

		void SelectFile(string file);

		void ValidateTitle();
		bool ContentsHaveChanged();
	}
}