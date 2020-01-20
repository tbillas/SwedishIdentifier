using System;
using System.Collections.Generic;
using System.Linq;

namespace Billas.Identifier.VGR
{
    /// <summary>
    /// 12 tecken
    /// Format: yyyymmddGggC
    ///
    /// yyyy = Årtal
    /// mm = Månad
    /// dd = Dag
    ///
    /// Ovanstående ska vara ett giltigt datum enl.svensk kalender.
    /// 
    /// G = Kön 1. Giltiga värden är versalerna "K" (kvinna), "M" (man) eller "X" (okänt).
    /// gg = Löpnummer.Giltiga värden är 06-79, udda för män och jämn för kvinna samt 80-89 för kön okänt.
    /// C = Kontrollsiffra enligt Luhn-metoden.Bokstäver får först värden enligt följande:
    ///     "K" = 5
    ///     "M" = 7
    ///     "X" = 8
    /// 
    /// Exempel:
    /// 19810829M070
    /// 19450829K087
    /// 19930829X801
    /// </summary>
    public class VGRFormatter : DateBasedFormatter
    {
        public char GenderIndicator { get; }
        public int TwoDigitSerial { get; }
        public int LuhnControlNumber { get; }

        public VGRFormatter(DateTime date, string serialNumber)
            : this(date.Year, date.Month, date.Day, serialNumber) { }

        public VGRFormatter(int year, int month, int day, string serialNumber)
            : this($"{year:0000}{month:00}{day:00}{serialNumber}") { }

        public VGRFormatter(string value) 
            : base(value)
        {
            if (Value.Length != 12)
                throw new PersonIdentifierFormatException(value, ExceptionMessage.IncorrectLength);

            if (SerialNumber.Length != 4)
                throw new PersonIdentifierFormatException(value, ExceptionMessage.IncorrectLength);

            GenderIndicator = SerialNumber[0];
            TwoDigitSerial = Convert.ToInt32(SerialNumber.Substring(1, 2));
            LuhnControlNumber = Convert.ToInt32(SerialNumber[3]);

            var nr = GenderMap.FirstOrDefault(x => x.Letter == GenderIndicator)?.Number ?? throw new PersonIdentifierFormatException(value, $"Incorrect VGR format -> invalid gender indicator '{GenderIndicator}'.");
            var str = Value.Replace(GenderIndicator, nr);
            if(!LuhnAlgorithm.Validate(str))
                throw new PersonIdentifierFormatException(value, ExceptionMessage.LuhnError);
        }

        internal static readonly List<GenderMapItem> GenderMap = new List<GenderMapItem>
        {
            new GenderMapItem {Gender = PersonIdentityGender.Female, Letter = 'K', Number = '5'},
            new GenderMapItem {Gender = PersonIdentityGender.Male, Letter = 'M', Number = '7'},
            new GenderMapItem {Gender = PersonIdentityGender.Unknown, Letter = 'X', Number = '8'}
        };

        internal class GenderMapItem
        {
            public PersonIdentityGender Gender;
            public char Letter;
            public char Number;
        }
    }
}