using System;
using System.Linq;
using System.Windows.Forms;
using BaconBuilder.Model;

namespace BaconBuilder.View
{
	public partial class ImageSelectionDialog : Form
	{
		#region Fields, properties and constants

		private const string FilterInnerDelimiter = ";";
		private const string FilterOuterDelimiter = "|";

		private const string AllFilesTag = "All Image Files";
		private const string BmpFilesTag = "Bitmap";
		private const string GifFilesTag = "GIF";
		private const string JpgFilesTag = "JPEG";
		private const string PngFilesTag = "PNG";

		private const int BmpIndex = 0;
		private const int GifIndex = 1;
		private const int JpgIndex = 2;
		private const int PngIndex = 3;

		private static readonly string[][] filters = new[]
		                                             	{
		                                             		new[] {"*.bmp"},
		                                             		new[] {"*.gif"},
		                                             		new[] {"*.jpe", "*.jpeg", "*.jpg"},
		                                             		new[] {"*.png"}
		                                             	};

		private readonly BaconModel _model;
		private OpenFileDialog _openImageDialog;

		#endregion

		/// <summary>
		/// Constructor that accepts a model.
		/// </summary>
		/// <param name="model"></param>
		public ImageSelectionDialog(BaconModel model)
		{
			InitializeComponent();

			_model = model;

			BuildOpenImageDialog();

			BuildBrowseButton();
			BuildImageUrlTextbox();
			BuildOptions();
			BuildOkButton();
			BuildCancelButton();
		}

		private void BuildOptions()
		{
			comboBox1.Items.AddRange(new[]
			                         	{
			                         		"Insert image before selction",
			                         		"Insert image after selction",
			                         		"Replace selection with image"
			                         	});
			comboBox1.SelectedIndex = 2;
		}

		/// <summary>
		/// Builds the OpenImageDialog
		/// </summary>
		private void BuildOpenImageDialog()
		{
			_openImageDialog = new OpenFileDialog();

			var f = new[]
			        	{
			        		Filters(AllFilesTag),
			        		Filters(BmpFilesTag, BmpIndex),
			        		Filters(GifFilesTag, GifIndex),
			        		Filters(JpgFilesTag, JpgIndex),
			        		Filters(PngFilesTag, PngIndex)
			        	};

			_openImageDialog.Filter = String.Join(FilterOuterDelimiter, f.Select(p => p.ToString()).ToArray());
		}

		private static string Filters(string filterTags)
		{
			return filterTags + FilterOuterDelimiter +
			       String.Join(FilterInnerDelimiter,
			                   filters.SelectMany(secondLevel => secondLevel).Select(firstLevel => firstLevel).ToArray());
		}

		/// <summary>
		/// Creates a filter based on the filter index and the tag
		/// </summary>
		/// <param name="filterTags">Summary of the filter tag e.g. "All Files"</param>
		/// <param name="index">The index of the filer</param>
		/// <returns></returns>
		private static string Filters(string filterTags, int index)
		{
			return filterTags + FilterOuterDelimiter +
			       String.Join(FilterInnerDelimiter, filters[index].Select(firstLevel => firstLevel).ToArray());
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
			btnOK.Enabled = (!string.IsNullOrEmpty(txtImageURL.Text));
			btnOK.DialogResult = DialogResult.OK;

			btnOK.Click += btnOK_Click;
		}

		#region Event handlers

		private void txtImageURL_TextChanged(object sender, EventArgs e)
		{
			btnOK.Enabled = (!string.IsNullOrEmpty(txtImageURL.Text));
		}

		private void btnBrowser_Click(object sender, EventArgs e)
		{
			if (_openImageDialog.ShowDialog() != DialogResult.Cancel)
			{
				txtImageURL.Text = _openImageDialog.FileName;
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			_model.ImageUrl = txtImageURL.Text;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
		}

		#endregion
	}
}