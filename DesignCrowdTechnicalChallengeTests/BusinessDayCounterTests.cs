using DesignCrowdTechnicalChallenge;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace DesignCrowdTechnicalChallengeTests
{
    public class BusinessDayCounterTests
    {
        private BusinessDayCounter _sut;

        private static readonly object[][] WeekdaysBetweenTwoDatesTestCases = {
            // {firstDate, secondDate, expectedResult}
            new object[] { new DateTime(2013, 10, 7), new DateTime(2013, 10, 9), 1}, 
            new object[] { new DateTime(2013, 10, 5), new DateTime(2013, 10, 14), 5},
            new object[] { new DateTime(2013, 10, 7), new DateTime(2014, 1, 1), 61},
            new object[] { new DateTime(2023, 09, 06), new DateTime(2023, 09, 14), 5},
            new object[] { new DateTime(2023, 10, 12), new DateTime(2023, 10, 25), 8},
        };

        private static readonly object[][] BusinessDaysBetweenTwoDatesTestCases =
        {
            // { firstDate, secondDate, publicHoliday, expectedResult }
            new object[] { new DateTime(2013, 12, 24), new DateTime(2013, 12, 27), PublicHolidays, 0 },
            new object[] { new DateTime(2013, 1, 24), new DateTime(2013, 2, 10), new DateTime[] { new (2013, 1, 28)}, 10 }
        };

        private static readonly DateTime[] PublicHolidays =
        {
            new(2013, 12, 25),
            new(2013, 12, 26),
            new(2014, 1, 1)
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

        [TestCaseSource(nameof(BusinessDaysBetweenTwoDatesTestCases))]
        public void BusinessDaysBetweenTwoDates_WhenThereAreWeekdayPublicHolidaysBetweenTwoDates_ExcludesPublicHolidaysFromNumberOfBusinessDays(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays, int expectedResult)
        {
            // Act
            var result = _sut.BusinessDaysBetweenTwoDates(firstDate, secondDate, publicHolidays);

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void BusinessDaysBetweenTwoDates_WhenNoPublicHolidays_CalculatesNumberOfBusinessDays()
        {
            // Arrange
            var firstDate = new DateTime(2013, 10, 7);
            var secondDate = new DateTime(2013, 10, 9);
            var publicHolidays = Array.Empty<DateTime>();

            // Act
            var result = _sut.BusinessDaysBetweenTwoDates(firstDate, secondDate, publicHolidays);

            // Assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void BusinessDaysBetweenTwoDates_WhenThereAreWeekendPublicHolidaysBetweenTwoDates_IgnoresPublicHolidaysWhenCalculatingNumberOfBusinessDays()
        {
            // Arrange
            var firstDate = new DateTime(2023, 9, 7);
            var secondDate = new DateTime(2023, 9, 27);
            var publicHolidays = new DateTime[] {
                new DateTime(2023, 9, 16),
                new DateTime(2023, 9, 24)
            };

            // Act
            var result = _sut.BusinessDaysBetweenTwoDates(firstDate, secondDate, publicHolidays);

            // Assert
            Assert.That(result, Is.EqualTo(13));
        }

        [Test]
        public void BusinessDaysBetweenTwoDates_WhenThereAreWeekdayPublicHolidaysBeforeOrEqualToFirstDate_IgnoresPublicHolidaysWhenCalculatingNumberOfBusinessDays()
        {
            // Arrange
            var firstDate = new DateTime(2013, 12, 26);
            var secondDate = new DateTime(2013, 12, 30);
            var publicHolidays = new DateTime[] {
                new DateTime(2013, 12, 25),
                new DateTime(2013, 12, 26)
            };

            // Act
            var result = _sut.BusinessDaysBetweenTwoDates(firstDate, secondDate, publicHolidays);

            // Assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void BusinessDaysBetweenTwoDates_WhenThereAreWeekdayPublicHolidaysAfterOrEqualToSecondDate_IgnoresPublicHolidaysWhenCalculatingNumberOfBusinessDays()
        {
            // Arrange
            var firstDate = new DateTime(2013, 10, 7);
            var secondDate = new DateTime(2014, 1, 1);
            var publicHolidays = new DateTime[] {
                new DateTime(2014, 1, 1)
            };

            // Act
            var result = _sut.BusinessDaysBetweenTwoDates(firstDate, secondDate, publicHolidays);

            // Assert
            Assert.That(result, Is.EqualTo(61));
        }

        [Test]
        public void BusinessDaysBetweenTwoDates_WhenSecondDateIsOlderThanFirstDate_ReturnsZero()
        {
            // Arrange
            var firstDate = new DateTime(2013, 12, 24);
            var secondDate = new DateTime(2013, 12, 7);
            var publicHolidays = new DateTime[] {
                new DateTime(2013, 12, 25),
                new DateTime(2013, 12, 26)
            };

            // Act
            var result = _sut.BusinessDaysBetweenTwoDates(firstDate, secondDate, publicHolidays);

            // Assert
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void BusinessDaysBetweenTwoDates_WhenSecondDateIsEqualToFirstDate_ReturnsZero()
        {
            // Arrange
            var date = new DateTime(2013, 12, 24);
            var publicHolidays = new DateTime[] {
                new DateTime(2013, 12, 25),
                new DateTime(2013, 12, 26)
            };

            // Act
            var result = _sut.BusinessDaysBetweenTwoDates(date, date, publicHolidays);

            // Assert
            Assert.That(result, Is.EqualTo(0));
        }
    }
}