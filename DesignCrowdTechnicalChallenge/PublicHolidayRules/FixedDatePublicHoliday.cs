using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignCrowdTechnicalChallenge.PublicHolidayRules
{
    public class FixedDatePublicHoliday : PublicHoliday
    {
        public FixedDatePublicHoliday(string name, DateTime date) : base(name, date)
        {

        }
    }
}
