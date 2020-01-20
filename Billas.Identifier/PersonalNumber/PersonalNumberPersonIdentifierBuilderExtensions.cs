using System;
using Billas.Identifier.Builder;

namespace Billas.Identifier
{
    public static class PersonalNumberPersonIdentifierBuilderExtensions
    {
        public static PersonalNumberIdentifier BuildPersonalNumber(this PersonIdentifierBuilder builder)
        {
            var random = new Random(Guid.NewGuid().GetHashCode());
            var date = new PersonIdentifierBuilder.PersonIdentifierDateBuilder(random).Build(builder);
            var gender = new PersonIdentifierBuilder.PersonIdentifierGenderBuilder(random).BuildAsInt(builder);
            var serial = random.Next(0, 100).ToString("00");

            var luhnChecksum = LuhnAlgorithm.Generate($"{date:yyMMdd}{serial}{gender}");
            return new PersonalNumberIdentifier($"{date:yyyyMMdd}{serial}{gender}{luhnChecksum}");
        }
    }
}