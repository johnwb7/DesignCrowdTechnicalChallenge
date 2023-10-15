using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignCrowdTechnicalChallenge.PublicHolidayRules
{
    public class NthDayOfMonthPublicHoliday : PublicHoliday
    {
        public NthDayOfMonthPublicHoliday(string name, DayOfWeek dayOfWeek, int nthOccurence, int month, int year) : base(name, CalculateNthDayOfTheMonth(dayOfWeek, nthOccurence, month, year))
        {

        }

        private static DateTime CalculateNthDayOfTheMonth(DayOfWeek dayOfWeek, int nthOccurence, int month, int year)
        {
            var firstOfMonth = new DateTime(year, month, 1);
            var daysUntilFirstOccurence = (dayOfWeek < firstOfMonth.DayOfWeek ? 7 : 0) + dayOfWeek - firstOfMonth.DayOfWeek;
            var currentOccurence = firstOfMonth.AddDays(daysUntilFirstOccurence);
            
            var occurences = 1;

            while(occurences < nthOccurence)
            {
                currentOccurence = currentOccurence.AddDays(7);
                if (currentOccurence.Month != month) throw new ArgumentException($"nth occurence {nthOccurence} of {dayOfWeek} does not exist in month {month} year {year}");
                occurences++;
            }

            return currentOccurence;
        }
    }
}
