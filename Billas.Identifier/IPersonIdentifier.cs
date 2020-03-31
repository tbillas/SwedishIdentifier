using System;

namespace Billas.Identifier
{
    public interface IPersonIdentifier
    {
        string Value { get; }
        string System { get; }

        bool CanCalculateBirthDate { get; }
        bool CanCalculateGender { get; }

        DateTime CalculatedBirthDate { get; }
        PersonIdentityGender CalculatedGender { get; }

        int CalculateAge();
        int CalculateAge(DateTime ageAtDate);
        
        string ToString(PersonIdentifierFormatOption option);
    }
}