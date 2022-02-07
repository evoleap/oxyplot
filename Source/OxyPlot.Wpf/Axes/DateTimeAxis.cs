// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeAxis.cs" company="OxyPlot">
//   Copyright (c) 2014 OxyPlot contributors
// </copyright>
// <summary>
//   This is a WPF wrapper of OxyPlot.DateTimeAxis.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot.Wpf
{
    using System;
    using System.Globalization;
    using System.Windows;

    using OxyPlot.Axes;

    /// <summary>
    /// This is a WPF wrapper of OxyPlot.DateTimeAxis.
    /// </summary>
    public class DateTimeAxis : Axis
    {
        /// <summary>
        /// Identifies the <see cref="CalendarWeekRule"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty CalendarWeekRuleProperty =
            DependencyProperty.Register(
                "CalendarWeekRule",
                typeof(CalendarWeekRule),
                typeof(DateTimeAxis),
                new PropertyMetadata(CalendarWeekRule.FirstFourDayWeek, DataChanged));

        /// <summary>
        /// Identifies the <see cref="FirstDateTime"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty FirstDateTimeProperty = DependencyProperty.Register(
            "FirstDateTime", typeof(DateTime), typeof(DateTimeAxis), new PropertyMetadata(DateTime.MinValue));

        /// <summary>
        /// Identifies the <see cref="FirstDayOfWeek"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty FirstDayOfWeekProperty = DependencyProperty.Register(
            "FirstDayOfWeek",
            typeof(DayOfWeek),
            typeof(DateTimeAxis),
            new PropertyMetadata(DayOfWeek.Monday, DataChanged));

        /// <summary>
        /// Identifies the <see cref="IntervalType"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IntervalTypeProperty = DependencyProperty.Register(
            "IntervalType",
            typeof(DateTimeIntervalType),
            typeof(DateTimeAxis),
            new PropertyMetadata(DateTimeIntervalType.Auto));

        /// <summary>
        /// Identifies the <see cref="LastDateTime"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty LastDateTimeProperty = DependencyProperty.Register(
            "LastDateTime", typeof(DateTime), typeof(DateTimeAxis), new PropertyMetadata(DateTime.MaxValue));

        /// <summary>
        /// Identifies the <see cref="MinorIntervalType"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty MinorIntervalTypeProperty =
            DependencyProperty.Register(
                "MinorIntervalType",
                typeof(DateTimeIntervalType),
                typeof(DateTimeAxis),
                new PropertyMetadata(DateTimeIntervalType.Auto, DataChanged));

        /// <summary>
        /// Identifies the <see cref="NumberOfLabels"/> dependency property
        /// </summary>
        public static readonly DependencyProperty NumberOfLabelsProperty =
            DependencyProperty.Register(
                "NumberOfLabels",
                typeof(int),
                typeof(DateTimeAxis),
                new PropertyMetadata(0, DataChanged));

        /// <summary>
        /// Identifies the <see cref="TimeZone"/> dependency property
        /// </summary>
        public static readonly DependencyProperty TimeZoneProperty =
            DependencyProperty.Register(
                "TimeZone",
                typeof(TimeZoneInfo),
                typeof(DateTimeAxis),
                new PropertyMetadata(null, DataChanged));

        /// <summary>
        /// Identifies the <see cref="FormatAsFractions"/> dependency property
        /// </summary>
        public static readonly DependencyProperty FormatAsFractionsProperty =
            DependencyProperty.Register(
                "FormatAsFractions",
                typeof(bool),
                typeof(DateTimeAxis),
                new PropertyMetadata(false, DataChanged));

        /// <summary>
        /// Identifies the <see cref="FractionUnit"/> dependency property
        /// </summary>
        public static readonly DependencyProperty FractionUnitProperty =
            DependencyProperty.Register(
                "FractionUnit",
                typeof(double),
                typeof(DateTimeAxis),
                new PropertyMetadata(1.0, DataChanged));

        /// <summary>
        /// Identifies the <see cref="FractionUnitSymbol"/> dependency property
        /// </summary>
        public static readonly DependencyProperty FractionUnitSymbolProperty =
            DependencyProperty.Register(
                "FractionUnitSymbol",
                typeof(string),
                typeof(DateTimeAxis),
                new PropertyMetadata(null, DataChanged));

        /// <summary>
        /// Identifies the <see cref="IsTitleAndLabelsVisible"/> dependency property
        /// </summary>
        public static readonly DependencyProperty IsTitleAndLabelsVisibleProperty =
            DependencyProperty.Register(
                "IsTitleAndLabelsVisible",
                typeof(bool),
                typeof(DateTimeAxis),
                new PropertyMetadata(true, DataChanged));

        /// <summary>
        /// Identifies the <see cref="ShowMinorTicks"/> dependency property
        /// </summary>
        [Obsolete("Set MinorTickSize = 0 instead")]
        public static readonly DependencyProperty ShowMinorTicksProperty =
            DependencyProperty.Register(
                "ShowMinorTicks",
                typeof(bool),
                typeof(DateTimeAxis),
                new PropertyMetadata(true, DataChanged));

        /// <summary>
        /// Identifies the <see cref="TextColor"/> dependency property
        /// </summary>
        public static readonly DependencyProperty TextColorProperty =
            DependencyProperty.Register(
                "TextColor",
                typeof(OxyColor),
                typeof(DateTimeAxis),
                new PropertyMetadata(OxyColors.Automatic, DataChanged));

        /// <summary>
        /// Identifies the <see cref="Selectable"/> dependency property
        /// </summary>
        public static readonly DependencyProperty SelectableProperty =
            DependencyProperty.Register(
                "Selectable",
                typeof(bool),
                typeof(DateTimeAxis),
                new PropertyMetadata(true, DataChanged));

        /// <summary>
        /// Identifies the <see cref="SelectionMode"/> dependency property
        /// </summary>
        public static readonly DependencyProperty SelectionModeProperty =
            DependencyProperty.Register(
                "SelectionMode",
                typeof(SelectionMode),
                typeof(DateTimeAxis),
                new PropertyMetadata(SelectionMode.All, DataChanged));

        /// <summary>
        /// Initializes static members of the <see cref="DateTimeAxis" /> class.
        /// </summary>
        static DateTimeAxis()
        {
            PositionProperty.OverrideMetadata(typeof(DateTimeAxis), new PropertyMetadata(AxisPosition.Bottom, AppearanceChanged));
            IntervalLengthProperty.OverrideMetadata(typeof(DateTimeAxis), new PropertyMetadata(90.0, AppearanceChanged));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "DateTimeAxis" /> class.
        /// </summary>
        public DateTimeAxis()
        {
            this.InternalAxis = new Axes.DateTimeAxis();
        }

        /// <summary>
        /// Gets or sets CalendarWeekRule.
        /// </summary>
        public CalendarWeekRule CalendarWeekRule
        {
            get
            {
                return (CalendarWeekRule)this.GetValue(CalendarWeekRuleProperty);
            }

            set
            {
                this.SetValue(CalendarWeekRuleProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets FirstDateTime.
        /// </summary>
        public DateTime FirstDateTime
        {
            get
            {
                return (DateTime)this.GetValue(FirstDateTimeProperty);
            }

            set
            {
                this.SetValue(FirstDateTimeProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets FirstDayOfWeek.
        /// </summary>
        public DayOfWeek FirstDayOfWeek
        {
            get
            {
                return (DayOfWeek)this.GetValue(FirstDayOfWeekProperty);
            }

            set
            {
                this.SetValue(FirstDayOfWeekProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets IntervalType.
        /// </summary>
        public DateTimeIntervalType IntervalType
        {
            get
            {
                return (DateTimeIntervalType)this.GetValue(IntervalTypeProperty);
            }

            set
            {
                this.SetValue(IntervalTypeProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets LastDateTime.
        /// </summary>
        public DateTime LastDateTime
        {
            get
            {
                return (DateTime)this.GetValue(LastDateTimeProperty);
            }

            set
            {
                this.SetValue(LastDateTimeProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets MinorIntervalType.
        /// </summary>
        public DateTimeIntervalType MinorIntervalType
        {
            get
            {
                return (DateTimeIntervalType)this.GetValue(MinorIntervalTypeProperty);
            }

            set
            {
                this.SetValue(MinorIntervalTypeProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets NumberOfLabels
        /// </summary>
        public int NumberOfLabels
        {
            get
            {
                return (int)this.GetValue(NumberOfLabelsProperty);
            }

            set
            {
                this.SetValue(NumberOfLabelsProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets TimeZone
        /// </summary>
        public TimeZoneInfo TimeZone
        {
            get
            {
                return (TimeZoneInfo)this.GetValue(TimeZoneProperty);
            }

            set
            {
                this.SetValue(TimeZoneProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to FormatAsFractions
        /// </summary>
        public bool FormatAsFractions
        {
            get
            {
                return (bool)this.GetValue(FormatAsFractionsProperty);
            }

            set
            {
                this.SetValue(FormatAsFractionsProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets FractionUnit
        /// </summary>
        public double FractionUnit
        {
            get
            {
                return (double)this.GetValue(FractionUnitProperty);
            }

            set
            {
                this.SetValue(FractionUnitProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets FractionUnitSymbol
        /// </summary>
        public string FractionUnitSymbol
        {
            get
            {
                return (string)this.GetValue(FractionUnitSymbolProperty);
            }

            set
            {
                this.SetValue(FractionUnitSymbolProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show the axis title and labels
        /// </summary>
        public bool IsTitleAndLabelsVisible
        {
            get
            {
                return (bool)this.GetValue(IsTitleAndLabelsVisibleProperty);
            }

            set
            {
                this.SetValue(IsTitleAndLabelsVisibleProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to ShowMinorTicks
        /// </summary>
        [Obsolete("Set MinorTickSize = 0 instead")]
        public bool ShowMinorTicks
        {
            get
            {
                return (bool)this.GetValue(ShowMinorTicksProperty);
            }

            set
            {
                this.SetValue(ShowMinorTicksProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets TextColor
        /// </summary>
        public OxyColor TextColor
        {
            get
            {
                return (OxyColor)this.GetValue(TextColorProperty);
            }

            set
            {
                this.SetValue(TextColorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the axis is Selectable
        /// </summary>
        public bool Selectable
        {
            get
            {
                return (bool)this.GetValue(SelectableProperty);
            }

            set
            {
                this.SetValue(SelectableProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets SelectionMode
        /// </summary>
        public SelectionMode SelectionMode
        {
            get
            {
                return (SelectionMode)this.GetValue(SelectionModeProperty);
            }

            set
            {
                this.SetValue(SelectionModeProperty, value);
            }
        }

        /// <summary>
        /// Creates the internal model.
        /// </summary>
        /// <returns>The internal axis.</returns>
        public override Axes.Axis CreateModel()
        {
            this.SynchronizeProperties();
            return this.InternalAxis;
        }

        /// <summary>
        /// Synchronizes the properties.
        /// </summary>
        protected override void SynchronizeProperties()
        {
            base.SynchronizeProperties();
            var a = (Axes.DateTimeAxis)this.InternalAxis;

            a.IntervalType = this.IntervalType;
            a.MinorIntervalType = this.MinorIntervalType;
            a.FirstDayOfWeek = this.FirstDayOfWeek;
            a.CalendarWeekRule = this.CalendarWeekRule;
            a.NumberOfLabels = this.NumberOfLabels;
            a.TimeZone = this.TimeZone;
            a.FormatAsFractions = this.FormatAsFractions;
            a.FractionUnit = this.FractionUnit;
            a.FractionUnitSymbol = this.FractionUnitSymbol;
            a.IsTitleAndLabelsVisible = this.IsTitleAndLabelsVisible;
#pragma warning disable CS0618 // Type or member is obsolete
            if (!this.ShowMinorTicks)
#pragma warning restore CS0618 // Type or member is obsolete
            {
                this.MinorTickSize = 0;
            }

            a.MinorTickSize = this.MinorTickSize;
            a.TextColor = this.TextColor;
            a.Selectable = this.Selectable;
            a.SelectionMode = this.SelectionMode;

            if (this.FirstDateTime > DateTime.MinValue)
            {
                a.Minimum = Axes.DateTimeAxis.ToDouble(this.FirstDateTime);
            }

            if (this.LastDateTime < DateTime.MaxValue)
            {
                a.Maximum = Axes.DateTimeAxis.ToDouble(this.LastDateTime);
            }
        }
    }
}
