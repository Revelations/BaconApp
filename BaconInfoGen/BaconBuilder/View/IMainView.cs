using System.Windows.Forms;

namespace BaconBuilder.View
{
	public interface IMainView
	{
		string TitleText { get; set; }
		string XCoord { get; set; }
		string YCoord { get; set; }
		string Contents { get; set; }
		ListView.ListViewItemCollection Files { get; }
		bool IsRemoveButtonEnabled { get; set; }
	}
}
