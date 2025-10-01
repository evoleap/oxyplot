// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ListFillerTests.cs" company="OxyPlot">
//   Copyright (c) 2014 OxyPlot contributors
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using NUnit.Framework;
    using NUnit.Framework.Legacy;
    using OxyPlot.Series;

    // ReSharper disable InconsistentNaming
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    [TestFixture]
    public class ListFillerTests
    {
        private IList<TestObject> src;

        [SetUp]
        public void Setup()
        {
            this.src = new List<TestObject> { new TestObject { A = 3.14 } };
        }

        [Test]
        public void Fill()
        {
            var target = new List<ScatterPoint>();

            var filler = new ListFiller<ScatterPoint>();
            filler.Add("A", (p, v) => p.X = Convert.ToDouble(v));
            filler.Fill(target, this.src);

            ClassicAssert.AreEqual(1, target.Count);
            ClassicAssert.AreEqual(3.14, target[0].X);
        }

        [Test]
        public void Fill_InvalidProperty_ThrowsException()
        {
            var target = new List<ScatterPoint>();

            var filler = new ListFiller<ScatterPoint>();
            Assert.That(
                () =>
                {
                    filler.Add("B", (p, v) => p.X = Convert.ToDouble(v));
                    filler.Fill(target, this.src);
                },
                Throws.Exception);
        }

        [Test]
        public void Fill_NullProperty_IsIgnored()
        {
            var target = new List<ScatterPoint>();

            var filler = new ListFiller<ScatterPoint>();
            filler.Add(null, (p, v) => { });
            filler.Fill(target, this.src);

            ClassicAssert.AreEqual(1, target.Count);
            ClassicAssert.AreEqual(0, target[0].X);
        }

        private class TestObject
        {
            public double A { get; set; }
        }
    }
}
