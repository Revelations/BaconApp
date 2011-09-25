using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace BaconBuilder.Model
{
	public enum ImageType
	{
		Bmp,
		Gif,
		Jpg,
		Png
	}

	public class ImageManipulator
	{
		/// <summary>
		/// Constructor for the manipulator accepting a pre-created bitmap.
		/// </summary>
		/// <param name="bitmap">The bitmap to perform manipulation on.</param>
		public ImageManipulator(Bitmap bitmap)
		{
			Image = bitmap;
		}

		/// <summary>
		/// Constructor for the manipulator accepting a bitmap from file.
		/// </summary>
		/// <param name="fileName"></param>
		public ImageManipulator(string fileName)
		{
			if (File.Exists(fileName))
				Image = new Bitmap(fileName);
		}

		public Bitmap Image { get; private set; }

		/// <summary>
		/// Scales the size of an image to the given size.
		/// 
		/// Does not preserve aspect ratio.
		/// </summary>
		/// <param name="size">The desired output image size.</param>
		public void ScaleImage(Size size)
		{
			ScaleImage(size, false);
		}

		/// <summary>
		/// Scales the size of an image to the given width and height.
		/// 
		/// Does not preserve aspect ratio.
		/// </summary>
		/// <param name="width">The desired output image width.</param>
		/// <param name="height">The desired output image height.</param>
		public void ScaleImage(int width, int height)
		{
			ScaleImage(width, height, false);
		}

		/// <summary>
		/// Scales the size of an image to the given size.
		/// 
		/// Will optionally preserve aspect ratio and crop along one dimension if necessary to do so.
		/// </summary>
		/// <param name="size">The desired output image size.</param>
		/// <param name="preserveAspectRatio">Whether to preserve the original aspect ration of the image or not.</param>
		public void ScaleImage(Size size, bool preserveAspectRatio)
		{
			if (!preserveAspectRatio)
			{
				Image = new Bitmap(Image, size);
			}
			else
			{
				float desiredAspectRatio = (float) size.Width/size.Height;
				float currentAspectRatio = (float) Image.Width/Image.Height;

				int cropDistance =
					Math.Abs(Convert.ToInt32((currentAspectRatio*Image.Height - desiredAspectRatio*Image.Height)/2));

				Rectangle cropRegion = currentAspectRatio > desiredAspectRatio
				                       	? Rectangle.FromLTRB(cropDistance, 0, Image.Width - cropDistance, Image.Height)
				                       	: Rectangle.FromLTRB(0, cropDistance, Image.Width, Image.Height - cropDistance);

				Image = Image.Clone(cropRegion, Image.PixelFormat);

				Image = new Bitmap(Image, size);
			}
		}

		/// <summary>
		/// Scales the size of an image to the given size.
		/// 
		/// Will optionally preserve aspect ratio and crop along one dimension if necessary to do so.
		/// </summary>
		/// <param name="width">The desired output image width.</param>
		/// <param name="height">The desired output image height.</param>
		/// <param name="preserveAspectRatio">Whether to preserve the original aspect ration of the image or not.</param>
		public void ScaleImage(int width, int height, bool preserveAspectRatio)
		{
			ScaleImage(new Size(width, height), preserveAspectRatio);
		}

		/// <summary>
		/// Saves the current image in the manipulator to a file.
		/// </summary>
		/// <param name="directory">Output directory.</param>
		/// <param name="fileName">Name of the output image.</param>
		public void SaveImage(string directory, string fileName)
		{
			SaveImage(directory, fileName, ImageType.Png);
		}

		/// <summary>
		/// Saves the current image in the manipulator to a file.
		/// 
		/// Optional image format may be specified to output a certain image type.
		/// </summary>
		/// <param name="directory">Output directory.</param>
		/// <param name="fileName">Name of the output image.</param>
		/// <param name="type">The desired output format of the image.</param>
		public void SaveImage(string directory, string fileName, ImageType type)
		{
			Tuple<string, ImageFormat> formatInfo = GetImageFormatInfo(type);

			Image.Save(directory + fileName + formatInfo.Item1, formatInfo.Item2);
		}

		/// <summary>
		/// Gets the file extension and System.Drawing.Imaging.ImageFormat associated with the selected format type.
		/// </summary>
		/// <param name="imageType">The format to get extension / format information for.</param>
		/// <returns>Tuple containing string extension and System.Drawing.Imaging.ImageFormat of the image type.</returns>
		private Tuple<string, ImageFormat> GetImageFormatInfo(ImageType imageType)
		{
			string extension;
			ImageFormat format;

			switch (imageType)
			{
				case ImageType.Bmp:
					extension = ".bmp";
					format = ImageFormat.Bmp;
					break;
				case ImageType.Gif:
					extension = ".gif";
					format = ImageFormat.Gif;
					break;
				case ImageType.Jpg:
					extension = ".jpg";
					format = ImageFormat.Jpeg;
					break;
				case ImageType.Png:
					extension = ".png";
					format = ImageFormat.Png;
					break;
				default:
					extension = string.Empty;
					format = ImageFormat.Png;
					break;
			}

			var result = new Tuple<string, ImageFormat>(extension, format);

			return result;
		}
	}
}