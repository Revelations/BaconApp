namespace BaconBuilder.View
{
	public interface IMediaSelectionDialog
	{
		string FileName { get; set; }
		void ShowOpenItemDialog();
	}
}