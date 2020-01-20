using System;
using System.Text;

namespace Billas.Identifier
{
    public class DateBasedFormatter : PersonIdentifierFormatter
    {
        public int Century { get; }
        public bool CenturySpecified { get; }
        public int TwoDigitYear { get; }
        public int Month { get; }
        public int Day { get; protected set; }
        public string SerialNumber { get; }

        public DateBasedFormatter(DateTime date, string serialNumber)
            : this(date.Year, date.Month, date.Day, serialNumber) { }

        public DateBasedFormatter(int year, int month, int day, string serialNumber)
            : this($"{year:0000}{month:00}{day:00}{serialNumber}") {}

        public DateBasedFormatter(string value)
            : base(value)
        {
            if(Value.Length < 4)
                throw new PersonIdentifierFormatException(value, ExceptionMessage.IncorrectLength);

            var tmpValue = Value;
            SerialNumber = tmpValue.Substring(tmpValue.Length - 4, 4);

            tmpValue = tmpValue.Substring(0, tmpValue.Length - 4);

            if (tmpValue.Length == 8)
            {
                Century = Convert.ToInt32(tmpValue.Substring(0, 2));
                TwoDigitYear = Convert.ToInt32(tmpValue.Substring(2, 2));
                tmpValue = tmpValue.Substring(4);
                CenturySpecified = true;
            }
            else if(tmpValue.Length == 6)
            {
                if (string.IsNullOrEmpty(Hyphen))
                    throw new PersonIdentifierFormatException(value, ExceptionMessage.HyphenError);

                //Om året är 2020...
                //...och pnr är 190418+XXXX så är personen över 100 och århundradet ska då vara 19.
                //...och pnr är 190418-XXXX så är personen under 100 och århundradet ska då vara 20.

                //...och pnr är 990418+XXXX så är personen över 100 och århundradet ska då vara 18.
                //...och pnr är 990418-XXXX så är personen under 100 och århundradet ska då vara 19.

                var thisYearLastTwo = int.Parse(DateTime.Today.ToString("yy"));
                TwoDigitYear = Convert.ToInt32(tmpValue.Substring(0, 2));
                if (Hyphen == HyphenWhenOver100YearsOld)
                    Century = TwoDigitYear <= thisYearLastTwo ? 19 : 18;
                else
                    Century = TwoDigitYear <= thisYearLastTwo ? 20 : 19;

                tmpValue = tmpValue.Substring(2);
                CenturySpecified = false;
            }
            else
                throw new PersonIdentifierFormatException(value, ExceptionMessage.IncorrectLength);

            Month = Convert.ToInt32(tmpValue.Substring(0, 2));
            Day = Convert.ToInt32(tmpValue.Substring(2, 2));
        }

        public bool HaveValidYear()
        {
            var year = GetFourDigitYear();
            if (year < 1800 || year >= 2100) return false;
            return true;
        }

        public bool HaveValidDate()
        {
            return HaveValidYear() && Month > 0 && Month <= 12 && Day > 0 && Day <= 31;
        }

        public virtual int GetFourDigitYear()
        {
            return Convert.ToInt32($"{Century:00}{TwoDigitYear:00}");
        }
        
        protected virtual void FormatYear(StringBuilder sb, PersonIdentifierFormatOption option)
        {
            if (option == null) throw new ArgumentNullException(nameof(option));
            var withCentury = (option.WithCentury.HasValue && option.WithCentury.Value) || CenturySpecified;

            if (withCentury)
            {
                sb.Append(Century.ToString("00"));
                if (option.CenturySeparated)
                    sb.Append(option.CenturySeparationChar);
            }

            sb.Append(TwoDigitYear.ToString("00"));
        }
        protected virtual void FormatMonth(StringBuilder sb, PersonIdentifierFormatOption option)
        {
            sb.Append(Month.ToString("00"));
        }
        protected virtual void FormatDay(StringBuilder sb, PersonIdentifierFormatOption option)
        {
            sb.Append(Day.ToString("00"));
        }
        protected virtual void FormatHyphen(StringBuilder sb, PersonIdentifierFormatOption option)
        {
            var useHyphen = (option.WithHyphen.HasValue && option.WithHyphen.Value) || !string.IsNullOrEmpty(Hyphen);

            if (useHyphen)
            {
                var hyphen = option.HyphenChar;
                if (string.IsNullOrEmpty(hyphen))
                    hyphen = Hyphen;
                if (string.IsNullOrEmpty(hyphen))
                {
                    hyphen = DefaultHyphen;
                }

                sb.Append(hyphen);
            }
        }
        protected virtual void FormatSerial(StringBuilder sb, PersonIdentifierFormatOption option)
        {
            sb.Append(SerialNumber);
        }

        public override string Format(PersonIdentifierFormatOption option)
        {
            var sb = new StringBuilder();
            FormatYear(sb, option);
            FormatMonth(sb, option);
            FormatDay(sb, option);
            FormatHyphen(sb, option);
            FormatSerial(sb, option);
            return sb.ToString();
        }
    }
}