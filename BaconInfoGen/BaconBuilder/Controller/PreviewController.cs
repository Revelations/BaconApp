using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
			_view.BrowserText(text);
		}

		public void PreviewDocument()
		{
			_view.BrowserText(_model.CurrentContents);
		}
	}
}
