namespace Billas.Identifier
{
    public sealed class CoordinationNumberIdentifier : DateBasedPersonIdentifier<CoordinationNumberFormatter>
    {
        public const string Oid = "1.2.752.129.2.1.3.3";
        public override string System => Oid;
        public override string DisplayName => "Samordningsnummer";

        public override bool CanCalculateGender { get; }
        public override PersonIdentityGender CalculatedGender { get; }

        public CoordinationNumberIdentifier(string value)
            : this(new CoordinationNumberFormatter(value)) { }

        public CoordinationNumberIdentifier(CoordinationNumberFormatter formatter)
            : base(formatter)
        {
            CanCalculateGender = true;
            CalculatedGender = formatter.GenderIndicator % 2 == 0 ? PersonIdentityGender.Female : PersonIdentityGender.Male;
        }
    }
}