using System;
using System.Drawing;
using System.Windows.Forms;
using BaconBuilder.Controller;
using BaconBuilder.Model;

namespace BaconBuilder.View
{
	public partial class Preview : Form, IPreviewView
	{
		private readonly IModel _model;
		private readonly PreviewController _controller;
		public Preview(IModel model)
		{
			InitializeComponent();

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

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}