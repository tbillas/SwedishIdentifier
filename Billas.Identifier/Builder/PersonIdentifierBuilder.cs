using System;

namespace Billas.Identifier.Builder
{
    public partial class PersonIdentifierBuilder
    {
        private int? _year;
        private int? _month;
        private int? _day;
        private PersonIdentityGender? _gender;

        public PersonIdentifierBuilder BornYear(int year)
        {
            _year = year;
            return this;
        }

        public PersonIdentifierBuilder BornMonth(int month)
        {
            _month = month;
            return this;
        }

        public PersonIdentifierBuilder BornDay(int day)
        {
            _day = day;
            return this;
        }

        public PersonIdentifierBuilder BornDate(DateTime date)
        {
            _year = date.Year;
            _month = date.Month;
            _day = date.Day;
            return this;
        }

        public PersonIdentifierBuilder WithAge(int age)
        {
            var born = DateTime.Today.AddYears(-age);
            born = born.AddDays(-(new Random().Next(0, 300)));
            return BornDate(born);
        }

        public PersonIdentifierBuilder AsMale
        {
            get
            {
                _gender = PersonIdentityGender.Male;
                return this;
            }
        }
        public PersonIdentifierBuilder AsFemale
        {
            get
            {
                _gender = PersonIdentityGender.Female;
                return this;
            }
        }
    }
}