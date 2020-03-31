using System;

namespace Billas.Identifier
{
    public abstract class PersonIdentifier<TFormatter> : IPersonIdentifier where TFormatter : PersonIdentifierFormatter
    {
        public virtual string Value => ToString(new PersonIdentifierFormatOption { WithCentury = true, WithHyphen = false });
        public abstract string System { get; }

        private readonly TFormatter _formatter;

        protected PersonIdentifier(TFormatter formatter)
        {
            _formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
        }

        public abstract bool CanCalculateBirthDate { get; }
        public abstract bool CanCalculateGender { get; }
        public abstract DateTime CalculatedBirthDate { get; }
        public abstract PersonIdentityGender CalculatedGender { get; }
        public abstract int CalculateAge();
        public abstract int CalculateAge(DateTime ageAtDate);

        public override string ToString()
        {
            return ToString(PersonIdentifierFormatOption.None);
        }
        
        public string ToString(PersonIdentifierFormatOption option)
        {
            return Format(_formatter, option);
        }

        protected virtual string Format(TFormatter formatter, PersonIdentifierFormatOption option)
        {
            return formatter.Format(option);
        }
    }
}