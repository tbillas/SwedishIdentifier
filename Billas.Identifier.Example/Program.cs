using System;
using Billas.Identifier.Builder;
using Billas.Identifier.LiV;
using Billas.Identifier.ROL;
using Billas.Identifier.SLL;
using Billas.Identifier.VGR;

namespace Billas.Identifier.Example
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Nr\t\t\tKön\t\tÅlder");
            
            try
            {
                Console.WriteLine("****************Personnummer*********************");
                Print(PersonIdentifier.Parse("191212121212"));
                Print(new PersonIdentifierBuilder().BornYear(1979).BornMonth(11).BornDay(9).AsFemale.BuildPersonalNumber());
                Print(new PersonIdentifierBuilder().BuildPersonalNumber());
                Print(PersonIdentifier.Load(PersonalNumberIdentifier.Oid, new PersonIdentifierBuilder().BuildPersonalNumber().ToString(PersonIdentifierFormatOption.None)));

                Console.WriteLine();
                Console.WriteLine("****************Samordningsnummer*********************");
                Print(PersonIdentifier.Parse("19620670-3974"));
                Print(new PersonIdentifierBuilder().BornYear(1979).BornMonth(11).BornDay(9).AsFemale.BuildCoordinationNumber());
                Print(new PersonIdentifierBuilder().BuildCoordinationNumber());
                Print(PersonIdentifier.Load(CoordinationNumberIdentifier.Oid, new PersonIdentifierBuilder().BuildCoordinationNumber().ToString(PersonIdentifierFormatOption.None)));
                
                
                Console.WriteLine();
                Console.WriteLine("****************Nationella reservnummer*********************");
                Print(PersonIdentifier.Parse("22950606-FH20"));
                Print(PersonIdentifier.Parse("25780404-KHD5"));
                Print(PersonIdentifier.Parse("00342145-BZ31"));
                Print(PersonIdentifier.Parse("00749852-BZK0"));
                Print(new PersonIdentifierBuilder().BornYear(1979).BornMonth(11).BornDay(9).AsFemale.BuildNationalReserveNumber());
                Print(new PersonIdentifierBuilder().BuildNationalReserveNumber());
                Print(PersonIdentifier.Load(NationalReserveNumberIdentifier.Oid, new PersonIdentifierBuilder().BuildNationalReserveNumber().ToString(PersonIdentifierFormatOption.None)));
                


                Console.WriteLine();
                Console.WriteLine("****************Lokala reservnummer: SLL*********************");
                Print(PersonIdentifier.Load("1.2.752.97.3.1.3", "991981000011"));
                Print(PersonIdentifier.Load("1.2.752.97.3.1.3", "991945000024"));
                Print(PersonIdentifier.Load("1.2.752.97.3.1.3", "991993000033"));
                Print(new PersonIdentifierBuilder().BornYear(1979).BuildSLLIdentifier());
                Print(new PersonIdentifierBuilder().BuildSLLIdentifier());


                Console.WriteLine();
                Console.WriteLine("****************Lokala reservnummer: VGR*********************");
                Print(PersonIdentifier.Load("1.2.752.113.11.0.2.1.1.1", "19810829M070"));
                Print(PersonIdentifier.Load("1.2.752.113.11.0.2.1.1.1", "19450829K087"));
                Print(PersonIdentifier.Load("1.2.752.113.11.0.2.1.1.1", "19930829X801"));
                Print(new PersonIdentifierBuilder().BornYear(1979).BornMonth(11).BornDay(9).AsFemale.BuildVGRIdentifier());


                Console.WriteLine();
                Console.WriteLine("****************Lokala reservnummer: LiV*********************");
                Print(PersonIdentifier.Load("1.2.752.74.9.2", "19810829-SU3A"));
                Print(PersonIdentifier.Load("1.2.752.74.9.2", "19450829-SF2B"));
                Print(PersonIdentifier.Load("1.2.752.74.9.2", "19930829-SX0C"));
                Print(new PersonIdentifierBuilder().BornYear(1979).BornMonth(11).BornDay(9).AsFemale.BuildLiVIdentifier());
                Print(new PersonIdentifierBuilder().BuildLiVIdentifier());


                Console.WriteLine();
                Console.WriteLine("****************Lokala reservnummer: RÖL*********************");
                Print(PersonIdentifier.Load("1.2.752.74.9.3", "12345678TA0A"));
                Print(PersonIdentifier.Load("1.2.752.74.9.3", "19810829TB1F"));
                Print(PersonIdentifier.Load("1.2.752.74.9.3", "19930829T320"));
                Print(new PersonIdentifierBuilder().BornYear(1979).BornMonth(11).BornDay(9).AsFemale.BuildROLIdentifier());
                Print(new PersonIdentifierBuilder().BuildROLIdentifier());

            }
            catch (PersonIdentifierFormatException e)
            {
                Console.Error.WriteLine($"{e.Value} => {e.Message}");
            }
            catch (PersonIdentifierInstanceException e)
            {
                Console.Error.WriteLine($"{e.Message}");
            }

            Console.ReadLine();
        }

        private static void Print(IPersonIdentifier id)
        {
            var gender = id.CanCalculateGender ? id.CalculatedGender.ToString() : "[N/A]";
            var age = id.CanCalculateBirthDate ? id.CalculateAge().ToString() : "[N/A]";
            

            Console.WriteLine("{0}\t\t{1}\t\t{2}",id.ToString(PersonIdentifierFormatOption.ForDisplay), gender, age);
        }
    }
}
