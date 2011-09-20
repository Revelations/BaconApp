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
		private Model.BaconModel model;
		private OpenFileDialog openImageDialog;

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

		private static string[][] filters = new string[][] {
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
		public ImageSelectionDialog(BaconBuilder.Model.BaconModel model)
		{
			InitializeComponent();

			this.model = model;

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
				"Wrap selection",
				"Replace selection with image"
			});
		}

		/// <summary>
		/// Builds the OpenImageDialog
		/// </summary>
		private void BuildOpenImageDialog()
		{
			openImageDialog = new OpenFileDialog();

			string[] f = new[] {
				Filters(ALL_FILES_TAG),
				Filters(BMP_FILES_TAG, BMP_INDEX),
				Filters(GIF_FILES_TAG, GIF_INDEX),
				Filters(JPG_FILES_TAG, JPG_INDEX),
				Filters(PNG_FILES_TAG, PNG_INDEX)
			};

			openImageDialog.Filter = String.Join(FILTER_OUTER_DELIMITER, f.Select(p => p.ToString()).ToArray());
		}

		#region Filter parsing
		private string Filters(string filterTags)
		{
			string[] concatFilters = filters.SelectMany(secondLevel => secondLevel).Select(firstLevel => firstLevel).ToArray();

			return filterTags + FILTER_OUTER_DELIMITER + String.Join(FILTER_INNER_DELIMITER, concatFilters);
		}

		private string Filters(string filterTags, int index)
		{
			string[] concatFilters = filters[index].Select(firstLevel => firstLevel).ToArray();

			return filterTags + FILTER_OUTER_DELIMITER + String.Join(FILTER_INNER_DELIMITER, concatFilters);
		}

		#endregion


		/// <summary>
		/// Builds the ImageURL textbox.
		/// </summary>
		private void BuildImageUrlTextbox()
		{
			txtImageURL.TextChanged += new EventHandler(txtImageURL_TextChanged);
			txtImageURL.Select();
		}

		/// <summary>
		/// Builds the Browse button.
		/// </summary>
		private void BuildBrowseButton()
		{
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowser_Click);
		}

		/// <summary>
		/// Builds the Cancel button
		/// </summary>
		private void BuildCancelButton()
		{
			this.CancelButton = btnCancel;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;

			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
		}

		/// <summary>
		/// Builds the OK button.
		/// </summary>
		private void BuildOKButton()
		{
			this.AcceptButton = btnOK;
			this.btnOK.Enabled = (!string.IsNullOrEmpty(txtImageURL.Text));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
		}

		#region Event handlers
		private void txtImageURL_TextChanged(object sender, EventArgs e)
		{
			this.btnOK.Enabled = (!string.IsNullOrEmpty(txtImageURL.Text));
		}

		private void btnBrowser_Click(object sender, EventArgs e)
		{
			if (openImageDialog.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
			{
				txtImageURL.Text = openImageDialog.FileName;
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			model.ImageUrl = txtImageURL.Text;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			
		}
		#endregion
	}
}
