using System;

namespace Billas.Identifier.SLL
{
    /// <summary>
    /// 12 tecken
    /// Format: 99yyyyNNNNNC
    ///
    /// yyyy = Årtal
    /// NNNNN = Löpnummer
    /// C = Kontrollsiffra enligt Luhn-metoden
    ///
    /// Exempel:
    ///
    /// 991981000011
    /// 991945000024
    /// 991993000033
    /// </summary>
    public class SLLFormatter : PersonIdentifierFormatter
    {
        public const string Start = "99";
        public int Year { get; }
        public int FiveDigitSerialNumber { get; }
        public int LuhnControlNumber { get; }

        public SLLFormatter(string value) 
            : base(value)
        {
            if (Value.Length != 12)
                throw new PersonIdentifierFormatException(value, ExceptionMessage.IncorrectLength);

            if (!LuhnAlgorithm.Validate(value))
                throw new PersonIdentifierFormatException(value, ExceptionMessage.LuhnError);

            var start = Value.Substring(0, 2);
            if (start != Start)
                throw new PersonIdentifierFormatException(value, $"Incorrect SLL format -> starting with '{start}'. Expected '{Start}'.");

            Year = Convert.ToInt32(Value.Substring(2, 4));
            FiveDigitSerialNumber = Convert.ToInt32(Value.Substring(6, 5));
            LuhnControlNumber = Convert.ToInt32(Value.Substring(11, 1));
        }

        public override string Format(PersonIdentifierFormatOption option)
        {
            return $"{Start}{Year:0000}{FiveDigitSerialNumber:00000}{LuhnControlNumber}";
        }
    }
}