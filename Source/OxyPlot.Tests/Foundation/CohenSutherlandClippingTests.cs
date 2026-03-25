// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CohenSutherlandClippingTests.cs" company="OxyPlot">
//   Copyright (c) 2014 OxyPlot contributors
// </copyright>
// <summary>
//   Provides unit tests for the <see cref="CohenSutherlandClipping" /> class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot.Tests
{
    using NUnit.Framework;
    using NUnit.Framework.Legacy;

    /// <summary>
    /// Provides unit tests for the <see cref="CohenSutherlandClipping" /> class.
    /// </summary>
    [TestFixture]
    public class CohenSutherlandClippingTests
    {
        /// <summary>
        /// Tests the <see cref="CohenSutherlandClipping.IsInside" /> method.
        /// </summary>
        public class IsInside
        {
            /// <summary>
            /// Given a point inside the clipping rectangle, the method returns <c>true</c>.
            /// </summary>
            [Test]
            public void InsidePoint()
            {
                var clipping = new CohenSutherlandClipping(new OxyRect(0, 0, 1, 1));
                ClassicAssert.IsTrue(clipping.IsInside(new ScreenPoint(0.5, 0.5)));
            }

            /// <summary>
            /// Given a point outside the clipping rectangle, the method returns <c>false</c>.
            /// </summary>
            [Test]
            public void OutsidePoint()
            {
                var clipping = new CohenSutherlandClipping(new OxyRect(0, 0, 1, 1));
                ClassicAssert.IsFalse(clipping.IsInside(new ScreenPoint(-0.5, 0.5)));
            }
        }

        /// <summary>
        /// Tests the <see cref="CohenSutherlandClipping.ClipLine(ref ScreenPoint, ref ScreenPoint)" /> method.
        /// </summary>
        public class ClipLine
        {
            /// <summary>
            /// Given a line that crosses the clipping rectangle and the end points are outside the clipping rectangle,
            /// the method returns <c>true</c>.
            /// </summary>
            [Test]
            public void EndpointsOutsideArea()
            {
                var clipping = new CohenSutherlandClipping(new OxyRect(0, 0, 1, 1));
                var p0 = new ScreenPoint(0.3, -0.2);
                var p1 = new ScreenPoint(0.6, 1.3);
                ClassicAssert.IsTrue(clipping.ClipLine(ref p0, ref p1));
                ClassicAssert.AreEqual(0, p0.Y);
                ClassicAssert.AreEqual(1, p1.Y);
            }

            /// <summary>
            /// Given a line that crosses the clipping rectangle and the end points are outside the clipping rectangle,
            /// the method returns <c>true</c>.
            /// </summary>
            [Test]
            public void EndpointsOutsideArea2()
            {
                var clipping = new CohenSutherlandClipping(new OxyRect(0.3, -0.5, 0.5, 1));
                var p0 = new ScreenPoint(0, 0);
                var p1 = new ScreenPoint(1, 0);
                ClassicAssert.IsTrue(clipping.ClipLine(ref p0, ref p1));
                ClassicAssert.AreEqual(new ScreenPoint(0.3, 0), p0);
                ClassicAssert.AreEqual(new ScreenPoint(0.8, 0), p1);
            }

            /// <summary>
            /// Given points inside the clipping rectangle, the method returns <c>true</c>.
            /// </summary>
            [Test]
            public void EndpointsInsideArea()
            {
                var clipping = new CohenSutherlandClipping(new OxyRect(0, 0, 1, 1));
                var p0 = new ScreenPoint(0.3, 0.2);
                var p1 = new ScreenPoint(0.6, 0.8);
                ClassicAssert.IsTrue(clipping.ClipLine(ref p0, ref p1));
                ClassicAssert.AreEqual(0.2, p0.Y);
                ClassicAssert.AreEqual(0.8, p1.Y);
            }

            /// <summary>
            /// Given a line outside the clipping rectangle, the method returns <c>false</c>.
            /// </summary>
            [Test]
            public void LineOutsideArea()
            {
                var clipping = new CohenSutherlandClipping(new OxyRect(0, 0, 1, 1));
                var p0 = new ScreenPoint(0.3, -0.2);
                var p1 = new ScreenPoint(0.6, -0.2);
                ClassicAssert.IsFalse(clipping.ClipLine(ref p0, ref p1));
                ClassicAssert.AreEqual(-0.2, p0.Y);
                ClassicAssert.AreEqual(-0.2, p1.Y);
            }
        }
    }
}
