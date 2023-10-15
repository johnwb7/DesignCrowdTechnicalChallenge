using DesignCrowdTechnicalChallenge;
using NUnit.Framework;
using System;

namespace DesignCrowdTechnicalChallengeTests
{
    public class BusinessDayCounterTests
    {
        private BusinessDayCounter _sut;

        private static readonly object[][] WeekdaysBetweenTwoDatesTestCases = {
            new object[] { new DateTime(2013, 10, 7), new DateTime(2013, 10, 9), 1}, // {firstDate, secondDate, expectedResult}
            new object[] { new DateTime(2013, 10, 5), new DateTime(2013, 10, 14), 5},
            new object[] { new DateTime(2013, 10, 7), new DateTime(2014, 1, 1), 61},
            new object[] { new DateTime(2023, 09, 06), new DateTime(2023, 09, 14), 5},
            new object[] { new DateTime(2023, 10, 12), new DateTime(2023, 10, 25), 8},
        };

        [SetUp]
        public void Setup()
        {
            _sut = new BusinessDayCounter();
        }

        [TestCaseSource(nameof(WeekdaysBetweenTwoDatesTestCases))]
        public void WeekdaysBetweenTwoDates_CalculatesNumberOfWeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, int expectedResult)
        {
            // Act
            var result = _sut.WeekdaysBetweenTwoDates(firstDate, secondDate);

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void WeekdaysBetweenTwoDates_WhenFirstDateAndSecondDateAreTheSameWeekday_CalculatesNumberOfWeekdaysbetweenTwoDates()
        {
            // Arrange
            var firstDate = new DateTime(2023, 09, 14);
            var secondDate = new DateTime(2023, 09, 28);

            // Act
            var result = _sut.WeekdaysBetweenTwoDates(firstDate, secondDate);

            // Assert
            Assert.That(result, Is.EqualTo(9));
        }

        [Test]
        public void WeekdaysBetweenTwoDates_WhenSecondDateIsAWeekend_CalculatesNumberOfWeekdaysbetweenTwoDates()
        {
            // Arrange
            var firstDate = new DateTime(2013, 1, 24);
            var secondDate = new DateTime(2013, 2, 10);

            // Act
            var result = _sut.WeekdaysBetweenTwoDates(firstDate, secondDate);

            // Assert
            Assert.That(result, Is.EqualTo(11));
        }

        [Test]
        public void WeekdaysBetweenTwoDates_WhenFirstDateIsEqualToSecondDate_ReturnsZero()
        {
            // Arrange
            var date = new DateTime(2015, 4, 6);

            // Act
            var result = _sut.WeekdaysBetweenTwoDates(date, date);

            // Assert
            Assert.That(result, Is.Zero);
        }

        [Test]
        public void WeekdaysBetweenTwoDates_WhenSecondDateIsOlderThanFirstDate_ReturnsZero()
        {
            // Arrange
            var firstDate = new DateTime(2015, 4, 6);
            var secondDate = new DateTime(2015, 3, 23);

            // Act
            var result = _sut.WeekdaysBetweenTwoDates(firstDate, secondDate);

            // Assert
            Assert.That(result, Is.Zero);
        }

        [Test]
        public void WeekdaysBetweenTwoDates_WhenItIsALeapYear_ShouldIncludeFeb29thInResult()
        {
            // Arrange
            var firstDate = new DateTime(2016, 02, 24);
            var secondDate = new DateTime(2016, 03, 02);

            // Act
            var result = _sut.WeekdaysBetweenTwoDates(firstDate, secondDate);

            // Assert
            Assert.That(result, Is.EqualTo(4));
        }
    }
}