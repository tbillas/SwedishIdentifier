using System;
using System.Text;

namespace Billas.Identifier
{
    /// <summary>
    /// Samordningsnummer består liksom personnumret av tio siffror.
    /// De inledande sex siffrorna utgår från personens födelsetid med den skillnaden att man
    /// lägger till 60 till födelsedagen.
    /// För en person som är född den 23 augusti 1964 så blir de sex första siffrorna i samordningsnumret därför 640883.
    /// </summary>
    public sealed class CoordinationNumberFormatter : DateBasedFormatter
    {
        public const int AddToDays = 60;

        public int TwoDigitSerialNumber { get; }
        public int LuhnControlNumber { get; }
        public int GenderIndicator { get; }

        public CoordinationNumberFormatter(string value)
            : base(value)
        {
            if (SerialNumber.Length != 4)
                throw new PersonIdentifierFormatException(value, ExceptionMessage.IncorrectLength);

            if (Value.Length != 10 && Value.Length != 12)
                throw new PersonIdentifierFormatException(value, ExceptionMessage.IncorrectLength);

            TwoDigitSerialNumber = Convert.ToInt32(SerialNumber.Substring(0, 2));
            GenderIndicator = Convert.ToInt32(SerialNumber.Substring(2, 1));
            LuhnControlNumber = Convert.ToInt32(SerialNumber.Substring(3, 1));
            var luhnValidationDay = Day;
            Day -= AddToDays;

            if(Day < 1)
                throw new PersonIdentifierFormatException(value, ExceptionMessage.CoordinationNumberDayError);

            if (!LuhnAlgorithm.Validate($"{TwoDigitYear:00}{Month:00}{luhnValidationDay:00}{SerialNumber}"))
                throw new PersonIdentifierFormatException(value, ExceptionMessage.LuhnError);


        }

        protected override void FormatDay(StringBuilder sb, PersonIdentifierFormatOption option)
        {
            var day = Day + AddToDays;
            sb.Append(day.ToString("00"));
        }
    }
}