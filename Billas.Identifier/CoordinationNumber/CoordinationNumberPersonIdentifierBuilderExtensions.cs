using System;
using Billas.Identifier.Builder;

namespace Billas.Identifier
{
    public static class CoordinationNumberPersonIdentifierBuilderExtensions
    {
        public static CoordinationNumberIdentifier BuildCoordinationNumber(this PersonIdentifierBuilder builder)
        {
            var random = new Random(Guid.NewGuid().GetHashCode());
            var date = new PersonIdentifierBuilder.PersonIdentifierDateBuilder(random).Build(builder);
            var gender = new PersonIdentifierBuilder.PersonIdentifierGenderBuilder(random).BuildAsInt(builder);
            var dayWithAddedDays = date.Day + CoordinationNumberFormatter.AddToDays;
            var serial = random.Next(0, 100).ToString("00");


            var luhnChecksum = LuhnAlgorithm.Generate($"{date:yyMM}{dayWithAddedDays:00}{serial}{gender}");
            return new CoordinationNumberIdentifier($"{date:yyyyMM}{dayWithAddedDays:00}{serial}{gender}{luhnChecksum}");
        }
    }

}