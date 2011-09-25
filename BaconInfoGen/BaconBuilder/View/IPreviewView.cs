using System.Drawing;

namespace BaconBuilder.View
{
	internal interface IPreviewView
	{
		Image QrCodeImage { get; set; }
		void SetBrowserText(string text);
	}
}