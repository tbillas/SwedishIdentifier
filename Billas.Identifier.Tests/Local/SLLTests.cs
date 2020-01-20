using Billas.Identifier.Builder;
using Billas.Identifier.SLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Billas.Identifier.Tests.Local
{
    [TestClass]
    public class SLLTests : BaseTest
    {
        [TestMethod]
        public void CanCreate()
        {
            var identity = new PersonIdentifierBuilder().BornYear(1979).BornMonth(11).BornDay(9).AsFemale.BuildSLLIdentifier();

            Assert.IsInstanceOfType(identity, typeof(SLLIdentifier));

            Assert.IsFalse(identity.CanCalculateBirthDate);
            Assert.IsFalse(identity.CanCalculateGender);

            Print(identity);
        }

        [TestMethod]
        public void CanLoad_1()
        {
            var identity = PersonIdentifier.Load(SLLIdentifier.Oid, "991981000011");

            Assert.IsInstanceOfType(identity, typeof(SLLIdentifier));
            Assert.IsFalse(identity.CanCalculateBirthDate);
            Assert.IsFalse(identity.CanCalculateGender);

            Print(identity);
        }
        [TestMethod]
        public void CanLoad_2()
        {
            var identity = PersonIdentifier.Load(SLLIdentifier.Oid, "991945000024");

            Assert.IsInstanceOfType(identity, typeof(SLLIdentifier));
            Assert.IsFalse(identity.CanCalculateBirthDate);
            Assert.IsFalse(identity.CanCalculateGender);

            Print(identity);
        }
        [TestMethod]
        public void CanLoad_3()
        {
            var identity = PersonIdentifier.Load(SLLIdentifier.Oid, "991993000033");

            Assert.IsInstanceOfType(identity, typeof(SLLIdentifier));
            Assert.IsFalse(identity.CanCalculateBirthDate);
            Assert.IsFalse(identity.CanCalculateGender);

            Print(identity);
        }

        [TestMethod]
        public void CanLoad_4()
        {
            var identity = new PersonIdentifierBuilder().BuildSLLIdentifier();
            var loaded = PersonIdentifier.Load(SLLIdentifier.Oid, identity.ToString(PersonIdentifierFormatOption.None));

            Assert.IsInstanceOfType(loaded, typeof(SLLIdentifier));

            Print(identity);
        }

        [TestMethod]
        public void CanInstantiate()
        {
            var identity = new SLLIdentifier("991981000011");

            Assert.IsFalse(identity.CanCalculateBirthDate);
            Assert.IsFalse(identity.CanCalculateGender);

            Print(identity);
        }

        [TestMethod]
        [ExpectedException(typeof(PersonIdentifierFormatException))]
        public void CannotParse()
        {
            PersonIdentifier.Parse("991981000011");
        }
    }
}