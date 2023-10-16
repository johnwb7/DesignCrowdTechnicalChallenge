# DesignCrowdTechnicalChallenge
An implemantation for the DesignCrowd technical challenge. Implementation can be found in the ```DesignCrowdTechnicalChallenge``` project, with corresponding tests in ```DesignCrowdTechnicalChallengeTests```

## ```BusinessDayCounter.cs```
This class provides functionality for calculating business days between two givens dates. It provides implementations for the following methods.
- ```WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)```
- ```BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays)```
- ```BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<PublicHoliday> publicHolidays)```

#### WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
Returns the total number of weekdays between two given dates, excluding the provided `firstDate` and `secondDate` arguments.
E.g. a call to this method with the following arguments would return ```3```.
  - ```firstDate```: Monday 16th October 2023
  - ```secondDate```: Friday 20th October 2023

If ```secondDate``` is equal to or before ```firstDate```, the returned value will be ```0```

#### BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays)
Returns the total number of business days between two dates, excluding any public holidays that occur within this period. A typical business day is one of Monday, Tuesday, Wednesday, Thursday, Friday.
E.g. a call to this method with the following arguments would return ```2```
  - ```firstDate```: Monday 16th October 2023
  - ```secondDate```: Friday 20th October 2023
  - ```publicHolidays```: Wednesday 18th October 2023

If ```secondDate``` is equal to or before ```firstDate```, the returned value will be ```0```

#### BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<PublicHoliday> publicHolidays)
An overload for this method that accepts an ```IList``` of the ```PublicHoliday``` base class. Further information on this class can be found below

## ```PublicHoliday.cs```
An abstract base class for representing different types of public holidays and the rules that are used when calculating their date. Custom public holiday rules can be implemented by inheriting from this class.
This class contains the following properties:
  - ```Name```: ```string``` The name of the public holiday e.g. "Christmas Day"
  - ```Date```: ```DateTime``` The date this public holiday is celebrated e.g. "25th December 2012"
    
Implementations for some public holiday rules are provided as part of this project

#### ```FixedDatePublicHoliday : PublicHoliday```
Represents a public holiday with a date that is always on the same day every year. E.. Anzac Day is always celebrated on April 25th.

##### Constructor
```new FixedDatePublicHoliday(string name, DateTime date)```
##### Constructor Arguments
- ```name```: Name of the public holiday
- ```date```: Date of the public holiday

#### ```NextWeekdayPublicHoliday : PublicHoliday```
Represents a public holiday with a date that is always the same day unless it falls on a weekend, in which case the public holiday is moved to the next working day. E.g New Years Day

##### Constructor
```new NextWeekdayPublicHoliday(string name, DateTime occasionDate)```
##### Constructor Arguments
- ```name```: Name of the public holiday.
- ```occasionDate```: The original date of the occasion.

#### ```NthDayOfTheMonthPublicHoliday : PublicHoliday```
Represents a public holiday with a date that always occurs on the nth day of a given month. E.g the Queens Birthday always occurs on the second Monday in June.

##### Constructor
```new NthDayOfTheMonthPublicHoliday(string name, DayOfWeek dayOfWeek, int nthOccurence, int month, int year)```
##### Constructor Arguments
- ```name``` : Name of the public holiday
- ```dayOfWeek``` : The day of the week the public holiday should occur on
- ```nthOccurence``` : The nth occurence of the day of the week the public holiday should occur on
- ```month``` : The month the public holiday should occur in
- ```year``` : The year the public holiday should occur in

If the provided nth occurence does not exist, an ```ArgumentException will be thrown```

## ```DateTime``` Extensions
This project also offers some extensions for the ```DateTime``` type

#### ```IsWeekendDay(this DateTime dateTime)```
Returns ```true``` is the provided ```dateTime``` is a Saturday or Sunday. Otherwise returns ```false```