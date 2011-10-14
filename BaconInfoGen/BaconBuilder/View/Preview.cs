using System.Drawing;
using System.Windows.Forms;
using BaconBuilder.Controller;
using BaconBuilder.Model;

namespace BaconBuilder.View
{
	public partial class Preview : Form, IPreviewView
	{
		private readonly PreviewController _controller;
		private readonly BaconModel _model;

		public Preview(BaconModel model)
		{
			InitializeComponent();

			btnClose.DialogResult = DialogResult.OK;

			_model = model;
			_controller = new PreviewController(_model, this);
			_controller.QrCode();
		}

		#region IPreviewView Members

		public Image QrCodeImage
		{
			get { return picboxQRCode.Image; }
			set { picboxQRCode.Image = value; }
		}

		#endregion
	}
}