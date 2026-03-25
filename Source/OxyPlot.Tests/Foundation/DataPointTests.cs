// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataPointTests.cs" company="OxyPlot">
//   Copyright (c) 2014 OxyPlot contributors
// </copyright>
// <summary>
//   Provides unit tests for the <see cref="DataPoint" /> type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot.Tests
{
    using NUnit.Framework;
    using NUnit.Framework.Legacy;

    /// <summary>
    /// Provides unit tests for the <see cref="DataPoint" /> type.
    /// </summary>
    [TestFixture]
    public class DataPointTests
    {
        /// <summary>
        /// Tests the <see cref="DataPoint.IsDefined" /> method.
        /// </summary>
        public class IsDefined
        {
            /// <summary>
            /// Given valid points, <c>true</c> is returned.
            /// </summary>
            [Test]
            public void ValidPoints()
            {
                ClassicAssert.IsTrue(new DataPoint(1, 2).IsDefined());
                ClassicAssert.IsTrue(new DataPoint(double.MaxValue, double.MaxValue).IsDefined());
                ClassicAssert.IsTrue(new DataPoint(double.MinValue, double.MinValue).IsDefined());
            }

            /// <summary>
            /// Given invalid points, <c>false</c> is returned.
            /// </summary>
            [Test]
            public void InvalidPoints()
            {
                ClassicAssert.IsFalse(new DataPoint(double.NaN, double.NaN).IsDefined());
                ClassicAssert.IsFalse(new DataPoint(double.NaN, 2).IsDefined());
                ClassicAssert.IsFalse(new DataPoint(2, double.NaN).IsDefined());
                var p = DataPoint.Undefined;
                ClassicAssert.IsFalse(p.IsDefined());
            }
        }
    }
}
