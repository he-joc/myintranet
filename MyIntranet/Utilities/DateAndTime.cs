using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyIntranet.Utilities
{
    public class DateAndTime
    {
        public static decimal CalculateWorkingDays(DateTime dtmStart, DateTime dtmEnd, string startHalf, string endHalf)
        {
            if (dtmStart > dtmEnd)
            {
                DateTime temp = dtmStart;
                dtmStart = dtmEnd;
                dtmEnd = temp;
            }

            /* Move border dates to the monday of the first full week and sunday of the last week */
            DateTime startMonday = dtmStart;
            int startDays = 1;
            while (startMonday.DayOfWeek != DayOfWeek.Monday)
            {
                if (startMonday.DayOfWeek != DayOfWeek.Saturday && startMonday.DayOfWeek != DayOfWeek.Sunday)
                {
                    startDays++;
                }
                startMonday = startMonday.AddDays(1);
            }

            DateTime endSunday = dtmEnd;
            int endDays = 0;
            while (endSunday.DayOfWeek != DayOfWeek.Sunday)
            {
                if (endSunday.DayOfWeek != DayOfWeek.Saturday && endSunday.DayOfWeek != DayOfWeek.Sunday)
                {
                    endDays++;
                }
                endSunday = endSunday.AddDays(1);
            }

            double weekDays;

            /* calculate weeks between full week border dates and fix the offset created by moving the border dates */
            weekDays = (Math.Max(0, (int)Math.Ceiling((endSunday - startMonday).TotalDays + 1)) / 7 * 5) + startDays - endDays;

            if (dtmEnd.DayOfWeek == DayOfWeek.Saturday || dtmEnd.DayOfWeek == DayOfWeek.Sunday)
            {
                weekDays -= 1;
            }

            var db = new MyIntranet.Models.MyIntranetEntities();
         
            //Take out the bank holidays
            for (DateTime bhCounter = dtmStart; bhCounter <= dtmEnd; bhCounter = bhCounter.AddDays(1))
            {
                var bhCheck = (from b in db.OfficeClosures where bhCounter == b.Date select b).Any();
                if (bhCheck) weekDays--;
            }

            if ((startHalf == "AM") && (endHalf == "AM")) weekDays -= 0.5;
            if ((startHalf == "PM") && (endHalf == "PM")) weekDays -= 0.5;
            if ((startHalf == "PM") && (endHalf == "AM"))
            {
                if (weekDays > 1) weekDays -= 0.5;
            }

            return (decimal)weekDays;

        }
    }
}