using System;
using Billas.Identifier.Builder;
using Billas.Identifier.LiV;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Billas.Identifier.Tests.Local
{
    [TestClass]
    public class LiVTests : BaseTest
    {
        [TestMethod]
        public void CanCreate()
        {
            var identity = new PersonIdentifierBuilder().BornYear(1979).BornMonth(11).BornDay(9).AsFemale.BuildLiVIdentifier();

            Assert.IsInstanceOfType(identity, typeof(LiVIdentifier));

            Assert.IsTrue(identity.CanCalculateBirthDate);
            Assert.AreEqual(40, identity.CalculateAge(new DateTime(2020, 1, 1)));

            Assert.IsTrue(identity.CanCalculateGender);
            Assert.AreEqual(PersonIdentityGender.Female, identity.CalculatedGender);

            Print(identity);
        }

        [TestMethod]
        public void CanLoad_1()
        {
            var identity = PersonIdentifier.Load(LiVIdentifier.Oid, "19810829-SU3A");

            Assert.IsInstanceOfType(identity, typeof(LiVIdentifier));

            Assert.IsTrue(identity.CanCalculateBirthDate);
            Assert.AreEqual(38, identity.CalculateAge(new DateTime(2020, 1, 1)));

            Assert.IsTrue(identity.CanCalculateGender);
            Assert.AreEqual(PersonIdentityGender.Male, identity.CalculatedGender);

            Print(identity);
        }
        [TestMethod]
        public void CanLoad_2()
        {
            var identity = PersonIdentifier.Load(LiVIdentifier.Oid, "19450829-SF2B");

            Assert.IsInstanceOfType(identity, typeof(LiVIdentifier));

            Assert.IsTrue(identity.CanCalculateBirthDate);
            Assert.AreEqual(74, identity.CalculateAge(new DateTime(2020, 1, 1)));

            Assert.IsTrue(identity.CanCalculateGender);
            Assert.AreEqual(PersonIdentityGender.Female, identity.CalculatedGender);

            Print(identity);
        }
        [TestMethod]
        public void CanLoad_3()
        {
            var identity = PersonIdentifier.Load(LiVIdentifier.Oid, "19930829-SX0C");

            Assert.IsInstanceOfType(identity, typeof(LiVIdentifier));

            Assert.IsTrue(identity.CanCalculateBirthDate);
            Assert.AreEqual(26, identity.CalculateAge(new DateTime(2020, 1, 1)));

            Assert.IsTrue(identity.CanCalculateGender);
            Assert.AreEqual(PersonIdentityGender.Unknown, identity.CalculatedGender);

            Print(identity);
        }

        [TestMethod]
        public void CanInstantiate()
        {
            var identity = new LiVIdentifier("19810829-SU3A");

            Assert.IsTrue(identity.CanCalculateBirthDate);
            Assert.AreEqual(38, identity.CalculateAge(new DateTime(2020, 1, 1)));

            Assert.IsTrue(identity.CanCalculateGender);
            Assert.AreEqual(PersonIdentityGender.Male, identity.CalculatedGender);

            Print(identity);
        }

        [TestMethod]
        [ExpectedException(typeof(PersonIdentifierFormatException))]
        public void CannotParse()
        {
            PersonIdentifier.Parse("19810829-SU3A");
        }
    }
}