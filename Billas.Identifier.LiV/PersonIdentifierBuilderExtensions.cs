using System;
using Billas.Identifier.Builder;

namespace Billas.Identifier.LiV
{
    public static class PersonIdentifierBuilderExtensions
    {
        public static LiVIdentifier BuildLiVIdentifier(this PersonIdentifierBuilder builder)
        {
            var random = new Random(Guid.NewGuid().GetHashCode());
            var dateBuilder = new PersonIdentifierBuilder.PersonIdentifierDateBuilder(random);
            var genderBuilder = new PersonIdentifierBuilder.PersonIdentifierGenderBuilder(random);

            var date = dateBuilder.Build(builder);
            var type = dateBuilder.HaveDate(builder) ? 'F' : LiVFormatter.ValidTypeLetters[random.Next(0, LiVFormatter.ValidTypeLetters.Length)];
            var gender = genderBuilder.Build(builder);
            int genderNumber;
            if (gender == PersonIdentityGender.Unknown)
            {
                genderNumber = random.Next(0, 2);
            }
            else
            {
                do
                {
                    genderNumber = genderBuilder.ConvertToInt(gender);
                } while (genderNumber <= 1);
            }

            var order = LiVFormatter.ValidOrderLetters[random.Next(0, LiVFormatter.ValidOrderLetters.Length)];

            return new LiVIdentifier($"{date:yyyyMMdd}{LiVFormatter.RegionChar}{type}{genderNumber}{order}");
        }
    }
}