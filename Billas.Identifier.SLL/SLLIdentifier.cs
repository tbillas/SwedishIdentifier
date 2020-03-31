using System;

namespace Billas.Identifier.SLL
{
    public class SLLIdentifier : PersonIdentifier<SLLFormatter>
    {
        public const string Oid = "1.2.752.97.3.1.3";
        public override string System => Oid;
        
        public override bool CanCalculateBirthDate => false;
        public override bool CanCalculateGender => false;
        public override DateTime CalculatedBirthDate => throw new InvalidOperationException(ExceptionMessage.CannotCalculateAge);
        public override PersonIdentityGender CalculatedGender => throw new InvalidOperationException(ExceptionMessage.CannotCalculateGender);

        public SLLIdentifier(string value)
            : this(new SLLFormatter(value)) { }
        public SLLIdentifier(SLLFormatter formatter) 
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