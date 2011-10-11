using System;
using System.Drawing;
using System.IO;
using BaconBuilder.Model;
using NUnit.Framework;

namespace BaconBuilder
{
	[TestFixture]
	public class ImageManipulatorTest
	{
		#region Setup/Teardown

		[SetUp]
		public void SetUp()
		{
			_manipulator = new ImageManipulator(new Bitmap(1024, 768));
		}

		[TearDown]
		public void TearDown()
		{
			_manipulator = null;
		}

		#endregion

		private static readonly string HtmlDirectory = "C:/Users/" + Environment.UserName + "/test/";
		private ImageManipulator _manipulator;

		[Test]
		public void TestCropResize()
		{
			var expected = new Size(340, 300);
			_manipulator.ScaleImage(expected, true);

			Assert.AreEqual(expected, _manipulator.Image.Size);

			_manipulator = new ImageManipulator(new Bitmap(1024, 768));
			_manipulator.ScaleImage(expected.Width, expected.Height, true);

			Assert.AreEqual(expected, _manipulator.Image.Size);
		}

		[Test]
		public void TestImage()
		{
			var expected = new Bitmap(256, 256);

			_manipulator = new ImageManipulator(expected);

			Assert.AreEqual(expected, _manipulator.Image);
		}

		[Test]
		public void TestResize()
		{
			var expected = new Size(340, 300);
			_manipulator.ScaleImage(expected);

			Assert.AreEqual(expected, _manipulator.Image.Size);

			_manipulator = new ImageManipulator(new Bitmap(1024, 768));
			_manipulator.ScaleImage(expected.Width, expected.Height);

			Assert.AreEqual(expected, _manipulator.Image.Size);

			_manipulator = new ImageManipulator(new Bitmap(768, 1024));
			_manipulator.ScaleImage(expected);

			Assert.AreEqual(expected, _manipulator.Image.Size);
		}

		[Test]
		public void TestSave()
		{
			_manipulator.SaveImage(HtmlDirectory, "TestImage");
			Assert.IsTrue(File.Exists(HtmlDirectory + "TestImage.png"));
			File.Delete(HtmlDirectory + "Test.png");

			_manipulator.SaveImage(HtmlDirectory, "TestImage", ImageType.Jpg);
			Assert.IsTrue(File.Exists(HtmlDirectory + "TestImage.jpg"));
			File.Delete(HtmlDirectory + "TestImage.jpg");
		}
	}
}