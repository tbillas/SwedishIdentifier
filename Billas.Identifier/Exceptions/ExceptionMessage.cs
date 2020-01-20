namespace Billas.Identifier
{
    public static class ExceptionMessage
    {
        public const string NotAbleToParse = "Not able to parse supplied value. This parser can only parse values that correspond to either PersonalNumber-, CoordinationNumber- or NationalReserveNumber-formats.";
        public const string IncorrectLength = "Supplied value have incorrect length.";
        public const string LuhnError = "Supplied value did not validate.";
        public const string CoordinationNumberDayError = "Invalid coordination number. Day-part has invalid value.";
        public const string HyphenError = "When century is not supplied, a hyphen(+or -) is mandatory.";

        public const string CannotCalculateAge = "This instance of person identifier is not capable of calculating age.";
        public const string CannotCalculateGender = "This instance of person identifier is not capable of calculating gender.";
        public const string AgeError = "Invalid age calculation. Either supplied birth date is incorrect, or supplied 'today' is incorrect.";
    }
}