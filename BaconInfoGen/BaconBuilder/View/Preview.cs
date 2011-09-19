using System.Windows.Forms;
using System.Runtime.InteropServices;
using BaconBuilder.Model;

namespace BaconBuilder.View
{
	public partial class Preview : Form
	{
		#region Disabling navigation click sounds

			private const int FEATURE_DISABLE_NAVIGATION_SOUNDS = 21;
			private const int SET_FEATURE_ON_THREAD = 0x00000001;
			private const int SET_FEATURE_ON_PROCESS = 0x00000002;
			private const int SET_FEATURE_IN_REGISTRY = 0x00000004;
			private const int SET_FEATURE_ON_THREAD_LOCALMACHINE = 0x00000008;
			private const int SET_FEATURE_ON_THREAD_INTRANET = 0x00000010;
			private const int SET_FEATURE_ON_THREAD_TRUSTED = 0x00000020;
			private const int SET_FEATURE_ON_THREAD_INTERNET = 0x00000040;
			private const int SET_FEATURE_ON_THREAD_RESTRICTED = 0x00000080;
			private Model.BaconModel model;

			[DllImport("urlmon.dll")]
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.Error)]
			static extern int CoInternetSetFeatureEnabled(
				int FeatureEntry,
				[MarshalAs(UnmanagedType.U4)] int dwFlags,
				bool fEnable);

			private void DisableNavClickSounds()
			{
				int feature = FEATURE_DISABLE_NAVIGATION_SOUNDS;
				CoInternetSetFeatureEnabled(feature, SET_FEATURE_ON_PROCESS, true);
			}

		#endregion

		public Preview(BaconModel model)
		{
			this.model = model;
			DisableNavClickSounds();

			InitializeComponent();

			browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(browser_DocumentCompleted);
		}

		private void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			// ((WebBrowser)sender).Document.OpenNew(true);
			// ((WebBrowser)sender).Document.Write("<html><body><p>Hello</p></body></html>");

			((WebBrowser)sender).DocumentText = "<html><body><p>Hello</p></body></html>";//model.GetPageCode();

			((WebBrowser)sender).DocumentCompleted -= browser_DocumentCompleted;
		}


	}
}
