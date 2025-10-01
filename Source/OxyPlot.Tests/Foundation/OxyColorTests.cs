// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OxyColorTests.cs" company="OxyPlot">
//   Copyright (c) 2014 OxyPlot contributors
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using NUnit.Framework;
    using NUnit.Framework.Legacy;

    // ReSharper disable InconsistentNaming
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    [TestFixture]
    public class OxyColorTests
    {
        [Test]
        public void Parse()
        {
            ClassicAssert.AreEqual(OxyColors.Red, OxyColor.Parse("#FF0000"));
            ClassicAssert.AreEqual(OxyColors.Red, OxyColor.Parse("#FFFF0000"));
            ClassicAssert.AreEqual(OxyColors.Red, OxyColor.Parse("255,0,0"));
            ClassicAssert.AreEqual(OxyColors.Red, OxyColor.Parse("255,255,0,0"));
        }

        [Test]
        public void ColorDifference()
        {
            ClassicAssert.AreEqual(1.1189122525867927d, OxyColor.ColorDifference(OxyColors.Red, OxyColors.Green), 1e-6);
        }

        [Test]
        public void FromAColor()
        {
            ClassicAssert.AreEqual(OxyColor.FromArgb(0x80, 0xff, 0, 0), OxyColor.FromAColor(0x80, OxyColors.Red));
        }

        [Test]
        public void FromHsv()
        {
            ClassicAssert.AreEqual(OxyColors.Red, OxyColor.FromHsv(0, 1, 1));
        }

        [Test]
        public void FromRgb()
        {
            ClassicAssert.AreEqual(OxyColors.Red, OxyColor.FromRgb(255, 0, 0));
        }

        [Test]
        public void FromArgb()
        {
            ClassicAssert.AreEqual(OxyColors.Red, OxyColor.FromArgb(255, 255, 0, 0));
        }

        [Test]
        public void FromUInt32()
        {
            ClassicAssert.AreEqual(OxyColors.Red, OxyColor.FromUInt32(0xFFFF0000));
        }

        [Test]
        public void GetColorName()
        {
            ClassicAssert.AreEqual("Red", OxyColors.Red.GetColorName());
        }

        [Test]
        public void ChangeIntensity()
        {
            ClassicAssert.AreEqual(OxyColor.FromArgb(255, 127, 0, 0), OxyColors.Red.ChangeIntensity(0.5));
        }

        [Test]
        public void ChangeSaturation()
        {
            ClassicAssert.AreEqual(OxyColor.FromArgb(255, 255, 127, 127), OxyColors.Red.ChangeSaturation(0.5));
        }

        [Test]
        public void ChangeSaturation_OverSaturate()
        {
            ClassicAssert.AreEqual(OxyColor.FromArgb(255, 255, 0, 0), OxyColors.Red.ChangeSaturation(2));
        }

        [Test]
        public void Complementary()
        {
            ClassicAssert.AreEqual(OxyColors.Cyan, OxyColors.Red.Complementary());
        }

        [Test]
        public void ToByteString()
        {
            ClassicAssert.AreEqual("255,255,0,0", OxyColors.Red.ToByteString());
        }

        [Test]
        public void ToCode()
        {
            ClassicAssert.AreEqual("OxyColors.Red", OxyColors.Red.ToCode());
            ClassicAssert.AreEqual("OxyColor.FromArgb(1, 2, 3, 4)", OxyColor.FromArgb(0x01, 0x02, 0x03, 0x04).ToCode());
        }

        [Test]
        public void ToHsv()
        {
            ClassicAssert.AreEqual(new[] { 0, 1, 1 }, OxyColors.Red.ToHsv());
        }

        [Test]
        public new void ToString()
        {
            ClassicAssert.AreEqual("#ffff0000", OxyColors.Red.ToString());
        }

        [Test]
        public void ToUint()
        {
            ClassicAssert.AreEqual(0xFFFF0000, OxyColors.Red.ToUint());
        }

        [Test]
        public void HueDifference()
        {
            ClassicAssert.AreEqual(1.0 / 3, OxyColor.HueDifference(OxyColors.Red, OxyColors.Blue), 1e-6);
        }
    }
}
