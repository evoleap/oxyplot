// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PngDecoderTests.cs" company="OxyPlot">
//   Copyright (c) 2014 OxyPlot contributors
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using NUnit.Framework;
    using NUnit.Framework.Legacy;

    // ReSharper disable InconsistentNaming
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    [TestFixture]
    public class PngDecoderTests
    {
        [Test]
        [TestCase(@"Imaging\TestImages\test_32bit.png", 137, 59)]
        public void Decode_32bitTestImages(string path, int w, int h)
        {
            var d = new PngDecoder();
            var pixels = d.Decode(File.ReadAllBytes(path));
            ClassicAssert.AreEqual(w, pixels.GetLength(0));
            ClassicAssert.AreEqual(h, pixels.GetLength(1));
            ClassicAssert.IsNotNull(pixels);
            var e = new PngEncoder(new PngEncoderOptions());
            var encodedPixels = e.Encode(pixels);
            File.WriteAllBytes(Path.ChangeExtension(path, "out.png"), encodedPixels);
        }

        [Test]
        [TestCase(@"Imaging\TestImages\test_8bit.png", 137, 59)]
        public void Decode_8bitTestImages(string path, int w, int h)
        {
            var d = new PngDecoder();

            // Expect exception here, 8bit pngs are not yet supported
            ClassicAssert.Throws<NotImplementedException>(() => d.Decode(File.ReadAllBytes(path)));
        }
    }
}
