// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeAxisUtilities.cs" company="OxyPlot">
//   Copyright (c) 2014 OxyPlot contributors
// </copyright>
// <summary>
//   These are the utilities to help with the date/time axis
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using OxyPlot.Axes;
    using static OxyPlot.Axes.DateTimeAxis;

    /// <summary>
    /// These are utilities to help with the date/time axis
    /// </summary>
    internal static class DateTimeAxisUtilities
    {
        /// <summary>
        /// Get time span to next occurrence of a month
        /// </summary>
        /// <param name="startTime">The start time</param>
        /// <param name="monthNumber">The month number</param>
        /// <returns>The time span to the given month</returns>
        public static TimeSpan TimeToNextOccurrenceOfMonth(this DateTime startTime, int monthNumber)
        {
            var targetDate = new DateTime(startTime.Year + (startTime.Month > monthNumber ? 1 : 0), monthNumber, 1);
            return targetDate - startTime;
        }

        /// <summary>
        /// Add months to a date
        /// </summary>
        /// <param name="startTime">The start date</param>
        /// <param name="numMonths">The months to add</param>
        /// <returns>Start date plus number of months</returns>
        public static DateTime AddMonths(this DateTime startTime, int numMonths)
        {
            int targetYear = startTime.Year + ((int)(numMonths / 12));
            int targetMonth = startTime.Month + (numMonths % 12);
            if (targetMonth > 12)
            {
                targetYear++;
                targetMonth -= 12;
            }

            return new DateTime(targetYear, targetMonth, startTime.Day, startTime.Hour, startTime.Minute, startTime.Second, startTime.Kind);
        }

        /// <summary>
        /// Get the closest nice interval given a time span
        /// </summary>
        /// <param name="startTime">The start date time</param>
        /// <param name="t">The time span</param>
        /// <param name="niceIntervals">A list of nice intervals</param>
        /// <param name="timeField">A function that gets the time field</param>
        /// <param name="secondsPerUnit">The number of seconds per unit</param>
        /// <param name="margin">The margin</param>
        /// <returns>The closest nice interval</returns>
        public static TimeSpan GetToClosestNiceInterval(this DateTime startTime, TimeSpan t, int[] niceIntervals, Func<DateTime, int> timeField, double secondsPerUnit, double margin)
        {
            TimeSpan diff = TimeSpan.FromSeconds(0);
            for (int i = niceIntervals.Length - 1; i >= 0; i--)
            {
                diff = TimeSpan.FromSeconds((((((int)(timeField(startTime) / niceIntervals[i])) + 1) * niceIntervals[i]) - timeField(startTime)) * secondsPerUnit);
                if (diff.TotalSeconds < (t.TotalSeconds * margin))
                {
                    break;
                }
            }

            return diff;
        }

        /// <summary>
        /// Get the next day
        /// </summary>
        /// <param name="startTime">After this date time</param>
        /// <returns>The next day after the passed date time</returns>
        public static DateTime NextDay(this DateTime startTime)
        {
            return startTime.Date + TimeSpan.FromDays(1);
        }

        /// <summary>
        /// Get a nice next day
        /// </summary>
        /// <param name="startTime">The start time</param>
        /// <param name="niceDayNumbers">The list of nice days</param>
        /// <returns>A date time with a nice day</returns>
        public static DateTime NextNiceDay(this DateTime startTime, int[] niceDayNumbers)
        {
            if (startTime.Day < niceDayNumbers[1])
            {
                return new DateTime(startTime.Year, startTime.Month, niceDayNumbers[1], 0, 0, 0, DateTimeKind.Local);
            }
            else
            {
                return startTime.FirstOfFollowingMonth();
            }
        }

        /// <summary>
        /// Get the first of the following month
        /// </summary>
        /// <param name="startTime">The date time with this month</param>
        /// <returns>The date time of the first of the month</returns>
        public static DateTime FirstOfFollowingMonth(this DateTime startTime)
        {
            if (startTime.Month < 12)
            {
                return new DateTime(startTime.Year, startTime.Month + 1, 1, 0, 0, 0, DateTimeKind.Local);
            }

            return startTime.FirstOfFollowingYear();
        }

        /// <summary>
        /// Get the number of days in the date's month
        /// </summary>
        /// <param name="date">The date with the month to check</param>
        /// <returns>Number of days in month</returns>
        public static int NumberOfDaysInMonth(this DateTime date)
        {
            DateTime lastDayOfMonth = date.FirstOfFollowingMonth().Subtract(TimeSpan.FromDays(1));
            return lastDayOfMonth.Day;
        }

        /// <summary>
        /// Get the first of the following year
        /// </summary>
        /// <param name="startTime">The date time with this year</param>
        /// <returns>The date time of the first of the year</returns>
        public static DateTime FirstOfFollowingYear(this DateTime startTime)
        {
            return new DateTime(startTime.Year + 1, 1, 1, 0, 0, 0, DateTimeKind.Local);
        }

        /// <summary>
        /// Pick a nice interval
        /// </summary>
        /// <param name="startingTick">Starting date time</param>
        /// <param name="endTime">End date time</param>
        /// <param name="lowerLimit">Lower limit</param>
        /// <param name="upperLimit">Upper Limit</param>
        /// <param name="numLabels">Number of Labels</param>
        /// <param name="secondsPerUnit">Seconds per Unit</param>
        /// <returns>A nice interval for the date time axis</returns>
        public static TimeSpan PickNiceInterval(DateTime startingTick, DateTime endTime, double lowerLimit, double upperLimit, int numLabels, double secondsPerUnit)
        {
            var upperLimit_s = upperLimit * secondsPerUnit;
            var lowerLimit_s = lowerLimit * secondsPerUnit;
            var labelsWithLL_s = (int)((endTime - startingTick).TotalSeconds / lowerLimit_s) + 1;
            var labelsWithUL_s = (int)((endTime - startingTick).TotalSeconds / upperLimit_s) + 1;
            var intervalToUse_s = lowerLimit_s;
            if (labelsWithLL_s > numLabels || labelsWithUL_s == numLabels)
            {
                intervalToUse_s = upperLimit_s;
            }

            return TimeSpan.FromSeconds(intervalToUse_s);
        }
    }
}
