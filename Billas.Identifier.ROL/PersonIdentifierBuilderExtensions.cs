using System;
using Billas.Identifier.Builder;

namespace Billas.Identifier.ROL
{
    public static class PersonIdentifierBuilderExtensions
    {
        public static ROLIdentifier BuildROLIdentifier(this PersonIdentifierBuilder builder)
        {
            var random = new Random(Guid.NewGuid().GetHashCode());
            var dateBuilder = new PersonIdentifierBuilder.PersonIdentifierDateBuilder(random);

            var date = (dateBuilder.HaveDate(builder))
                ? dateBuilder.Build(builder).ToString("yyyyMMdd") 
                : random.Next(0, 100000000).ToString("00000000");

            var num = random.Next(0, 26);
            var pos2 = ((char)('a' + num)).ToString().ToUpper();
            if (random.Next(0, 2) == 0)
                pos2 = random.Next(0, 10).ToString();

            var pos3 = random.Next(0, 10);

            num = random.Next(0, 26);
            var pos4 = ((char)('a' + num)).ToString().ToUpper();
            if (random.Next(0, 2) == 0)
                pos4 = random.Next(0, 10).ToString();

            return new ROL.ROLIdentifier($"{date}{ROLFormatter.RegionChar}{pos2}{pos3:0}{pos4}");
        }
    }
}