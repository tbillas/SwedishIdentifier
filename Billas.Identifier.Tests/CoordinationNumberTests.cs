using System;
using Billas.Identifier.Builder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Billas.Identifier.Tests
{
    [TestClass]
    public class CoordinationNumberTests : BaseTest
    {
        [TestMethod]
        public void CanParse_1()
        {
            var identity = PersonIdentifier.Parse("19620670-3974");

            Assert.IsInstanceOfType(identity, typeof(CoordinationNumberIdentifier));

            Assert.AreEqual("196206703974", identity.Value);
            Assert.AreEqual(CoordinationNumberIdentifier.Oid, identity.System);

            Assert.IsTrue(identity.CanCalculateBirthDate);
            Assert.AreEqual(57, identity.CalculateAge(new DateTime(2020, 1, 1)));

            Assert.IsTrue(identity.CanCalculateGender);
            Assert.AreEqual(PersonIdentityGender.Male, identity.CalculatedGender);

            Print(identity);
        }

        [TestMethod]
        public void CanCreate()
        {
            var identity = new PersonIdentifierBuilder().BornYear(1979).BornMonth(11).BornDay(9).AsFemale.BuildCoordinationNumber();

            Assert.IsInstanceOfType(identity, typeof(CoordinationNumberIdentifier));

            Assert.IsTrue(identity.CanCalculateBirthDate);
            Assert.AreEqual(40, identity.CalculateAge(new DateTime(2020, 1, 1)));

            Assert.IsTrue(identity.CanCalculateGender);
            Assert.AreEqual(PersonIdentityGender.Female, identity.CalculatedGender);

            Print(identity);
        }

        [TestMethod]
        public void CanLoad()
        {
            var identity = new PersonIdentifierBuilder().BuildCoordinationNumber();
            var loaded = PersonIdentifier.Load(CoordinationNumberIdentifier.Oid, identity.ToString(PersonIdentifierFormatOption.None));

            Assert.IsInstanceOfType(loaded, typeof(CoordinationNumberIdentifier));

            Print(identity);
        }

        [TestMethod]
        public void CanInstantiate()
        {
            var identity = new CoordinationNumberIdentifier("19620670-3974");

            Assert.AreEqual("196206703974", identity.Value);
            Assert.AreEqual(CoordinationNumberIdentifier.Oid, identity.System);

            Assert.IsTrue(identity.CanCalculateBirthDate);
            Assert.AreEqual(57, identity.CalculateAge(new DateTime(2020, 1, 1)));

            Assert.IsTrue(identity.CanCalculateGender);
            Assert.AreEqual(PersonIdentityGender.Male, identity.CalculatedGender);

            Print(identity);
        }
    }
}