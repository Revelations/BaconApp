using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BaconBuilder.Model;

namespace BaconBuilder.View
{
    public enum ContentType
    {
        Image,
        Audio
    }

	public partial class MediaSelectionDialog : Form, IMediaSelectionDialog
	{
		#region Fields, properties and constants

		private string _filter;

		private readonly BaconModel _model;
	    private readonly ContentType _contentType;
	    private OpenFileDialog _openImageDialog;

		#endregion

	    /// <summary>
	    /// Constructor that accepts a model.
	    /// </summary>
	    /// <param name="model"></param>
	    /// <param name="contentType"></param>
	    public MediaSelectionDialog(BaconModel model, ContentType contentType)
		{
			InitializeComponent();

			_model = model;
	        _contentType = contentType;

            InitBasedOnContentType();

	        BuildOpenImageDialog();
			BuildBrowseButton();
			BuildImageUrlTextbox();
			BuildOptions();
			BuildOkButton();
			BuildCancelButton();


		}

        private void InitBasedOnContentType()
        {
            lblImageLocation.Text = _contentType + @" Location:";
            Text = @"Select " + _contentType;

            comboBox1.Items.AddRange(new[]
			                         	{
			                         		"Insert " + _contentType.ToString().ToLower() + " before selction",
			                         		"Insert " + _contentType.ToString().ToLower() + " after selction",
			                         		"Replace selection with " + _contentType.ToString().ToLower()
			                         	});

            if(_contentType == ContentType.Image)
                _filter =
                    "All Image Files|*.bmp;*.gif;*.jpe;*.jpeg;*.jpg;*.png|Bitmap|*.bmp|GIF|*.gif|JPEG|*.jpe;*.jpeg;*.jpg|PNG|*.png";
            else
                _filter = "All Audio Files|*.mp3;*.ogg|MP3|*.mp3|Ogg|*.ogg";
        }

		private void BuildOptions()
		{
			comboBox1.SelectedIndex = 2;
		}

		/// <summary>
		/// Builds the OpenImageDialog
		/// </summary>
		private void BuildOpenImageDialog()
		{
			_openImageDialog = new OpenFileDialog();
			_openImageDialog.Filter = _filter;
		}

		/// <summary>
		/// Builds the ImageURL textbox.
		/// </summary>
		private void BuildImageUrlTextbox()
		{
			txtImageURL.TextChanged += txtImageURL_TextChanged;
			txtImageURL.Select();
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

		private void txtImageURL_TextChanged(object sender, EventArgs e)
		{
			btnOK.Enabled = (!string.IsNullOrEmpty(ItemFileName));
		}

		private void btnBrowser_Click(object sender, EventArgs e)
		{
			if (_openImageDialog.ShowDialog() != DialogResult.Cancel)
			{
				ItemFileName = _openImageDialog.FileName;
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
            FileInfo f = new FileInfo(ItemFileName);
            ImageManipulator i = new ImageManipulator(ItemFileName);

            i.ScaleImage(270, 200, true);

		    string fileName = f.Name.Replace(f.Extension, "");

            i.SaveImage("C:/Users/" + Environment.UserName + "/test/", fileName, ImageType.Jpg);

			_model.ImageUrl = fileName + ".jpg";
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{

		}

		#endregion

		public string ItemFileName
		{
			get { return txtImageURL.Text; }
			set { txtImageURL.Text = value; }
		}

		public void ShowOpenItemDialog()
		{
			 
		}
	}
}