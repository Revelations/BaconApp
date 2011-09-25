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

		public void PreviewPage(string text)
		{
			_view.SetBrowserText(text);
		}

		public void PreviewDocument()
		{
			_view.SetBrowserText(_model.CurrentContents);
		}
	}
}
