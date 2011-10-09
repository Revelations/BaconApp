using BaconBuilder.Model;
using BaconBuilder.View;

namespace BaconBuilder.Controller
{
	class PreviewController
	{
		private readonly IModel _model;
		private readonly IPreviewView _view;

		public PreviewController(IModel model, IPreviewView view)
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
