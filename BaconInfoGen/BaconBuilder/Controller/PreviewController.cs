using BaconBuilder.Model;
using BaconBuilder.View;

namespace BaconBuilder.Controller
{
	internal class PreviewController
	{
		private readonly BaconModel _model;
		private readonly IPreviewView _view;

		public PreviewController(BaconModel model, IPreviewView view)
		{
			_model = model;
			_view = view;
		}

		public void QrCode(string fileName)
		{
			_view.QrCodeImage = _model.QrCode(fileName);
		}

		public void QrCode()
		{
			QrCode(_model.CurrentFileName);
		}
	}
}