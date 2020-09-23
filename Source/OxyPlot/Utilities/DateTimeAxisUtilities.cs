using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OxyPlot.Utilities
{
    static class DateTimeAxisUtilities
    {
        public static TimeSpan TimeToNextOccurrenceOfMonth(this DateTime startTime, int monthNumber)
        {
            var targetDate = new DateTime(startTime.Year + (startTime.Month > monthNumber ? 1 : 0), monthNumber, 1);
            return targetDate - startTime;
        }
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
        public static TimeSpan GetToClosestNiceInterval(this DateTime startTime, TimeSpan t, int[] niceIntervals, Func<DateTime, int> timeField, double secondsPerUnit, double margin)
        {
            TimeSpan diff = TimeSpan.FromSeconds(0);
            for (int i = niceIntervals.Length - 1; i >= 0; i--)
            {
                diff = TimeSpan.FromSeconds(((((int)(timeField(startTime) / niceIntervals[i])) + 1) * niceIntervals[i] - timeField(startTime)) * secondsPerUnit);
                if (diff.TotalSeconds < (t.TotalSeconds * margin))
                    break;
            }
            return diff;
        }
        public static DateTime NextDay(this DateTime startTime)
        {
            return startTime.Date + TimeSpan.FromDays(1);
        }

        public static DateTime NextNiceDay(this DateTime startTime, int[] niceDayNumbers)
        {
            if (startTime.Day < niceDayNumbers[1])
                return new DateTime(startTime.Year, startTime.Month, niceDayNumbers[1], 0, 0, 0, DateTimeKind.Local);
            else
                return startTime.FirstOfFollowingMonth();
        }

        public static DateTime FirstOfFollowingMonth(this DateTime startTime)
        {
            if (startTime.Month < 12)
            {
                return new DateTime(startTime.Year, startTime.Month + 1, 1, 0, 0, 0, DateTimeKind.Local);
            }
            return startTime.FirstOfFollowingYear();
        }

        public static DateTime FirstOfFollowingYear(this DateTime startTime)
        {
            return new DateTime(startTime.Year + 1, 1, 1, 0, 0, 0, DateTimeKind.Local);
        }
        public static TimeSpan PickNiceInterval(DateTime startingTick, DateTime endTime, double lowerLimit, double upperLimit, int numLabels, double secondsPerUnit)
        {
            var upperLimit_s = upperLimit * secondsPerUnit;
            var lowerLimit_s = lowerLimit * secondsPerUnit;
            var labelsWithLL_s = (int)((endTime - startingTick).TotalSeconds / lowerLimit_s) + 1;
            var labelsWithUL_s = (int)((endTime - startingTick).TotalSeconds / upperLimit_s) + 1;
            var intervalToUse_s = lowerLimit_s;
            if (labelsWithLL_s > numLabels || labelsWithUL_s == numLabels)
                intervalToUse_s = upperLimit_s;
            return TimeSpan.FromSeconds(intervalToUse_s);
        }
    }
}
