using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BaconBuilder.Model;

namespace BaconBuilder
{
    [TestFixture]
    public class ImageManipulatorTest
    {
        private ImageManipulator manipulator;

        [SetUp]
        public void SetUp()
        {
            manipulator = new ImageManipulator(new Bitmap(1024, 768));
        }

        [TearDown]
        public void TearDown()
        {
            manipulator = null;
        }

        [Test]
        public void TestImage()
        {
            Bitmap expected = new Bitmap(256, 256);

            manipulator = new ImageManipulator(expected);

            Assert.AreEqual(expected, manipulator.Image);
        }

        [Test]
        public void TestResize()
        {
            Size expected = new Size(340, 300);
            manipulator.ScaleImage(expected);

            Assert.AreEqual(expected, manipulator.Image.Size);

            manipulator = new ImageManipulator(new Bitmap(1024, 768));
            manipulator.ScaleImage(expected.Width, expected.Height);

            Assert.AreEqual(expected, manipulator.Image.Size);

            manipulator = new ImageManipulator(new Bitmap(768, 1024));
            manipulator.ScaleImage(expected);

            Assert.AreEqual(expected, manipulator.Image.Size);
        }

        [Test]
        public void TestCropResize()
        {
            Size expected = new Size(340, 300);
            manipulator.ScaleImage(expected, true);

            Assert.AreEqual(expected, manipulator.Image.Size);

            manipulator = new ImageManipulator(new Bitmap(1024, 768));
            manipulator.ScaleImage(expected.Width, expected.Height, true);

            Assert.AreEqual(expected, manipulator.Image.Size);
        }

        [Test]
        public void TestSave()
        {
            manipulator.SaveImage("C:/", "Test");
            Assert.IsTrue(File.Exists("C:/Test.png"));
            File.Delete("C:/Test.png");

            manipulator.SaveImage("C:/", "Test", ImageType.Jpg);
            Assert.IsTrue(File.Exists("C:/Test.jpg"));
            File.Delete("C:/Test.jpg");
        }
    }
}