using System;
using System.Linq;
using System.Windows.Forms;
using BaconBuilder.Model;

namespace BaconBuilder.View
{
	public partial class AudioSelectionDialog : Form, IMediaSelectionDialog
	{
		#region Fields, properties and constants

		private IModel _model;
		private AudioSelectionController _controller;

		private OpenFileDialog _openFileDialog;
		private const string Filter = "All Audio Files|*.mp3;*.ogg|MP3|*.mp3|Ogg|*.ogg";

		#endregion

		/// <summary>
		/// Constructor that accepts a model.
		/// </summary>
		/// <param name="model"></param>
		public AudioSelectionDialog(IModel model)
		{
			InitializeComponent();
			_model = model;
			_controller = new AudioSelectionController(_model, this);
			BuildOpenImageDialog();

			BuildBrowseButton();
			BuildImageUrlTextbox();
			BuildOptions();
			BuildOkButton();
			BuildCancelButton();
		}

		private void BuildOptions()
		{
			comboBox1.Items.AddRange(new[] {
			        "Insert audio before selction",
			        "Insert audio after selction",
			        "Replace selection with audio"
			    });
			comboBox1.SelectedIndex = 2;
		}

		/// <summary>
		/// Builds the OpenImageDialog
		/// </summary>
		private void BuildOpenImageDialog()
		{
			_openFileDialog = new OpenFileDialog();

			_openFileDialog.Filter = Filter;
		}

		/// <summary>
		/// Builds the ImageURL textbox.
		/// </summary>
		private void BuildImageUrlTextbox()
		{
			txtFileURL.TextChanged += TxtFileUrlTextChanged;
			txtFileURL.Select();
		}

		/// <summary>
		/// Builds the Browse button.
		/// </summary>
		private void BuildBrowseButton()
		{
			btnBrowse.Click += btnBrowser_Click;
		}

		/// <summary>
		/// Builds the Cancel button
		/// </summary>
		private void BuildCancelButton()
		{
			CancelButton = btnCancel;
			btnCancel.DialogResult = DialogResult.Cancel;

			btnCancel.Click += btnCancel_Click;
		}

		/// <summary>
		/// Builds the OK button.
		/// </summary>
		private void BuildOkButton()
		{
			AcceptButton = btnOK;
			btnOK.Enabled = (!string.IsNullOrEmpty(ItemFileName));
			btnOK.DialogResult = DialogResult.OK;

			btnOK.Click += btnOK_Click;
		}

		#region Event handlers

		private void TxtFileUrlTextChanged(object sender, EventArgs e)
		{
			btnOK.Enabled = (!string.IsNullOrEmpty(txtFileURL.Text));
		}

		private void btnBrowser_Click(object sender, EventArgs e)
		{
			if (_openFileDialog.ShowDialog() != DialogResult.Cancel)
			{
				txtFileURL.Text = _openFileDialog.FileName;
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			//_controller.
			_model.AudioUrl = ItemFileName;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{

		}

		#endregion

		public string ItemFileName
		{
			get { return txtFileURL.Text; }
			set { txtFileURL.Text = value; }
		}

		public void ShowOpenItemDialog()
		{
			_openFileDialog.ShowDialog();
		}
	}
}