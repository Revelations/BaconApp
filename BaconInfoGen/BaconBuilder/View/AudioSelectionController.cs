using BaconBuilder.Model;

namespace BaconBuilder.View
{
	public class AudioSelectionController
	{
		private readonly IModel _model;
		private readonly IMediaSelectionDialog _view;
		
		public AudioSelectionController(IModel model, IMediaSelectionDialog view)
		{
			_model = model;
			_view = view;
		}

		public void InsertAudio()
		{
			//Take selection tex and do stuff to it.
			//string selectedtext = _model.SelectedText;
			//int start = _view.SelectionStart()
			//_model.CurrentContents
		}
	}
}