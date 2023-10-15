using DesignCrowdTechnicalChallenge.PublicHolidayRules;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignCrowdTechnicalChallengeTests.PublicHolidayRules
{
    internal class NextWeekdayPublicHolidayTests
    {
        private static readonly DateTime[][] WeekendDayTestCases =
        {
            new [] { new DateTime(2023, 10, 14), new DateTime(2023, 10, 16)},
            new [] { new DateTime(2023, 10, 15), new DateTime(2023, 10, 16)}
        };

        [TestCaseSource(nameof(WeekendDayTestCases))]
        public void NextWeekdayPublicHoliday_WhenDateIsAWeekendDay_SetsDateToNextWeekday(DateTime occurenceDate, DateTime expectedDate)
        {
            // Arrange
            var name = "Some Public Holiday";

            // Act
            var publicHoliday = new NextWeekdayPublicHoliday(name, occurenceDate);

            // Assert
            Assert.That(publicHoliday.Date, Is.EqualTo(expectedDate));
            Assert.That(publicHoliday.Date.DayOfWeek, Is.EqualTo(DayOfWeek.Monday));
        }

        [Test]
        public void NextWeekdayPublicHoliday_WhenDateIsAWeekday_DoesNotModifyDate()
        {
            // Arrange
            var name = "Some Public Holiday";
            var date = new DateTime(2023, 10, 16);

            // Act
            var publicHoliday = new NextWeekdayPublicHoliday(name, date);

            // Assert
            Assert.That(publicHoliday.Date, Is.EqualTo(date));
        }
    }
}
