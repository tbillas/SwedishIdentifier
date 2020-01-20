using System;
using Billas.Identifier.Builder;

namespace Billas.Identifier.VGR
{
    public static class PersonIdentiferBuilderExtensions
    {
        public static VGRIdentifier BuildVGRIdentifier(this PersonIdentifierBuilder builder)
        {
            var random = new Random(Guid.NewGuid().GetHashCode());
            var date = new PersonIdentifierBuilder.PersonIdentifierDateBuilder(random).Build(builder);
            
            var gender = new PersonIdentifierBuilder.PersonIdentifierGenderBuilder(random).Build(builder, true);
            var genderMap = VGRFormatter.GenderMap.Find(x => x.Gender == gender);

            int sequence = 0;
            if (gender == PersonIdentityGender.Female)
                sequence = 2 * random.Next(6 / 2, 80 / 2);
            if (gender == PersonIdentityGender.Male)
                sequence = 2 * random.Next(6 / 2, 80 / 2) + 1;
            if (gender == PersonIdentityGender.Unknown)
                sequence = random.Next(80, 90);
            
            var number = $"{date:yyyyMMdd}{genderMap.Number}{sequence:00}";
            var luhnCheckSum = LuhnAlgorithm.Generate(number);

            var fourLast = $"{genderMap.Letter}{sequence:00}{luhnCheckSum}";
            
            return new VGRIdentifier($"{date:yyyyMMdd}{fourLast}");
        }
    }
}