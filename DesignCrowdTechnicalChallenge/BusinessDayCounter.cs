using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignCrowdTechnicalChallenge
{
    public class BusinessDayCounter
    {
        private const int DaysInWeek = 7;
        private const int WeekdaysInWeek = 5;

        public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            if (secondDate <= firstDate) return 0;

            var totalDays = (int)(secondDate - firstDate).TotalDays - 1; // -1 to exclude first date from result
            var totalFullWeeks = totalDays / DaysInWeek;
            var totalWeekdaysFromFullWeeks = totalFullWeeks * WeekdaysInWeek;

            var firstDateDayOfWeek = GetClosestPriorWeekday(firstDate);
            var secondDateDayOfWeek = GetClosestPriorWeekday(secondDate);
            if (secondDateDayOfWeek <= firstDateDayOfWeek) secondDateDayOfWeek += WeekdaysInWeek;

            var totalWeekdaysFromPartialWeek = IsWeekendDay(secondDate) ? 
                secondDateDayOfWeek - firstDateDayOfWeek :
                secondDateDayOfWeek - firstDateDayOfWeek - 1; // -1 to exclude second date from result
            var totalWeekdays = totalWeekdaysFromFullWeeks + totalWeekdaysFromPartialWeek;
            return totalWeekdays;
        }
        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime>
        publicHolidays)
        {
            //todo
            return -1;
        }

        private int GetClosestPriorWeekday(DateTime dateTime) => IsWeekendDay(dateTime) ? (int)DayOfWeek.Friday : (int)dateTime.DayOfWeek;
        private bool IsWeekendDay(DateTime dateTime) => dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday;

    }
}
