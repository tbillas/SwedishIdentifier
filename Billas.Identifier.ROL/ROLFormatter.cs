using System;

namespace Billas.Identifier.ROL
{
    /// <summary>
    /// 12 tecken
    /// Format: aaaaaaaaBcde
    ///
    /// aaaaaaaa = 8 siffror 0-9
    /// B = Region/Län(Örebro = T)
    /// c = Versal A-Z eller siffra 0-9
    /// d = Siffra 0-9
    /// e = Versal A-Z eller siffra 0-9
    ///
    /// Exempel:
    /// 12345678TA0A
    /// 19810829TB1F
    /// 19930829T320
    /// </summary>
    public class ROLFormatter : PersonIdentifierFormatter
    {
        public const char RegionChar = 'T';
        public string EightNumbers { get; }
        public char RegionIndicator { get; }
        public char Position2 { get; }
        public int Position3 { get; }
        public char Position4 { get; }

        public ROLFormatter(string value)
            : base(value)
        {
            if (Value.Length != 12)
                throw new PersonIdentifierFormatException(value, ExceptionMessage.IncorrectLength);

            EightNumbers = Value.Substring(0, 8);
            for (int i = 0; i < EightNumbers.Length; i++)
            {
                if(!char.IsNumber(EightNumbers[i]))
                    throw new PersonIdentifierFormatException(value, $"Invalid value for position {i+1} '{EightNumbers[i]}'. Expected a number.");
            }

            RegionIndicator = Value[8];
            if(RegionIndicator != RegionChar)
                throw new PersonIdentifierFormatException(value, $"Invalid region indicator '{RegionIndicator}'. Expected '{RegionChar}'.");

            Position2 = Value[9];
            if(!char.IsLetterOrDigit(Position2) || char.IsLower(Position2) || Position2 == 'Å' || Position2 == 'Ä' || Position2 == 'Ö')
                throw new PersonIdentifierFormatException(value, $"Invalid value for position 10 '{Position2}'.");

            if(!char.IsNumber(Value[10]))
                throw new PersonIdentifierFormatException(value, $"Invalid value for position 11 '{Position3}'. Expected a number.");
            Position3 = Convert.ToInt32(Value[10]);

            Position4 = Value[11];
            if (!char.IsLetterOrDigit(Position4) || char.IsLower(Position4) || Position4 == 'Å' || Position4 == 'Ä' || Position4 == 'Ö')
                throw new PersonIdentifierFormatException(value, $"Invalid value for position 12 '{Position4}'.");
        }
    }
}