namespace BaconBuilder.Model
{
	public interface IModel
	{
		void RemoveFile(string fileName);
		void CreateNewFile(string fileName);
		void RenameFile(string oldName, string newName);
		void LoadFiles();
	}
}