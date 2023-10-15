using DesignCrowdTechnicalChallenge.PublicHolidayRules;
using NUnit.Framework;
using System;

namespace DesignCrowdTechnicalChallengeTests.PublicHolidayRules
{
    internal class FixedDatePublicHolidayTests
    {
        [Test]
        public void FixedPublicHoliday_CreatesWithCorrectDate()
        {
            // Arrange
            var name = "Christmas Day";
            var date = new DateTime(2022, 12, 25);

            // Act
            var publicHoliday = new FixedDatePublicHoliday(name, date);

            // Assert
            Assert.That(publicHoliday.Date, Is.EqualTo(date));
        }
    }
}
