using System;
using System.Globalization;

namespace Billas.Identifier.Builder
{
    public partial class PersonIdentifierBuilder
    {
        public class PersonIdentifierDateBuilder
        {
            private readonly Random _random;

            public PersonIdentifierDateBuilder()
                : this(new Random(Guid.NewGuid().GetHashCode())) { }
            public PersonIdentifierDateBuilder(Random random)
            {
                _random = random;
            }
            public bool HaveDate(PersonIdentifierBuilder builder)
            {
                return builder._year.HasValue || builder._month.HasValue || builder._day.HasValue;
            }
            public DateTime Build(PersonIdentifierBuilder builder)
            {
                return Build(builder._year, builder._month, builder._day);
            }
            public DateTime Build(int? year, int? month, int? day)
            {
                if (!year.HasValue)
                {
                    var thisYear = DateTime.Today.Year;
                    year = _random.Next(thisYear - 130, thisYear - 1);
                }
                if (!month.HasValue)
                    month = _random.Next(1, 13);
                if (!day.HasValue)
                    day = _random.Next(1, CultureInfo.CurrentCulture.Calendar.GetDaysInMonth(year.Value, month.Value) + 1);


                return new DateTime(year.Value, month.Value, day.Value);
            }
        }
    }
}
