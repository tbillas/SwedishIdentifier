using System;

namespace Billas.Identifier
{
    public abstract class DateBasedPersonIdentifier<TFormatter> : PersonIdentifier<TFormatter> where TFormatter: DateBasedFormatter
    {
        public override bool CanCalculateBirthDate { get; }
        public override DateTime CalculatedBirthDate { get; }

        protected DateBasedPersonIdentifier(TFormatter formatter)
            : base(formatter)
        {
            var canCalculateBirthDate = formatter.HaveValidDate();
            CanCalculateBirthDate = canCalculateBirthDate;
            if (canCalculateBirthDate)
                CalculatedBirthDate = new DateTime(formatter.GetFourDigitYear(), formatter.Month, formatter.Day);
        }

        
        public override int CalculateAge()
        {
            return CalculateAge(DateTime.Today);
        }
        public override int CalculateAge(DateTime ageAtDate)
        {
            if(!CanCalculateBirthDate)
                throw new InvalidOperationException(ExceptionMessage.CannotCalculateAge);

            var ticks = ageAtDate.Date.Subtract(CalculatedBirthDate).Ticks;
            if (ticks < DateTime.MinValue.Ticks)
                throw new PersonIdentifierInstanceException(ExceptionMessage.AgeError);
            if (ticks > DateTime.MaxValue.Ticks)
                throw new PersonIdentifierInstanceException(ExceptionMessage.AgeError);
            return new DateTime(ticks).Year - 1;
        }

        protected override string Format(TFormatter formatter, PersonIdentifierFormatOption option)
        {
            if (string.IsNullOrEmpty(option.HyphenChar) && CanCalculateBirthDate)
            {
                option.HyphenChar = CalculateAge() >= 100 ? PersonIdentifierFormatter.HyphenWhenOver100YearsOld : PersonIdentifierFormatter.DefaultHyphen;
            }
            return base.Format(formatter, option);
        }
    }
}