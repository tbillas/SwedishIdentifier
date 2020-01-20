using System.Linq;

namespace Billas.Identifier.LiV
{
    /// <summary>
    /// 13 tecken inklusive bindestreck
    /// Format: yyyymmdd-ABNC
    ///
    /// yyyy = Årtal
    /// mm = Månad
    /// dd = Dag
    ///
    /// Ovanstående ska vara ett giltigt datum enl.svensk kalender.
    ///
    /// A = Region/Län (Värmland = S)
    /// B = Typ.Giltiga värden är versalerna:
    ///     "F" = Känt födelsedatum
    ///     "U"  = Utan födelsedatum
    ///     "X" = Okänd
    ///     "P" = Personal utan personnummer el.samordningsnummer
    ///     "L"= Labprov utan känd patient
    /// N = Kön.Giltiga värden är 2,4,6,8 för kvinnor och 3,5,7,9 för män. 0, 1 för kön okänt.
    /// C = Ordningsnummer.Giltiga värden är versalerna A-Z utom V.Siffra 1-9 för testmiljöer.
    ///
    ///
    /// Exempel:
    /// 19810829-SU3A
    /// 19450829-SF2B
    /// 19930829-SX0C
    /// </summary>
    public class LiVFormatter : DateBasedFormatter
    {
        public const char RegionChar = 'S';
        public char RegionIndicator { get; }
        public char TypeIndicator { get; }
        public int GenderIndicator { get; }
        public char Order { get; }

        public static char[] ValidOrderLetters = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'X', 'Y', 'Z'};
        
        //"F" = Känt födelsedatum, "U"  = Utan födelsedatum, "X" = Okänd, "P" = Personal utan personnummer el.samordningsnummer, "L"= Labprov utan känd patient
        public static char[] ValidTypeLetters = { 'F', 'U', 'X', 'P', 'L' };

        public LiVFormatter(string value) 
            : base(value)
        {
            if (Value.Length != 12)
                throw new PersonIdentifierFormatException(value, ExceptionMessage.IncorrectLength);

            if (SerialNumber.Length != 4)
                throw new PersonIdentifierFormatException(value, ExceptionMessage.IncorrectLength);

            RegionIndicator = SerialNumber[0];
            if(RegionIndicator != RegionChar)
                throw new PersonIdentifierFormatException(value, $"Invalid region indicator '{RegionIndicator}'. Expected '{RegionChar}'.");

            TypeIndicator = SerialNumber[1];
            if(!ValidTypeLetters.Contains(TypeIndicator))
                throw new PersonIdentifierFormatException(value, $"Invalid type indicator '{TypeIndicator}'.");

            if(!char.IsNumber(SerialNumber[2]))
                throw new PersonIdentifierFormatException(value, $"Invalid gender indicator '{SerialNumber[2]}'.");
            GenderIndicator = SerialNumber[2] - '0';
            
            Order = SerialNumber[3];
            if(!ValidOrderLetters.Contains(Order))
                throw new PersonIdentifierFormatException(value, $"Invalid order indicator '{Order}'.");
        }

    }
}