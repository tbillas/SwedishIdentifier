namespace Billas.Identifier
{
    public sealed class PersonalNumberIdentifier : DateBasedPersonIdentifier<PersonalNumberFormatter>
    {
        public const string Oid = "1.2.752.129.2.1.3.1";
        public override string System => Oid;
        public override string DisplayName => "Personnummer";

        public override bool CanCalculateGender { get; }
        public override PersonIdentityGender CalculatedGender { get; }

        public PersonalNumberIdentifier(string value)
            : this(new PersonalNumberFormatter(value)) { }

        public PersonalNumberIdentifier(PersonalNumberFormatter formatter)
            : base(formatter)
        {
            CanCalculateGender = true;
            CalculatedGender = formatter.GenderIndicator % 2 == 0 ? PersonIdentityGender.Female : PersonIdentityGender.Male;
        }
    }
}