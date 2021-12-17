// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeAxisTests.cs" company="OxyPlot">
//   Copyright (c) 2014 OxyPlot contributors
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using NSubstitute;
    using NUnit.Framework;

    using OxyPlot.Axes;
    using OxyPlot.Series;

    // ReSharper disable InconsistentNaming
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    [TestFixture]
    public class DateTimeAxisTests
    {
        [Test]
        public void ToDouble_ValidDate()
        {
            Assert.AreEqual(40616, DateTimeAxis.ToDouble(new DateTime(2011, 3, 15)));
        }

        [Test]
        public void ToDouble_NoDate()
        {
            Assert.AreEqual(-693594, DateTimeAxis.ToDouble(new DateTime()));
        }

        [Test]
        public void ToDateTime_ValidDate()
        {
            Assert.AreEqual(new DateTime(2011, 3, 15), DateTimeAxis.ToDateTime(40616));
        }

        [Test]
        public void ToDateTime_NoDate()
        {
            Assert.AreEqual(new DateTime(), DateTimeAxis.ToDateTime(-693594));
        }

        [Test]
        public void ToDateTime_NaN()
        {
            Assert.AreEqual(new DateTime(), DateTimeAxis.ToDateTime(double.NaN));
        }

        [Test]
        public void ToDateTime_VeryBigValue()
        {
            Assert.AreEqual(new DateTime(), DateTimeAxis.ToDateTime(double.MaxValue));
        }

        [Test]
        public void ToDateTime_VerySmallValue()
        {
            Assert.AreEqual(new DateTime(), DateTimeAxis.ToDateTime(double.MinValue));
        }

        [Test]
        public void TestGetTickValues()
        {
            var rc = Substitute.For<IRenderContext>();
            var plot = new PlotModel { Title = "Backgrounds" };
            ((IPlotModel)plot).Render(rc, 500, 500);

            DateTimeAxis axis = new DateTimeAxis();
            axis.Minimum = DateTimeAxis.ToDouble(new DateTime(2016, 1, 1));
            axis.Maximum = DateTimeAxis.ToDouble(new DateTime(2016, 4, 21));
            axis.MajorStep = double.NaN;
            axis.MinorStep = double.NaN;

            plot.Axes.Add(axis);
                var yaxis1 = new LinearAxis { Position = AxisPosition.Left, Title = "Y1", Key = "Y1", StartPosition = 0, EndPosition = 0.5 };
            plot.Axes.Add(yaxis1);
            axis.Reset();
            ((IPlotModel)plot).Render(rc, 500, 500);

            ((IPlotModel)plot).Update(false);

            IList<double> majorLabelValues;
            IList<double> majorTickValues;
            IList<double> minorTickValues;
            axis.GetTickValues(out majorLabelValues, out majorTickValues, out minorTickValues);
            Assert.AreEqual(7, majorLabelValues.Count);

            Action<LineSeries> addExamplePoints = ls =>
            {
                ls.Points.Add(new DataPoint(axis.Minimum + 3, 13));
                ls.Points.Add(new DataPoint(axis.Minimum + 10, 47));
                ls.Points.Add(new DataPoint(axis.Minimum + 30, 23));
                ls.Points.Add(new DataPoint(axis.Minimum + 40, 65));
                ls.Points.Add(new DataPoint(axis.Minimum + 80, 10));
            };

            var ls1 = new LineSeries { Background = OxyColors.LightSeaGreen, YAxisKey = "Y1" };
            addExamplePoints(ls1);
            plot.Series.Add(ls1);

            ((IPlotModel)plot).Update(true);

            axis.Minimum = 44468.85;
            axis.Maximum = 44476.76;
            axis.Reset();
            axis.MajorStep = 1.01;
            ((IPlotModel)plot).Render(rc, 600, 600);

            // The code will call render in a loop, so this simulates that.
            axis.Render(rc, plot, AxisLayer.BelowSeries, 0);
            axis.Render(rc, plot, AxisLayer.BelowSeries, 1);
            axis.Render(rc, plot, AxisLayer.BelowSeries, 2);

            axis.GetTickValues(out majorLabelValues, out majorTickValues, out minorTickValues);
            Assert.AreEqual(8, majorLabelValues.Count);
        }

        [Test]
        public void GetSetLabelsTickValues()
        {
            var rc = Substitute.For<IRenderContext>();
            var plot = new PlotModel { Title = "Backgrounds" };
            ((IPlotModel)plot).Render(rc, 500, 500);

            DateTimeAxis axis = new DateTimeAxis();

            axis.Minimum = 44468.85;
            axis.Maximum = 44476.76;
            axis.NumberOfLabels = 20;
            axis.Reset();
            plot.Axes.Add(axis);
            ((IPlotModel)plot).Render(rc, 500, 500);

            // The code will call render in a loop, so this simulates that.
            axis.Render(rc, plot, AxisLayer.BelowSeries, 0);
            axis.Render(rc, plot, AxisLayer.BelowSeries, 1);
            axis.Render(rc, plot, AxisLayer.BelowSeries, 2);

            IList<double> majorLabelValues;
            IList<double> majorTickValues;
            IList<double> minorTickValues;

            axis.GetTickValues(out majorLabelValues, out majorTickValues, out minorTickValues);
            Assert.AreEqual(16, majorLabelValues.Count);
        }
    }
}
