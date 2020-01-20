using Billas.Identifier.Builder;
using Billas.Identifier.ROL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Billas.Identifier.Tests.Local
{
    [TestClass]
    public class ROLTests : BaseTest
    {
        [TestMethod]
        public void CanCreate()
        {
            var identity = new PersonIdentifierBuilder().BornYear(1979).BornMonth(11).BornDay(9).AsFemale.BuildROLIdentifier();

            Assert.IsInstanceOfType(identity, typeof(ROLIdentifier));

            Assert.IsFalse(identity.CanCalculateBirthDate);
            Assert.IsFalse(identity.CanCalculateGender);
           
            Print(identity);
        }

        [TestMethod]
        public void CanLoad_1()
        {
            var identity = PersonIdentifier.Load(ROLIdentifier.Oid, "12345678TA0A");

            Assert.IsInstanceOfType(identity, typeof(ROLIdentifier));

            Assert.IsFalse(identity.CanCalculateBirthDate);
            Assert.IsFalse(identity.CanCalculateGender);

            Print(identity);
        }
        [TestMethod]
        public void CanLoad_2()
        {
            var identity = PersonIdentifier.Load(ROLIdentifier.Oid, "19810829TB1F");

            Assert.IsInstanceOfType(identity, typeof(ROLIdentifier));

            Assert.IsFalse(identity.CanCalculateBirthDate);
            Assert.IsFalse(identity.CanCalculateGender);

            Print(identity);
        }
        [TestMethod]
        public void CanLoad_3()
        {
            var identity = PersonIdentifier.Load(ROLIdentifier.Oid, "19930829T320");

            Assert.IsInstanceOfType(identity, typeof(ROLIdentifier));

            Assert.IsFalse(identity.CanCalculateBirthDate);
            Assert.IsFalse(identity.CanCalculateGender);

            Print(identity);
        }

        [TestMethod]
        public void CanInstantiate()
        {
            var identity = new ROLIdentifier("12345678TA0A");

            Assert.IsFalse(identity.CanCalculateBirthDate);
            Assert.IsFalse(identity.CanCalculateGender);

            Print(identity);
        }

        [TestMethod]
        [ExpectedException(typeof(PersonIdentifierFormatException))]
        public void CannotParse()
        {
            PersonIdentifier.Parse("12345678TA0A");
        }
    }
}