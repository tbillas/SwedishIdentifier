using System;

namespace Billas.Identifier
{
    public sealed class NationalReserveNumberIdentifier : DateBasedPersonIdentifier<NationalReserveNumberFormatter>
    {
        public const string Oid = "1.2.752.74.9.1";
        private readonly PersonIdentityGender _calculatedGender;
        public override string System => Oid;
        
        public override bool CanCalculateGender { get; }

        public override PersonIdentityGender CalculatedGender
        {
            get
            {
                if (!CanCalculateGender)
                    throw new InvalidOperationException(ExceptionMessage.CannotCalculateGender);
                return _calculatedGender;
            }
        }


        public NationalReserveNumberIdentifier(string value)
            : this(new NationalReserveNumberFormatter(value)) { }

        public NationalReserveNumberIdentifier(NationalReserveNumberFormatter formatter)
            : base(formatter)
        {
            var canCalculateGender = char.IsNumber(formatter.PossibleGenderIndicator);
            CanCalculateGender = canCalculateGender;
            if (canCalculateGender)
            {
                var genderNumber = formatter.PossibleGenderIndicator - '0';
                _calculatedGender = genderNumber % 2 == 0 ? PersonIdentityGender.Female : PersonIdentityGender.Male;
            }
            else
                _calculatedGender = PersonIdentityGender.Unknown;
        }
    }
}