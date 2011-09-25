﻿using System;
using System.Drawing;
using System.IO;
using BaconBuilder.Model;
using BaconBuilder.Properties;
using NUnit.Framework;

namespace BaconBuilder
{
	[TestFixture]
	public class ImageManipulatorTest
	{
		private ImageManipulator _manipulator;

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

		[Test]
		public void TestImage()
		{
			Bitmap expected = new Bitmap(256, 256);

			_manipulator = new ImageManipulator(expected);

			Assert.AreEqual(expected, _manipulator.Image);
		}

		[Test]
		public void TestResize()
		{
			Size expected = new Size(340, 300);
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
		public void TestCropResize()
		{
			Size expected = new Size(340, 300);
			_manipulator.ScaleImage(expected, true);

			Assert.AreEqual(expected, _manipulator.Image.Size);

			_manipulator = new ImageManipulator(new Bitmap(1024, 768));
			_manipulator.ScaleImage(expected.Width, expected.Height, true);

			Assert.AreEqual(expected, _manipulator.Image.Size);
		}

		[Test]
		public void TestSave()
		{
			_manipulator.SaveImage(Resources.HtmlDirectory, "TestImage");
			Assert.IsTrue(File.Exists(Resources.HtmlDirectory + "TestImage.png"));
			File.Delete(Resources.HtmlDirectory +"Test.png");

			_manipulator.SaveImage(Resources.HtmlDirectory, "TestImage", ImageType.Jpg);
			Assert.IsTrue(File.Exists(Resources.HtmlDirectory + "TestImage.jpg"));
			File.Delete(Resources.HtmlDirectory + "TestImage.jpg");
		}
	}
}