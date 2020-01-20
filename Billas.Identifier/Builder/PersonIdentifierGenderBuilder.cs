using System;

namespace Billas.Identifier.Builder
{
    public partial class PersonIdentifierBuilder
    {
        public class PersonIdentifierGenderBuilder
        {
            private readonly Random _random;

            public PersonIdentifierGenderBuilder()
                : this(new Random(Guid.NewGuid().GetHashCode())) { }
            public PersonIdentifierGenderBuilder(Random random)
            {
                _random = random;
            }

            public bool HaveGender(PersonIdentifierBuilder builder)
            {
                return builder._gender.HasValue;
            }

            public PersonIdentityGender Build(PersonIdentifierBuilder builder, bool acceptUnknown = false)
            {
                return Build(builder._gender, acceptUnknown);
            }

            public PersonIdentityGender Build(PersonIdentityGender? gender, bool acceptUnknown = false)
            {
                if (!gender.HasValue)
                {
                    var rand = _random.Next(acceptUnknown ? 0 : 1, 3);
                    gender = (PersonIdentityGender)rand;
                }

                return gender.Value;
            }

            public int BuildAsInt(PersonIdentifierBuilder builder)
            {
                return BuildAsInt(builder._gender);
            }
            public int BuildAsInt(PersonIdentityGender? gender)
            {
                if (!gender.HasValue)
                    gender = Build((PersonIdentityGender?)null);

                return ConvertToInt(gender.Value);
            }

            public int ConvertToInt(PersonIdentityGender gender)
            {
                if (gender == PersonIdentityGender.Unknown) throw new ArgumentException();
                var randomNumber = 2 * _random.Next(0, 10);
                return gender == PersonIdentityGender.Male ? randomNumber > 0 ? (randomNumber % 10) + 1 : 1 : randomNumber > 0 ? randomNumber % 10 : 0;
            }
        }
    }
}
