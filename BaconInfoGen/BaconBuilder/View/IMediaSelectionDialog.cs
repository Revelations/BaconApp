namespace BaconBuilder.View
{
	public interface IMediaSelectionDialog
	{
		string ItemFileName { get; set; }
		void ShowOpenItemDialog();
	}
}
