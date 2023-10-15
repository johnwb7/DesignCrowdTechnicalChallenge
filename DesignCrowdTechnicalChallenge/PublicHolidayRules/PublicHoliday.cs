namespace DesignCrowdTechnicalChallenge.PublicHolidayRules
{
    public abstract class PublicHoliday
    {
        public string Name { get; }
        public DateTime Date { get; }

        public PublicHoliday(string name, DateTime date)
        {
            Name = name;
            Date = date;
        }
    }
}
