using DesignCrowdTechnicalChallenge.Extensions;
using NUnit.Framework;
using System;

namespace DesignCrowdTechnicalChallengeTests.Extensions
{
    internal class DateTimeExtensionsTests
    {
        private static readonly DateTime[] WeekendDays =
        {
            new DateTime(2023, 10, 14), // Sat
            new DateTime(2023, 10, 15), // Sun
        };

        private static readonly DateTime[] Weekdays =
        {
            new DateTime(2023, 10, 9), // Mon
            new DateTime(2023, 10, 10), // Tue
            new DateTime(2023, 10, 11), // Wed
            new DateTime(2023, 10, 12), // Thur
            new DateTime(2023, 10, 13), // Fri
        };

        [TestCaseSource(nameof(WeekendDays))]
        public void IsWeekendDay_WhenWeekendDay_ReturnsTrue(DateTime dateTime)
        {
            // Act
            var result = dateTime.IsWeekendDay();

            // Assert
            Assert.That(result, Is.True);
        }

        [TestCaseSource(nameof(Weekdays))]
        public void IsWeekendDay_WhenWeekday_ReturnsFalse(DateTime dateTime)
        {
            // Act
            var result = dateTime.IsWeekendDay();

            // Assert
            Assert.That(result, Is.False);
        }
    }
}
