using DesignCrowdTechnicalChallenge.Extensions;

namespace DesignCrowdTechnicalChallenge.PublicHolidayRules
{
    public class NextWeekdayPublicHoliday : PublicHoliday
    {
        public NextWeekdayPublicHoliday(string name, DateTime occasionDate) : base(name, CalculateNextWorkingDay(occasionDate))
        {
            
        }

        private static DateTime CalculateNextWorkingDay(DateTime occasionDate)
        {
            if (!occasionDate.IsWeekendDay()) return occasionDate;
            if (occasionDate.DayOfWeek == DayOfWeek.Saturday) return occasionDate.AddDays(2);
            return occasionDate.AddDays(1);
        }
    }
}
