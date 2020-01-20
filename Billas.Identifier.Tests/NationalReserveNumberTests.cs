using System;
using Billas.Identifier.Builder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Billas.Identifier.Tests
{
    [TestClass]
    public class NationalReserveNumberTests : BaseTest
    {
        [TestMethod]
        public void CanParse_1()
        {
            var identity = PersonIdentifier.Parse("22950606-FH20");

            Assert.IsInstanceOfType(identity, typeof(NationalReserveNumberIdentifier));

            Assert.IsTrue(identity.CanCalculateBirthDate);
            Assert.AreEqual(24, identity.CalculateAge(new DateTime(2020, 1, 1)));

            Assert.IsTrue(identity.CanCalculateGender);
            Assert.AreEqual(PersonIdentityGender.Female, identity.CalculatedGender);

            Print(identity);
        }
        [TestMethod]
        public void CanParse_2()
        {
            var identity = PersonIdentifier.Parse("25780404-KHD5");

            Assert.IsInstanceOfType(identity, typeof(NationalReserveNumberIdentifier));

            Assert.IsTrue(identity.CanCalculateBirthDate);
            Assert.AreEqual(41, identity.CalculateAge(new DateTime(2020, 1, 1)));

            Assert.IsFalse(identity.CanCalculateGender);

            Print(identity);
        }
        [TestMethod]
        public void CanParse_3()
        {
            var identity = PersonIdentifier.Parse("00342145-BZ31");

            Assert.IsInstanceOfType(identity, typeof(NationalReserveNumberIdentifier));
            Assert.IsFalse(identity.CanCalculateBirthDate);

            Assert.IsTrue(identity.CanCalculateGender);
            Assert.AreEqual(PersonIdentityGender.Male, identity.CalculatedGender);

            Print(identity);
        }

        [TestMethod]
        public void CanParse_4()
        {
            var identity = PersonIdentifier.Parse("00749852-BZK0");

            Assert.IsInstanceOfType(identity, typeof(NationalReserveNumberIdentifier));
            Assert.IsFalse(identity.CanCalculateBirthDate);
            Assert.IsFalse(identity.CanCalculateGender);

            Print(identity);
        }

        [TestMethod]
        public void CanCreate()
        {
            var identity = new PersonIdentifierBuilder().BornYear(1979).BornMonth(11).BornDay(9).AsFemale.BuildNationalReserveNumber();

            Assert.IsInstanceOfType(identity, typeof(NationalReserveNumberIdentifier));

            Assert.IsTrue(identity.CanCalculateBirthDate);
            Assert.AreEqual(40, identity.CalculateAge(new DateTime(2020, 1, 1)));

            Assert.IsTrue(identity.CanCalculateGender);
            Assert.AreEqual(PersonIdentityGender.Female, identity.CalculatedGender);

            Print(identity);
        }

        [TestMethod]
        public void CanLoad()
        {
            var identity = new PersonIdentifierBuilder().BuildNationalReserveNumber();
            var loaded = PersonIdentifier.Load(NationalReserveNumberIdentifier.Oid, identity.ToString(PersonIdentifierFormatOption.None));

            Assert.IsInstanceOfType(loaded, typeof(NationalReserveNumberIdentifier));

            Print(identity);
        }

        [TestMethod]
        public void CanInstantiate()
        {
            var identity = new NationalReserveNumberIdentifier("22950606-FH20");

            Assert.IsTrue(identity.CanCalculateBirthDate);
            Assert.AreEqual(24, identity.CalculateAge(new DateTime(2020, 1, 1)));

            Assert.IsTrue(identity.CanCalculateGender);
            Assert.AreEqual(PersonIdentityGender.Female, identity.CalculatedGender);

            Print(identity);
        }
    }
}