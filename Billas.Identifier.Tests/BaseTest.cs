using System;

namespace Billas.Identifier.Tests
{
    public abstract class BaseTest
    {
        protected void Print(IPersonIdentifier identifier)
        {
            string gender = identifier.CanCalculateGender ? identifier.CalculatedGender.ToString() : "[N/A]";
            var age = identifier.CanCalculateBirthDate ? identifier.CalculateAge().ToString() : "[N/A]";
            Console.WriteLine("{3}: {0}   gender:{1}   age:{2}", identifier.ToString(PersonIdentifierFormatOption.ForDisplay), gender, age, identifier.DisplayName);
        }
    }
}