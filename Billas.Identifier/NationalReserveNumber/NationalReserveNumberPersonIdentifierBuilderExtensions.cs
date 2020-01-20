using System;
using System.Linq;
using Billas.Identifier.Builder;

namespace Billas.Identifier
{
    public static class NationalReserveNumberPersonIdentifierBuilderExtensions
    {
        private static readonly string[] UnwantedStrings = { "APA", "ARG", "ASS", "BAJ", "BSS", "CUC", "CUK", "DUM", "ETA", "ETT", "FAG", "FAN", "FEG", "FEL", "FEM", "FES", "FET", "FNL", "FUC", "FUK", "FUL", "GAM", "GAY", "GEJ", "GEY", "GHB", "GUD", "GYN", "HAT", "HBT", "HKH", "HOR", "HOT", "KGB", "KKK", "KUC", "KUF", "KUG", "KUK", "KYK", "LAM", "LAT", "LEM", "LOJ", "LSD", "LUS", "MAD", "MAO", "MEN", "MES", "MLB", "MUS", "NAZ", "NRP", "NSF", "NYP", "OND", "OOO", "ORM", "PAJ", "PKK", "PLO", "PMS", "PUB", "RAP", "RAS", "ROM", "RPS", "RUS", "SEG", "SEX", "SJU", "SOS", "SPY", "SUG", "SUP", "SUR", "TBC", "TOA", "TOK", "TRE", "TYP", "UFO", "USA", "WAM", "WAR", "WWW", "XTC", "XTZ", "XXL", "XXX", "ZEX", "ZOG", "ZPY", "ZUG", "ZUP", "ZOO" };
        private static readonly string[] Letters = { "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L", "M", "N", "P", "R", "S", "T", "U", "X", "Y", "Z" };

        public static NationalReserveNumberIdentifier BuildNationalReserveNumber(this PersonIdentifierBuilder builder)
        { 
            var random = new Random(Guid.NewGuid().GetHashCode());

            int year, month, day;

            var dateBuilder = new PersonIdentifierBuilder.PersonIdentifierDateBuilder(random);
            

            if (!dateBuilder.HaveDate(builder))
            {
                //Skapa reservnummer där fördelsedatum är okänt
                year = random.Next(1, 100);
                month = random.Next(20, 100);
                day = random.Next(40, 60);
            }
            else
            {
                var date = dateBuilder.Build(builder);
                year = date.Year + (random.Next(0, 2) == 0 ? 300 : 600); //Orkar inte hantera fler alternativ!!
                month = date.Month;
                day = date.Day;
            }

            var genderBuilder = new PersonIdentifierBuilder.PersonIdentifierGenderBuilder(random);
            string serial;
            do
            {
                var firstRandom = Letters[random.Next(0, Letters.Length)];
                var secondRandom = Letters[random.Next(0, Letters.Length)];

                var gender = genderBuilder.HaveGender(builder) ? genderBuilder.BuildAsInt(builder).ToString() : Letters[random.Next(0, 21)];

                serial = $"{firstRandom}{secondRandom}{gender}";

            } while (UnwantedStrings.Contains(serial));

            var luhnPart = $"{year:0000}{month:00}{day:00}{serial}";
            var check = LuhnAlgorithm.Generate(luhnPart);

            var century = year > 1000 ? Convert.ToInt32(year.ToString().Substring(0, 2)) : 0;
            var twoDigitYear = year > 1000 ? Convert.ToInt32(year.ToString().Substring(2, 2)) : year;

            return new NationalReserveNumberIdentifier(new NationalReserveNumberFormatter($"{century:00}{twoDigitYear:00}{month:00}{day:00}{serial}{check}"));
        }
    }

}