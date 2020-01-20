using System;
using System.Linq;

namespace Billas.Identifier
{
    public class PersonIdentifierFormatter
    {
        public const string DefaultHyphen = "-";
        public const string HyphenWhenOver100YearsOld = "+";
        public string Value { get; }
        public string Hyphen { get; }

        public PersonIdentifierFormatter(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (value.Contains(DefaultHyphen))
                Hyphen = DefaultHyphen;

            else if (value.Contains(HyphenWhenOver100YearsOld))
                Hyphen = HyphenWhenOver100YearsOld;

            Value = value.Replace(DefaultHyphen, "").Replace(HyphenWhenOver100YearsOld, "");
        }
        
        public bool IsAllNumeric()
        {
            return Value.All(char.IsNumber);
        }

        public virtual string Format(PersonIdentifierFormatOption option)
        {
            return Value;
        }

        public override string ToString()
        {
            return Format(PersonIdentifierFormatOption.None);
        }
    }
}