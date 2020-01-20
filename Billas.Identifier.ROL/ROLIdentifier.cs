using System;

namespace Billas.Identifier.ROL
{
    public class ROLIdentifier : PersonIdentifier<ROLFormatter>
    {
        public const string Oid = "1.2.752.74.9.3";
        public override string System => Oid;
        public override string DisplayName => "Region Örebro Län";
        public override bool CanCalculateBirthDate => false;
        public override bool CanCalculateGender => false;
        public override DateTime CalculatedBirthDate => throw new InvalidOperationException(ExceptionMessage.CannotCalculateAge);
        public override PersonIdentityGender CalculatedGender => throw new InvalidOperationException(ExceptionMessage.CannotCalculateGender);

        public ROLIdentifier(string value)
            : this(new ROLFormatter(value)) { }

        public ROLIdentifier(ROLFormatter formatter)
            : base(formatter) { }

        public override int CalculateAge()
        {
            throw new InvalidOperationException(ExceptionMessage.CannotCalculateAge);
        }

        public override int CalculateAge(DateTime ageAtDate)
        {
            throw new InvalidOperationException(ExceptionMessage.CannotCalculateAge);
        }
    }
}