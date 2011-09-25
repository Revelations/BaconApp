using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

namespace BaconBuilder.View
{
	public partial class ImageSelectionDialog : Form
	{
		#region Fields, properties and constants
		private readonly Model.BaconModel _model;
		private OpenFileDialog _openImageDialog;

		private const string FILTER_INNER_DELIMITER = ";";
		private const string FILTER_OUTER_DELIMITER = "|";

		private const string ALL_FILES_TAG = "All Image Files";
		private const string BMP_FILES_TAG = "Bitmap";
		private const string GIF_FILES_TAG = "GIF";
		private const string JPG_FILES_TAG = "JPEG";
		private const string PNG_FILES_TAG = "PNG";

		private const int BMP_INDEX = 0;
		private const int GIF_INDEX = 1;
		private const int JPG_INDEX = 2;
		private const int PNG_INDEX = 3;

		private static readonly string[][] filters = new [] {
			new [] {"*.bmp"},
			new [] {"*.gif"},
			new [] {"*.jpe", "*.jpeg", "*.jpg"},
			new [] {"*.png"}
		};
		#endregion

		/// <summary>
		/// Constructor that accepts a model.
		/// </summary>
		/// <param name="model"></param>
		public ImageSelectionDialog(Model.BaconModel model)
		{
			InitializeComponent();

			_model = model;

			BuildOpenImageDialog();

			BuildBrowseButton();
			BuildImageUrlTextbox();
			BuildOptions();
			BuildOKButton();
			BuildCancelButton();
		}

		private void BuildOptions()
		{
			comboBox1.Items.AddRange(new [] {
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

			var f = new[] {
				Filters(ALL_FILES_TAG),
				Filters(BMP_FILES_TAG, BMP_INDEX),
				Filters(GIF_FILES_TAG, GIF_INDEX),
				Filters(JPG_FILES_TAG, JPG_INDEX),
				Filters(PNG_FILES_TAG, PNG_INDEX)
			};

			_openImageDialog.Filter = String.Join(FILTER_OUTER_DELIMITER, f.Select(p => p.ToString()).ToArray());
		}

		private static string Filters(string filterTags)
		{
			return filterTags + FILTER_OUTER_DELIMITER + String.Join(FILTER_INNER_DELIMITER, filters.SelectMany(secondLevel => secondLevel).Select(firstLevel => firstLevel).ToArray());
		}

		/// <summary>
		/// Creates a filter based on the filter index and the tag
		/// </summary>
		/// <param name="filterTags">Summary of the filter tag e.g. "All Files"</param>
		/// <param name="index">The index of the filer</param>
		/// <returns></returns>
		private static string Filters(string filterTags, int index)
		{
			return filterTags + FILTER_OUTER_DELIMITER + String.Join(FILTER_INNER_DELIMITER, filters[index].Select(firstLevel => firstLevel).ToArray());
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
		private void BuildOKButton()
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
