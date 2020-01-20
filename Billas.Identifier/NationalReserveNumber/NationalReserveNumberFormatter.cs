using System;

namespace Billas.Identifier
{
    /// <summary>
    /// Generellt format för NRID är enligt: XXYYMMDDNNGC där C alltid är en kontrollsiffra.
    /// Formatet kan specificera känt respektive okänt födelsedatum samt känt respektive okänt kön.
    ///
    /// XX
    /// - Känt födelsedatum: födelsesekel + konstant
    ///     Första serien har födelsesekel +3 (d.v.s. 19 → 22, 20 → 23).
    ///     När första serien tar slut för en kombination datum/kön ökas konstanten modulo 3:
    ///     +3, +6, +9 o.s.v.till +78 (totalt 26 serier)
    ///     (01-17 reserveras för framtida behov, 18-21 används inte för att inte blandas ihop med PNR/SNR)
    /// - Okänt födelsedatum: 00
    ///
    /// YYMMDD
    /// - Känt födelsedatum: födelsedatum
    ///     YY=00-99
    ///     MM=01-12
    ///     DD=01-31
    /// - Okänt födelsedatum: löpnummer
    ///     YY = 00 - 99
    ///     MM=20-99
    ///     DD=40-59
    ///
    /// NN
    /// - Löpande bokstavskombination
    ///     versala bokstäver A-Z
    ///     ej IOQVW(21 unika bokstäver)
    ///
    /// G
    /// - Känt kön: siffra (samma som för PNR)
    ///     kvinna = 0,2,4,6,8
    ///     man = 1,3,5,7,9
    /// - okänt kön: versal bokstav
    ///     A-Z
    ///     ej IOQVW(21 unika bokstäver)
    ///
    /// C
    /// - Kontrollsiffra
    ///     Enligt standard Luhn, https://sv.wikipedia.org/wiki/Luhn-algoritmen
    ///     Bokstäver ersätts med dess ASCII-värde, t.ex.A → 65
    ///     Beräkningen baseras på hela id-strängen före kontrollsiffran d.v.s.XXYYMMDDNNG(11-tecken).
    /// </summary>
    public sealed class NationalReserveNumberFormatter : DateBasedFormatter
    {
        public int ActualCentury { get; }
        public char FirstSerialChar { get; }
        public char SecondSerialChar { get; }
        public char PossibleGenderIndicator { get; }
        public int LuhnControlNumber { get; }

        public NationalReserveNumberFormatter(string value)
            : base(value)
        {
            if (SerialNumber.Length != 4)
                throw new PersonIdentifierFormatException(value, ExceptionMessage.IncorrectLength);

            if (Value.Length != 12)
                throw new PersonIdentifierFormatException(value, ExceptionMessage.IncorrectLength);

            ActualCentury = Century > 0 ? ((Century - 19) % 3) + 19 : 0;
            FirstSerialChar = SerialNumber[0];
            SecondSerialChar = SerialNumber[1];
            PossibleGenderIndicator = SerialNumber[2];
            LuhnControlNumber = SerialNumber[3];
        }
        
        public override int GetFourDigitYear()
        {
            return Convert.ToInt32($"{ActualCentury:00}{TwoDigitYear:00}");
        }
    }
}