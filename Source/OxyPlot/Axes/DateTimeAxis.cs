// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeAxis.cs" company="OxyPlot">
//   Copyright (c) 2014 OxyPlot contributors
// </copyright>
// <summary>
//   Represents an axis presenting <see cref="System.DateTime" /> values.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot.Axes
{
    using OxyPlot.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// Represents an axis presenting <see cref="System.DateTime" /> values.
    /// </summary>
    /// <remarks>The actual numeric values on the axis are days since 1900/01/01.
    /// Use the static ToDouble and ToDateTime to convert numeric values to and from DateTimes.
    /// The StringFormat value can be used to force formatting of the axis values
    /// <code>"yyyy-MM-dd"</code> shows date
    /// <code>"w"</code> or <code>"ww"</code> shows week number
    /// <code>"h:mm"</code> shows hours and minutes</remarks>
    public class DateTimeAxis : LinearAxis
    {
        /// <summary>
        /// The time origin.
        /// </summary>
        /// <remarks>This gives the same numeric date values as Excel</remarks>
        private static readonly DateTime TimeOrigin = new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// The maximum day value
        /// </summary>
        private static readonly double MaxDayValue = (DateTime.MaxValue - TimeOrigin).TotalDays;

        /// <summary>
        /// The minimum day value
        /// </summary>
        private static readonly double MinDayValue = (DateTime.MinValue - TimeOrigin).TotalDays;

        /// <summary>
        /// The actual interval type.
        /// </summary>
        private DateTimeIntervalType actualIntervalType;

        /// <summary>
        /// The actual minor interval type.
        /// </summary>
        private DateTimeIntervalType actualMinorIntervalType;

        /// <summary>
        /// Initializes a new instance of the <see cref = "DateTimeAxis" /> class.
        /// </summary>
        public DateTimeAxis()
        {
            this.Position = AxisPosition.Bottom;
            this.IntervalType = DateTimeIntervalType.Auto;
            this.FirstDayOfWeek = DayOfWeek.Monday;
            this.CalendarWeekRule = CalendarWeekRule.FirstFourDayWeek;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeAxis" /> class.
        /// </summary>
        /// <param name="position">The position of the axis.</param>
        /// <param name="title">The axis title.</param>
        /// <param name="format">The string format for the axis values.</param>
        /// <param name="intervalType">The interval type.</param>
        [Obsolete]
        public DateTimeAxis(
            AxisPosition position,
            string title = null,
            string format = null,
            DateTimeIntervalType intervalType = DateTimeIntervalType.Auto)
            : base(position, title)
        {
            this.FirstDayOfWeek = DayOfWeek.Monday;
            this.CalendarWeekRule = CalendarWeekRule.FirstFourDayWeek;

            this.StringFormat = format;
            this.IntervalType = intervalType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeAxis" /> class.
        /// </summary>
        /// <param name="position">The position of the axis.</param>
        /// <param name="firstDateTime">The first date/time on the axis.</param>
        /// <param name="lastDateTime">The last date/time on the axis.</param>
        /// <param name="title">The axis title.</param>
        /// <param name="format">The string format for the axis values.</param>
        /// <param name="intervalType">The interval type.</param>
        [Obsolete]
        public DateTimeAxis(
            AxisPosition position,
            DateTime firstDateTime,
            DateTime lastDateTime,
            string title = null,
            string format = null,
            DateTimeIntervalType intervalType = DateTimeIntervalType.Auto)
            : this(position, title, format, intervalType)
        {
            this.Minimum = ToDouble(firstDateTime);
            this.Maximum = ToDouble(lastDateTime);
        }

        /// <summary>
        /// Gets or sets CalendarWeekRule.
        /// </summary>
        public CalendarWeekRule CalendarWeekRule { get; set; }

        /// <summary>
        /// Gets or sets FirstDayOfWeek.
        /// </summary>
        public DayOfWeek FirstDayOfWeek { get; set; }

        /// <summary>
        /// Gets or sets IntervalType.
        /// </summary>
        public DateTimeIntervalType IntervalType { get; set; }

        /// <summary>
        /// Gets or sets MinorIntervalType.
        /// </summary>
        public DateTimeIntervalType MinorIntervalType { get; set; }

        /// <summary>
        /// Gets or sets the time zone (used when formatting date/time values).
        /// </summary>
        /// <value>The time zone info.</value>
        /// <remarks>No date/time conversion will be performed if this property is <c>null</c>.</remarks>
        public TimeZoneInfo TimeZone { get; set; }

        /// <summary>
        /// Creates a data point.
        /// </summary>
        /// <param name="x">The x value.</param>
        /// <param name="y">The y value.</param>
        /// <returns>A data point.</returns>
        public static DataPoint CreateDataPoint(DateTime x, double y)
        {
            return new DataPoint(ToDouble(x), y);
        }

        /// <summary>
        /// Creates a data point.
        /// </summary>
        /// <param name="x">The x value.</param>
        /// <param name="y">The y value.</param>
        /// <returns>A data point.</returns>
        public static DataPoint CreateDataPoint(DateTime x, DateTime y)
        {
            return new DataPoint(ToDouble(x), ToDouble(y));
        }

        /// <summary>
        /// Creates a data point.
        /// </summary>
        /// <param name="x">The x value.</param>
        /// <param name="y">The y value.</param>
        /// <returns>A data point.</returns>
        public static DataPoint CreateDataPoint(double x, DateTime y)
        {
            return new DataPoint(x, ToDouble(y));
        }

        /// <summary>
        /// Converts a numeric representation of the date (number of days after the time origin) to a DateTime structure.
        /// </summary>
        /// <param name="value">The number of days after the time origin.</param>
        /// <returns>A <see cref="DateTime" /> structure. Ticks = 0 if the value is invalid.</returns>
        public static DateTime ToDateTime(double value)
        {
            if (double.IsNaN(value) || value < MinDayValue || value > MaxDayValue)
            {
                return new DateTime();
            }

            return TimeOrigin.AddDays(value - 1);
        }

        /// <summary>
        /// Converts a DateTime to days after the time origin.
        /// </summary>
        /// <param name="value">The date/time structure.</param>
        /// <returns>The number of days after the time origin.</returns>
        public static double ToDouble(DateTime value)
        {
            var span = value - TimeOrigin;
            return span.TotalDays + 1;
        }

        /// <summary>
        /// Gets the tick values.
        /// </summary>
        /// <param name="majorLabelValues">The major label values.</param>
        /// <param name="majorTickValues">The major tick values.</param>
        /// <param name="minorTickValues">The minor tick values.</param>
        public override void GetTickValues(
            out IList<double> majorLabelValues, out IList<double> majorTickValues, out IList<double> minorTickValues)
        {
            minorTickValues = this.CreateDateTimeTickValues(
                this.ActualMinimum, this.ActualMaximum, this.ActualMinorStep, this.actualMinorIntervalType);
            majorTickValues = this.CreateDateTimeTickValues(
                this.ActualMinimum, this.ActualMaximum, this.ActualMajorStep, this.actualIntervalType);
            majorLabelValues = majorTickValues;
        }

        /// <summary>
        /// Gets the value from an axis coordinate, converts from double to the correct data type if necessary.
        /// e.g. DateTimeAxis returns the DateTime and CategoryAxis returns category strings.
        /// </summary>
        /// <param name="x">The coordinate.</param>
        /// <returns>The value.</returns>
        public override object GetValue(double x)
        {
            var time = ToDateTime(x);

            if (this.TimeZone != null)
            {
                time = TimeZoneInfo.ConvertTime(time, this.TimeZone);
            }

            return time;
        }

        /// <summary>
        /// Updates the intervals.
        /// </summary>
        /// <param name="plotArea">The plot area.</param>
        internal override void UpdateIntervals(OxyRect plotArea)
        {
            base.UpdateIntervals(plotArea);
            switch (this.actualIntervalType)
            {
                case DateTimeIntervalType.Years:
                    this.ActualMinorStep = 31;
                    this.actualMinorIntervalType = DateTimeIntervalType.Years;
                    if (this.ActualStringFormat == null)
                    {
                        this.ActualStringFormat = "yyyy";
                    }

                    break;
                case DateTimeIntervalType.Months:
                    this.actualMinorIntervalType = DateTimeIntervalType.Months;
                    if (this.ActualStringFormat == null)
                    {
                        this.ActualStringFormat = "yyyy-MM-dd";
                    }

                    break;
                case DateTimeIntervalType.Weeks:
                    this.actualMinorIntervalType = DateTimeIntervalType.Days;
                    this.ActualMajorStep = 7;
                    this.ActualMinorStep = 1;
                    if (this.ActualStringFormat == null)
                    {
                        this.ActualStringFormat = "yyyy/ww";
                    }

                    break;
                case DateTimeIntervalType.Days:
                    this.ActualMinorStep = this.ActualMajorStep;
                    if (this.ActualStringFormat == null)
                    {
                        this.ActualStringFormat = "yyyy-MM-dd";
                    }

                    break;
                case DateTimeIntervalType.Hours:
                    this.ActualMinorStep = this.ActualMajorStep;
                    if (this.ActualStringFormat == null)
                    {
                        this.ActualStringFormat = "HH:mm";
                    }

                    break;
                case DateTimeIntervalType.Minutes:
                    this.ActualMinorStep = this.ActualMajorStep;
                    if (this.ActualStringFormat == null)
                    {
                        this.ActualStringFormat = "HH:mm";
                    }

                    break;
                case DateTimeIntervalType.Seconds:
                    this.ActualMinorStep = this.ActualMajorStep;
                    if (this.ActualStringFormat == null)
                    {
                        this.ActualStringFormat = "HH:mm:ss";
                    }

                    break;
                case DateTimeIntervalType.Manual:
                    break;
                case DateTimeIntervalType.Auto:
                    break;
            }
        }

        /// <summary>
        /// Formats the value to be used on the axis.
        /// </summary>
        /// <param name="x">The value to format.</param>
        /// <returns>The formatted value.</returns>
        protected override string FormatValueOverride(double x)
        {
            // convert the double value to a DateTime
            var time = ToDateTime(x);

            // If a time zone is specified, convert the time
            if (this.TimeZone != null)
            {
                time = TimeZoneInfo.ConvertTime(time, this.TimeZone);
            }

            string fmt = this.ActualStringFormat;
            if (fmt == null)
            {
                return time.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);
            }

            int week = this.GetWeek(time);
            fmt = fmt.Replace("ww", week.ToString("00"));
            fmt = fmt.Replace("w", week.ToString(CultureInfo.InvariantCulture));
            fmt = string.Concat("{0:", fmt, "}");
            return string.Format(this.ActualCulture, fmt, time);
        }

        private static int[] _niceMillisecondIntervals = new[] { 1, 5, 10, 50, 100, 250, 500, 1000 };
        private static int[] _niceSecondIntervals = new[] { 1, 2, 5, 10, 15, 30, 60 };
        private static int[] _niceSecondNumbers = new[] { 5, 10, 15, 30, 45, 60 };
        private static int[] _niceMinuteIntervals = new[] { 1, 2, 3, 5, 10, 15, 20, 30, 45, 60 };
        private static int[] _niceMinuteNumbers = new[] { 5, 10, 15, 30, 45, 60 };
        private static double[] _niceHourIntervals = new[] { 1, 1.5, 2, 3, 4, 6, 8, 12 };
        private static int[] _niceHourNumbers = new[] { 0, 3, 6, 9, 12, 15, 18, 21 };
        private static int[] _niceDayNumbers = new[] { 1, 15 };
        private static int[] _niceMonthIntervals = new[] { 1, 3, 6, 12, 18, 24 };
        private static int[] _niceMonthNumbers = new[] { 1, 7, 4, 10 };
        private const double AVERAGE_DAYS_PER_MONTH = 30.4377;
        private const double AVERAGE_DAYS_PER_YEAR = 365.2524;

        /// <summary>
        /// Calculates the actual interval.
        /// </summary>
        /// <param name="availableSize">Size of the available area.</param>
        /// <param name="maxIntervalSize">Maximum length of the intervals.</param>
        /// <returns>The calculate actual interval.</returns>
        protected override double CalculateActualInterval(double availableSize, double maxIntervalSize)
        {
            double factor = 0.5;
            int numLabels = (int)(availableSize / maxIntervalSize);
            double dRange = Math.Abs(this.ActualMinimum - this.ActualMaximum);
            var startTime = ToDateTime(Math.Min(this.ActualMinimum, this.ActualMaximum));
            var endTime = ToDateTime(Math.Max(this.ActualMinimum, this.ActualMaximum));
            DateTime? startingTick = null;
            var range = TimeSpanAxis.ToTimeSpan(dRange);
            double interval = 1.0;
            if (range.TotalDays > 365)
            {

                // Pick the closest 1st of a nice month
                for (int i = 0; i < _niceMonthNumbers.Length; i++)
                    if (startTime.TimeToNextOccurrenceOfMonth(_niceMonthNumbers[i]) < TimeSpan.FromSeconds(range.TotalSeconds * factor))
                    {
                        startingTick = new DateTime(startTime.Year + (startTime.Month > _niceMonthNumbers[i] ? 1 : 0), _niceMonthNumbers[i], 1, 0, 0, 0, DateTimeKind.Local);
                        break;
                    }
                if (!startingTick.HasValue)
                    startingTick = startTime.FirstOfFollowingYear();
                int numMonthsToAdd = (int)((range.TotalDays / AVERAGE_DAYS_PER_YEAR) * 4 + 0.5) * 3;
                // Interval
                interval = TimeSpanAxis.ToDouble(TimeSpan.FromDays(numMonthsToAdd * AVERAGE_DAYS_PER_MONTH));
            }
            else if (range.TotalDays > 28)
            {

                // Pick the closest 1st of a month
                startingTick = startTime.FirstOfFollowingMonth();
                //  var bHalfMonth = false;
                int monthIntTimesTwo = ((int)(range.TotalDays / AVERAGE_DAYS_PER_MONTH * 2.0)) + 1;
                //if (monthIntTimesTwo % 2 == 1)
                //    bHalfMonth = true;
                double daysToAdd = monthIntTimesTwo / 2.0 * AVERAGE_DAYS_PER_MONTH;
                // Interval
                interval = TimeSpanAxis.ToDouble(TimeSpan.FromDays(daysToAdd));
            }
            else if (range.TotalDays > 1)
            {

                var nextNiceDay = startTime.NextNiceDay(_niceDayNumbers);
                var timeToNextNiceDay = nextNiceDay - startTime;
                if (timeToNextNiceDay.TotalDays < range.TotalDays * factor)
                {
                    startingTick = nextNiceDay;
                }
                else
                    startingTick = startTime.NextDay();
                // Interval
                interval = TimeSpanAxis.ToDouble(DateTimeAxisUtilities.PickNiceInterval(startingTick.Value, endTime,
                    (int)range.TotalDays, (int)range.TotalDays + 1, numLabels, 86400));
            }
            else if (range.TotalMinutes > 50)
            {

                // return next nice hour
                var closestNiceHour = 0;
                for (int i = 1; i < _niceHourNumbers.Length; i++)
                    if (startTime.Hour < _niceHourNumbers[i])
                    {
                        closestNiceHour = _niceHourNumbers[i];
                        break;
                    }
                DateTime closestNiceTime;
                if (closestNiceHour == 0)
                    closestNiceTime = startTime.NextDay();
                else
                    closestNiceTime = startTime.Date + TimeSpan.FromHours(closestNiceHour);
                if ((closestNiceTime - startTime).TotalHours < range.TotalHours * factor)
                    startingTick = closestNiceTime;
                else
                    startingTick = startTime.Date + TimeSpan.FromHours(startTime.Hour + 1);
                // Interval
                int minutesToAdd = ((int)((range.TotalMinutes / 30.0) + 0.9)) * 30;
                for (int i = 0; i < _niceHourIntervals.Length; i++)
                    if (minutesToAdd / 60.0 <= _niceHourIntervals[i])
                    {
                        minutesToAdd = (int)(_niceHourIntervals[i] * 60);
                        break;
                    }
                interval = TimeSpanAxis.ToDouble(TimeSpan.FromMinutes(minutesToAdd));
            }
            else if (range.TotalSeconds > 50)
            {

                TimeSpan diff = startTime.GetToClosestNiceInterval(range, _niceMinuteNumbers, (st) => st.Minute, 60.0, factor);
                startingTick = startTime + diff - TimeSpan.FromSeconds(startTime.Second + startTime.Millisecond / 1000);
                // Interval
                int secondsToAdd = ((int)((range.TotalSeconds / 30.0) + 1)) * 30;
                for (int i = 0; i < _niceMinuteIntervals.Length; i++)
                    if (secondsToAdd / 60 <= _niceMinuteIntervals[i])
                    {
                        secondsToAdd = (int)(_niceMinuteIntervals[i] * 60);
                        break;
                    }
                interval = TimeSpanAxis.ToDouble(TimeSpan.FromSeconds(secondsToAdd));
            }
            else if (range.TotalSeconds > 1)
            {

                TimeSpan diffSec = startTime.GetToClosestNiceInterval(range, _niceSecondNumbers, (st) => st.Second, 1.0, factor);
                startingTick = startTime + diffSec;
                // Interval
                for (int i = 1; i < _niceSecondIntervals.Length; i++)
                {
                    if (range.TotalSeconds < _niceSecondIntervals[i])
                    {
                        interval = TimeSpanAxis.ToDouble(DateTimeAxisUtilities.PickNiceInterval(startingTick.Value, endTime,
                            _niceSecondIntervals[i - 1], _niceSecondIntervals[i], numLabels, 1));
                        break;
                    }
                }
            }
            else
            {

                TimeSpan diffMicrosec = startTime.GetToClosestNiceInterval(range, _niceMillisecondIntervals, (st) => st.Millisecond, 0.001, factor);
                startingTick = startTime + diffMicrosec;
                // Interval
                for (int i = 1; i < _niceMillisecondIntervals.Length; i++)
                {
                    if (range.TotalMilliseconds < _niceMillisecondIntervals[i])
                    {
                        interval = TimeSpanAxis.ToDouble(DateTimeAxisUtilities.PickNiceInterval(startingTick.Value, endTime,
                            _niceMillisecondIntervals[i - 1], _niceMillisecondIntervals[i], numLabels, 0.001));
                        break;
                    }
                }
            }

            
            this.actualIntervalType = this.IntervalType;
            this.actualMinorIntervalType = this.MinorIntervalType;

            if (this.IntervalType == DateTimeIntervalType.Auto)
            {
                this.actualIntervalType = DateTimeIntervalType.Seconds;
                if (interval >= 1.0 / 24 / 60)
                {
                    this.actualIntervalType = DateTimeIntervalType.Minutes;
                }

                if (interval >= 1.0 / 24)
                {
                    this.actualIntervalType = DateTimeIntervalType.Hours;
                }

                if (interval >= 1)
                {
                    this.actualIntervalType = DateTimeIntervalType.Days;
                }

                if (interval >= AVERAGE_DAYS_PER_MONTH)
                {
                    this.actualIntervalType = DateTimeIntervalType.Months;
                }

                if (dRange >= AVERAGE_DAYS_PER_YEAR)
                {
                    this.actualIntervalType = DateTimeIntervalType.Years;
                }
            }

            if (this.actualMinorIntervalType == DateTimeIntervalType.Auto)
            {
                switch (this.actualIntervalType)
                {
                    case DateTimeIntervalType.Years:
                        this.actualMinorIntervalType = DateTimeIntervalType.Months;
                        break;
                    case DateTimeIntervalType.Months:
                        this.actualMinorIntervalType = DateTimeIntervalType.Days;
                        break;
                    case DateTimeIntervalType.Weeks:
                        this.actualMinorIntervalType = DateTimeIntervalType.Days;
                        break;
                    case DateTimeIntervalType.Days:
                        this.actualMinorIntervalType = DateTimeIntervalType.Hours;
                        break;
                    case DateTimeIntervalType.Hours:
                        this.actualMinorIntervalType = DateTimeIntervalType.Minutes;
                        break;
                    default:
                        this.actualMinorIntervalType = DateTimeIntervalType.Days;
                        break;
                }
            }

            return interval;
        }

        /// <summary>
        /// Creates the date tick values.
        /// </summary>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <param name="step">The step.</param>
        /// <param name="intervalType">Type of the interval.</param>
        /// <returns>Date tick values.</returns>
        private IList<double> CreateDateTickValues(
            double min, double max, double step, DateTimeIntervalType intervalType)
        {
            var values = new Collection<double>();
            double factor = 1.0;
            int numLabels = (int)((max - min) / step);
            double dRange = Math.Abs(max - min);
            var startTime = ToDateTime(Math.Min(this.ActualMinimum, this.ActualMaximum));
            var endTime = ToDateTime(Math.Max(this.ActualMinimum, this.ActualMaximum));
            DateTime? startingTick = null;
            var range = TimeSpanAxis.ToTimeSpan(step);
            double interval = 1.0;
            DateTime nextTick;
            if (range.TotalDays > 365)
            {
                // Pick the closest 1st of a nice month
                for (int i = 0; i < _niceMonthNumbers.Length; i++)
                    if (startTime.TimeToNextOccurrenceOfMonth(_niceMonthNumbers[i]) < TimeSpan.FromSeconds(range.TotalSeconds * factor))
                    {
                        startingTick = new DateTime(startTime.Year + (startTime.Month > _niceMonthNumbers[i] ? 1 : 0), _niceMonthNumbers[i], 1, 0, 0, 0, DateTimeKind.Local);
                        break;
                    }
                if (!startingTick.HasValue)
                    startingTick = startTime.FirstOfFollowingYear();
                nextTick = startingTick.Value;
                values.Add(ToDouble(nextTick));
                // Interval
                int numMonthsToAdd = (int)((range.TotalDays / AVERAGE_DAYS_PER_YEAR) * 4 + 0.5) * 3;
                while ((nextTick = nextTick.AddMonths(numMonthsToAdd)) < endTime)
                    values.Add(ToDouble(nextTick));
                interval = TimeSpanAxis.ToDouble(TimeSpan.FromDays(numMonthsToAdd * AVERAGE_DAYS_PER_MONTH));
            }
            else if (range.TotalDays > 28)
            {

                // Pick the closest 1st of a month
                startingTick = startTime.FirstOfFollowingMonth();
                nextTick = startingTick.Value;
                values.Add(ToDouble(nextTick));

                var bHalfMonth = false;
                int monthIntTimesTwo = ((int)(range.TotalDays / AVERAGE_DAYS_PER_MONTH * 2.0)) + 1;
                if (monthIntTimesTwo % 2 == 1)
                    bHalfMonth = true;
                double daysToAdd = monthIntTimesTwo / 2.0 * AVERAGE_DAYS_PER_MONTH;
                var tsDays = TimeSpan.FromDays(daysToAdd);
                while (true)
                {
                    nextTick = nextTick + tsDays;
                    if (nextTick.Day < 5 || nextTick.Day > 25 || !bHalfMonth)
                    {
                        if (nextTick.Day > 25)
                        {
                            nextTick = nextTick.FirstOfFollowingMonth();
                        }
                        else
                            nextTick = new DateTime(nextTick.Year, nextTick.Month, 1, 0, 0, 0, startTime.Kind);
                    }
                    else
                        nextTick = new DateTime(nextTick.Year, nextTick.Month, 15, 0, 0, 0, startTime.Kind);
                    if (nextTick < endTime)
                        values.Add(ToDouble(nextTick));
                    else
                        break;
                }
            }
            else if (range.TotalDays > 1)
            {

                var nextNiceDay = startTime.NextNiceDay(_niceDayNumbers);
                var timeToNextNiceDay = nextNiceDay - startTime;
                if (timeToNextNiceDay.TotalDays < range.TotalDays * factor)
                {
                    startingTick = nextNiceDay;
                }
                else
                    startingTick = startTime.NextDay();
                nextTick = startingTick.Value;
                values.Add(ToDouble(nextTick));

                // Interval
                var tsInterval = DateTimeAxisUtilities.PickNiceInterval(startingTick.Value, endTime,
                    (int)range.TotalDays, (int)range.TotalDays + 1, numLabels, 86400);
                while ((nextTick = nextTick + tsInterval) < endTime)
                    values.Add(ToDouble(nextTick));
            }
            else if (range.TotalMinutes > 50)
            {

                // return next nice hour
                var closestNiceHour = 0;
                for (int i = 1; i < _niceHourNumbers.Length; i++)
                    if (startTime.Hour < _niceHourNumbers[i])
                    {
                        closestNiceHour = _niceHourNumbers[i];
                        break;
                    }
                DateTime closestNiceTime;
                if (closestNiceHour == 0)
                    closestNiceTime = startTime.NextDay();
                else
                    closestNiceTime = startTime.Date + TimeSpan.FromHours(closestNiceHour);
                if ((closestNiceTime - startTime).TotalHours < range.TotalHours * factor)
                    startingTick = closestNiceTime;
                else
                    startingTick = startTime.Date + TimeSpan.FromHours(startTime.Hour + 1);
                // Interval
                int minutesToAdd = ((int)((range.TotalMinutes / 30.0) + 0.9)) * 30;
                for (int i = 0; i < _niceHourIntervals.Length; i++)
                    if (minutesToAdd / 60.0 <= _niceHourIntervals[i])
                    {
                        minutesToAdd = (int)(_niceHourIntervals[i] * 60);
                        break;
                    }
                nextTick = startingTick.Value;
                values.Add(ToDouble(nextTick));
                while ((nextTick = nextTick + TimeSpan.FromMinutes(minutesToAdd)) < endTime)
                    values.Add(ToDouble(nextTick));
            }
            else if (range.TotalSeconds > 50)
            {

                TimeSpan diff = startTime.GetToClosestNiceInterval(range, _niceMinuteNumbers, (st) => st.Minute, 60.0, factor);
                startingTick = startTime + diff - TimeSpan.FromSeconds(startTime.Second + startTime.Millisecond / 1000);
                // Interval
                int secondsToAdd = ((int)((range.TotalSeconds / 30.0) + 1)) * 30;
                for (int i = 0; i < _niceMinuteIntervals.Length; i++)
                    if (secondsToAdd / 60 <= _niceMinuteIntervals[i])
                    {
                        secondsToAdd = (int)(_niceMinuteIntervals[i] * 60);
                        break;
                    }
                nextTick = startingTick.Value;
                values.Add(ToDouble(nextTick));
                while ((nextTick = nextTick + TimeSpan.FromSeconds(secondsToAdd)) < endTime)
                    values.Add(ToDouble(nextTick));
            }
            else if (range.TotalSeconds > 1)
            {

                TimeSpan diffSec = startTime.GetToClosestNiceInterval(range, _niceSecondNumbers, (st) => st.Second, 1.0, factor);
                startingTick = startTime + diffSec;
                // Interval
                for (int i = 1; i < _niceSecondIntervals.Length; i++)
                {
                    if (range.TotalSeconds < _niceSecondIntervals[i])
                    {
                        interval = TimeSpanAxis.ToDouble(DateTimeAxisUtilities.PickNiceInterval(startingTick.Value, endTime,
                            _niceSecondIntervals[i - 1], _niceSecondIntervals[i], numLabels, 1));
                        break;
                    }
                }
                nextTick = startingTick.Value;
                values.Add(ToDouble(nextTick));
                var tsSec = TimeSpanAxis.ToTimeSpan(interval);
                while ((nextTick = nextTick + tsSec) < endTime)
                    values.Add(ToDouble(nextTick));
            }
            else
            {
                TimeSpan diffMicrosec = startTime.GetToClosestNiceInterval(range, _niceMillisecondIntervals, (st) => st.Millisecond, 0.001, factor);
                startingTick = startTime + diffMicrosec;
                // Interval
                for (int i = 1; i < _niceMillisecondIntervals.Length; i++)
                {
                    if (range.TotalMilliseconds < _niceMillisecondIntervals[i])
                    {
                        interval = TimeSpanAxis.ToDouble(DateTimeAxisUtilities.PickNiceInterval(startingTick.Value, endTime,
                            _niceMillisecondIntervals[i - 1], _niceMillisecondIntervals[i], numLabels, 0.001));
                        break;
                    }
                }
                nextTick = startingTick.Value;
                values.Add(ToDouble(nextTick));
                var diffMilSec = TimeSpanAxis.ToTimeSpan(interval);
                while ((nextTick = nextTick + diffMilSec) < endTime)
                    values.Add(ToDouble(nextTick));
            }

            return values;
        }

        /// <summary>
        /// Creates <see cref="DateTime" /> tick values.
        /// </summary>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <param name="interval">The interval.</param>
        /// <param name="intervalType">The interval type.</param>
        /// <returns>A list of <see cref="DateTime" /> tick values.</returns>
        private IList<double> CreateDateTimeTickValues(
            double min, double max, double interval, DateTimeIntervalType intervalType)
        {
            return this.CreateDateTickValues(min, max, interval, intervalType);
        }

        /// <summary>
        /// Gets the week number for the specified date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>The week number for the current culture.</returns>
        private int GetWeek(DateTime date)
        {
            return this.ActualCulture.Calendar.GetWeekOfYear(date, this.CalendarWeekRule, this.FirstDayOfWeek);
        }
    }
}
