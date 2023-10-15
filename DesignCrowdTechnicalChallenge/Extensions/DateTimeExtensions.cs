using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignCrowdTechnicalChallenge.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsWeekendDay(this DateTime dateTime) => dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday;
    }
}
