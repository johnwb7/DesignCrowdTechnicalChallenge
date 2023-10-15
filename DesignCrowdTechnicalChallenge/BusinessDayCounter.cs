using DesignCrowdTechnicalChallenge.Extensions;
using DesignCrowdTechnicalChallenge.PublicHolidayRules;

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

            var firstDateDayOfWeek = ToWeekdayOrFriday(firstDate);
            var secondDateDayOfWeek = ToWeekdayOrFriday(secondDate);
            if (secondDateDayOfWeek <= firstDateDayOfWeek) secondDateDayOfWeek += WeekdaysInWeek;

            var totalWeekdaysFromPartialWeek = secondDate.IsWeekendDay() ? 
                secondDateDayOfWeek - firstDateDayOfWeek :
                secondDateDayOfWeek - firstDateDayOfWeek - 1; // -1 to exclude second date from result
            var totalWeekdays = totalWeekdaysFromFullWeeks + totalWeekdaysFromPartialWeek;
            return totalWeekdays;
        }
        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime>publicHolidays)
        {
            if (secondDate <= firstDate) return 0;

            var relevantPublicHolidays = publicHolidays
                .Where(ph => IsRelevantPublicHoliday(firstDate, secondDate, ph))
                .ToList();

            var totalWeekdays = WeekdaysBetweenTwoDates(firstDate, secondDate);
            var totalBusinessDays = totalWeekdays - relevantPublicHolidays.Count;
            return totalBusinessDays;
        }

        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<PublicHoliday> publicHolidays)
        {
            var publicHolidayDates = publicHolidays.Select(ph => ph.Date).ToList();
            return BusinessDaysBetweenTwoDates(firstDate, secondDate, publicHolidayDates);
        }

        private int ToWeekdayOrFriday(DateTime dateTime) => dateTime.IsWeekendDay() ? (int)DayOfWeek.Friday : (int)dateTime.DayOfWeek;
        private bool IsRelevantPublicHoliday(DateTime firstDate, DateTime secondDate, DateTime publicHoliday) => publicHoliday > firstDate && publicHoliday < secondDate && !publicHoliday.IsWeekendDay();


    }
}
