using DesignCrowdTechnicalChallenge.PublicHolidayRules;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignCrowdTechnicalChallengeTests.PublicHolidayRules
{
    internal class NthDayOfMonthPublicHolidayTests
    {
        [Test]
        public void NthDayOfTheMonthPublicHoliday_SetsDateToNthOccurence()
        {
            // Arrange
            var expectedDate = new DateTime(2023, 06, 12);

            // Act
            var publicHoliday = new NthDayOfMonthPublicHoliday("Queens Birthday", DayOfWeek.Monday, 2, 06, 2023);

            // Assert
            Assert.That(publicHoliday.Date, Is.EqualTo(expectedDate));
        }

        [Test]
        public void NthDayOfTheMonthPublicHoliday_WhenNthOccurenceDoesNotExist_ThrowsException()
        {
            // Arrange
            var nthOccurence = 20;

            // Act, Assert
            Assert.Throws<ArgumentException>(() => new NthDayOfMonthPublicHoliday("APublicHoliday", DayOfWeek.Monday, nthOccurence, 06, 2023));
        }
    }
}
