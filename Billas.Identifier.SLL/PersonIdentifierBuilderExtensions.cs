using System;
using Billas.Identifier.Builder;

namespace Billas.Identifier.SLL
{
    public static class PersonIdentifierBuilderExtensions
    {
        public static SLLIdentifier BuildSLLIdentifier(this PersonIdentifierBuilder builder)
        {
            var random = new Random(Guid.NewGuid().GetHashCode());

            var year = new PersonIdentifierBuilder.PersonIdentifierDateBuilder(random).Build(builder).Year;

            var sequence = random.Next(0, 100000);

            var nr = $"{SLLFormatter.Start}{year:0000}{sequence:00000}";
            var luhnCheckSum = LuhnAlgorithm.Generate(nr);

            return new SLLIdentifier($"{nr}{luhnCheckSum}");
        }
    }
}