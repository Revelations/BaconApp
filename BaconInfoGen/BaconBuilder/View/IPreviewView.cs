using System.Drawing;

namespace BaconBuilder.View
{
	internal interface IPreviewView
	{
		Image QrCode { get; set; }
		void SetBrowserText(string text);
	}
}