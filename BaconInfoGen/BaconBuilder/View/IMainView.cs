using System.Windows.Forms;

namespace BaconBuilder.View
{
	public interface IMainView
	{
		string TitleText { get; set; }
		decimal XCoord { get; set; }
		decimal YCoord { get; set; }
		string Contents { get; set; }
		ListView.ListViewItemCollection Files { get; }
		bool IsRemoveButtonEnabled { get; set; }
	}
}
