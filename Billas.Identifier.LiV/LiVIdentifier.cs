namespace Billas.Identifier.LiV
{
    public class LiVIdentifier : DateBasedPersonIdentifier<LiVFormatter>
    {
        public const string Oid = "1.2.752.74.9.2";
        public override string System => Oid;
        public override string DisplayName => "Landstinget i Värmland";

        public override bool CanCalculateGender { get; }
        public override PersonIdentityGender CalculatedGender { get; }

        public LiVIdentifier(string value)
            : this(new LiVFormatter(value)) { }

        public LiVIdentifier(LiVFormatter formatter)
            : base(formatter)
        {
            CanCalculateGender = true;
            CalculatedGender = formatter.GenderIndicator == 0 || formatter.GenderIndicator == 1 
                ? PersonIdentityGender.Unknown
                : formatter.GenderIndicator % 2 == 0 ? PersonIdentityGender.Female : PersonIdentityGender.Male;
        }
    }
}