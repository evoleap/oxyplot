// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringHelperTests.cs" company="OxyPlot">
//   Copyright (c) 2014 OxyPlot contributors
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using NUnit.Framework;
    using NUnit.Framework.Legacy;

    // ReSharper disable InconsistentNaming
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    [TestFixture]
    public class StringHelperTests
    {
        [Test]
        public void Format_StandardFormatString()
        {
            ClassicAssert.AreEqual("3.1", StringHelper.Format(CultureInfo.InvariantCulture, "{0}", null, 3.1));
            ClassicAssert.AreEqual("3.14", StringHelper.Format(CultureInfo.InvariantCulture, "{0:0.00}", null, Math.PI));
            ClassicAssert.AreEqual("PI=3.14", StringHelper.Format(CultureInfo.InvariantCulture, "PI={0:0.00}", null, Math.PI));
        }

        [Test]
        public void Format_Item()
        {
            var item = new Item { Text = "Hello World", Value = 3.14 };
            ClassicAssert.AreEqual(
                "3.14 3 Hello World 3.140",
                StringHelper.Format(CultureInfo.InvariantCulture, "{0} {1:0} {Text} {Value:0.000}", item, item.Value, item.Value));
            ClassicAssert.AreEqual(
                "Hello World",
                StringHelper.Format(CultureInfo.InvariantCulture, "{Text}", item));
        }

        public class Item
        {
            public string Text { get; set; }

            public double Value { get; set; }
        }
    }
}
