using System;

namespace Billas.Identifier
{
    /// <summary>
    /// De sex första siffrorna består av personens födelsedatum noterat som år (utan sekelangivning), månad och dag.
    /// De tre första siffrorna efter skiljetecknet är personnumrets födelsenummer, som består av ett löpnummer där
    /// tredje siffran anger personens kön – jämn siffra för kvinnor och udda för män. Den sista siffran är en kontrollsiffra.
    /// (Om födelsenumren har tagit slut för ett visst datum ges ett personnummer med datumet för efterföljande dag.)
    ///
    /// Personnumret är uppbyggt av 10 siffror indelade i två grupper om 6 respektive 4 siffror. Grupperna är åtskilda
    /// med ett skiljetecken, normalt ett bindestreck (-), men om personen är över 100 år ett plustecken (+)
    /// </summary>
    public sealed class PersonalNumberFormatter : DateBasedFormatter
    {
        public int TwoDigitSerialNumber { get; }
        public int LuhnControlNumber { get; }
        public int GenderIndicator { get; }

        public PersonalNumberFormatter(string value)
            : base(value)
        {
            if(SerialNumber.Length != 4)
                throw new PersonIdentifierFormatException(value, ExceptionMessage.IncorrectLength);

            if (Value.Length != 10 && Value.Length != 12)
                throw new PersonIdentifierFormatException(value, ExceptionMessage.IncorrectLength);

            TwoDigitSerialNumber = Convert.ToInt32(SerialNumber.Substring(0, 2));
            GenderIndicator = Convert.ToInt32(SerialNumber.Substring(2, 1));
            LuhnControlNumber = Convert.ToInt32(SerialNumber.Substring(3, 1));

            if (!LuhnAlgorithm.Validate($"{TwoDigitYear:00}{Month:00}{Day:00}{SerialNumber}"))
                throw new PersonIdentifierFormatException(value, ExceptionMessage.LuhnError);
        }
    }
}