// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinearAxisTests.cs" company="OxyPlot">
//   Copyright (c) 2014 OxyPlot contributors
// </copyright>
// <summary>
//   Provides unit tests for the <see cref="LinearAxis" /> class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot.Wpf.Tests
{
    using NUnit.Framework;
    using NUnit.Framework.Legacy;

    /// <summary>
    /// Provides unit tests for the <see cref="LinearAxis" /> class.
    /// </summary>
    public class LinearAxisTests
    {
        /// <summary>
        /// Asserts that default values in the <see cref="LinearAxis" /> equal the default values in <see cref="Axes.LinearAxis" />.
        /// </summary>
        public class DefaultValues
        {
            /// <summary>
            /// Compares the properties related to the axis title.
            /// </summary>
            [Test]
            public void CompareTitleProperties()
            {
                var axis = new Axes.LinearAxis();
                var wpfAxis = new LinearAxis();
                ClassicAssert.AreEqual(axis.TitleColor, wpfAxis.TitleColor.ToOxyColor(), "TitleColor");
                ClassicAssert.AreEqual(axis.Title, wpfAxis.Title, "Title");
                ClassicAssert.AreEqual(axis.TitleClippingLength, wpfAxis.TitleClippingLength, "TitleClippingLength");
                ClassicAssert.AreEqual(axis.TitleFont, wpfAxis.TitleFont, "TitleFont");
                ClassicAssert.AreEqual(axis.TitleFontSize, wpfAxis.TitleFontSize, "TitleFontSize");
                ClassicAssert.AreEqual(axis.TitleFontWeight, wpfAxis.TitleFontWeight.ToOpenTypeWeight(), "TitleFontWeight");
                ClassicAssert.AreEqual(axis.TitleFormatString, wpfAxis.TitleFormatString, "TitleFormatString");
                ClassicAssert.AreEqual(axis.TitlePosition, wpfAxis.TitlePosition, "TitlePosition");
            }
        }
    }
}
