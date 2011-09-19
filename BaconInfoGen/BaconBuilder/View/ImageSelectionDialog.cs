using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BaconBuilder.View
{
	public partial class ImageSelectionDialog : Form
	{
		OpenFileDialog openFileDialog;
		private static string filterInnerDelimiter = "; ";
		private static string filterOuterDelimiter = " | ";
		string[][] filters = new string[][] {
			new [] {"*.bmp"},
			new [] {"*.gif"},
			new [] {"*.jpg","*.jpe","*.jpg"},
			new [] {"*.png"}
		};

		public ImageSelectionDialog()
		{
			InitializeComponent();

			openFileDialog = new OpenFileDialog();
		
			string allFilters = AllFilters("All Image Files");
			string bmpFilters = BitmapFilters("Bitmap Files", 0);
			string gifFilters = GifFilters("GIF Files", 1);
			string jpgFilters = JPEGFilters("JPEG Files", 2);
			string pngFilters = PNGFilters("PNG Files", 3);

			string[] f = new[] { allFilters, bmpFilters, gifFilters, jpgFilters, pngFilters };

			openFileDialog.Filter = String.Join(filterOuterDelimiter, f.Select(p => p.ToString()).ToArray());

			this.CancelButton = btnCancel;
			this.AcceptButton = btnOK;

			this.btnBrowse.Click += new System.EventHandler(this.btnBrowser_Click);
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
		}

		private string PNGFilters(string filterTags, int index)
		{
			StringBuilder builder = new StringBuilder();
			foreach (string str in filters[3])
				builder.Append(str).Append(filterInnerDelimiter);
			builder.Remove(builder.Length - filterInnerDelimiter.Length, filterInnerDelimiter.Length);

			return filterTags + filterOuterDelimiter + builder.ToString();
		}

		private string JPEGFilters(string filterTags, int index)
		{
			StringBuilder builder = new StringBuilder();
			foreach (string str in filters[2])
				builder.Append(str).Append(filterInnerDelimiter);
			builder.Remove(builder.Length - filterInnerDelimiter.Length, filterInnerDelimiter.Length);

			return filterTags + filterOuterDelimiter + builder.ToString();
		}

		private string GifFilters(string filterTags, int index)
		{
			StringBuilder builder = new StringBuilder();
			foreach (string str in filters[index])
				builder.Append(str).Append(filterInnerDelimiter);
			builder.Remove(builder.Length - filterInnerDelimiter.Length, filterInnerDelimiter.Length);

			return filterTags + filterOuterDelimiter + builder.ToString();
		}

		private string AllFilters(string filterTags)
		{
			StringBuilder builder = new StringBuilder();
			foreach (string[] arr in filters)
				foreach (string str in arr)
					builder.Append(str).Append(filterInnerDelimiter);
			builder.Remove(builder.Length - filterInnerDelimiter.Length, filterInnerDelimiter.Length);

			return filterTags + filterOuterDelimiter + builder.ToString();
		}

		private string BitmapFilters(string filterTags, int index)
		{
			StringBuilder builder = new StringBuilder();
			foreach (string str in filters[0])
				builder.Append(str).Append(filterInnerDelimiter);
			builder.Remove(builder.Length - filterInnerDelimiter.Length, filterInnerDelimiter.Length);

			return filterTags + filterOuterDelimiter + builder.ToString();
		}

		private void btnBrowser_Click(object sender, EventArgs e)
		{
			openFileDialog.ShowDialog();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.Tag = txtImageURL.Text;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
