using System.Drawing;
using System.Windows.Forms;

namespace BaconBuilder.View
{
	interface IPreviewView
	{
		Image QrCode { get; set; }
		void BrowserText(string text);
	}
}
