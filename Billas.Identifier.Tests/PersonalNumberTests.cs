using System;
using Billas.Identifier.Builder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Billas.Identifier.Tests
{
    [TestClass]
    public class PersonalNumberTests : BaseTest
    {
        [TestMethod]
        public void CanParse_1()
        {
            var identity = PersonIdentifier.Parse("191212121212");

            Assert.IsInstanceOfType(identity, typeof(PersonalNumberIdentifier));

            Assert.AreEqual("191212121212", identity.Value);
            Assert.AreEqual(PersonalNumberIdentifier.Oid, identity.System);

            Assert.IsTrue(identity.CanCalculateBirthDate);
            Assert.AreEqual(107, identity.CalculateAge(new DateTime(2020, 1, 1)));

            Assert.IsTrue(identity.CanCalculateGender);
            Assert.AreEqual(PersonIdentityGender.Male, identity.CalculatedGender);

            Print(identity);
        }

        [TestMethod]
        public void CanParse_2()
        {
            var identity = PersonIdentifier.Parse("190510+7122");

            Assert.IsInstanceOfType(identity, typeof(PersonalNumberIdentifier));

            Assert.AreEqual("191905107122", identity.Value);
            Assert.AreEqual(PersonalNumberIdentifier.Oid, identity.System);

            Assert.IsTrue(identity.CanCalculateBirthDate);
            Assert.AreEqual(100, identity.CalculateAge(new DateTime(2020, 1, 1)));

            Assert.IsTrue(identity.CanCalculateGender);
            Assert.AreEqual(PersonIdentityGender.Female, identity.CalculatedGender);

            Print(identity);
        }

        [TestMethod]
        public void CanParse_3()
        {
            var identity = PersonIdentifier.Parse("991125+3830");

            Assert.IsInstanceOfType(identity, typeof(PersonalNumberIdentifier));

            Assert.AreEqual("189911253830", identity.Value);
            Assert.AreEqual(PersonalNumberIdentifier.Oid, identity.System);

            Assert.IsTrue(identity.CanCalculateBirthDate);
            Assert.AreEqual(120, identity.CalculateAge(new DateTime(2020, 1, 1)));

            Assert.IsTrue(identity.CanCalculateGender);
            Assert.AreEqual(PersonIdentityGender.Male, identity.CalculatedGender);

            Print(identity);//19990911-9944
        }

        [TestMethod]
        public void CanParse_4()
        {
            var identity = PersonIdentifier.Parse("990911-9944");

            Assert.IsInstanceOfType(identity, typeof(PersonalNumberIdentifier));

            Assert.AreEqual("199909119944", identity.Value);
            Assert.AreEqual(PersonalNumberIdentifier.Oid, identity.System);

            Assert.IsTrue(identity.CanCalculateBirthDate);
            Assert.AreEqual(20, identity.CalculateAge(new DateTime(2020, 1, 1)));

            Assert.IsTrue(identity.CanCalculateGender);
            Assert.AreEqual(PersonIdentityGender.Female, identity.CalculatedGender);

            Print(identity);
        }

        [TestMethod]
        public void CanCreate()
        {
            var identity = new PersonIdentifierBuilder().BornYear(1979).BornMonth(11).BornDay(9).AsFemale.BuildPersonalNumber();

            Assert.IsInstanceOfType(identity, typeof(PersonalNumberIdentifier));

            Assert.IsTrue(identity.CanCalculateBirthDate);
            Assert.AreEqual(40, identity.CalculateAge(new DateTime(2020, 1, 1)));

            Assert.IsTrue(identity.CanCalculateGender);
            Assert.AreEqual(PersonIdentityGender.Female, identity.CalculatedGender);

            Print(identity);
        }

        [TestMethod]
        public void CanLoad()
        {
            var identity = new PersonIdentifierBuilder().BuildPersonalNumber();
            var loaded = PersonIdentifier.Load(PersonalNumberIdentifier.Oid, identity.ToString(PersonIdentifierFormatOption.None));

            Assert.IsInstanceOfType(loaded, typeof(PersonalNumberIdentifier));

            Print(identity);
        }

        [TestMethod]
        public void CanInstantiate()
        {
            var identity = new PersonalNumberIdentifier("191212121212");

            Assert.IsInstanceOfType(identity, typeof(PersonalNumberIdentifier));

            Assert.AreEqual("191212121212", identity.Value);
            Assert.AreEqual(PersonalNumberIdentifier.Oid, identity.System);

            Assert.IsTrue(identity.CanCalculateBirthDate);
            Assert.AreEqual(107, identity.CalculateAge(new DateTime(2020, 1, 1)));

            Assert.IsTrue(identity.CanCalculateGender);
            Assert.AreEqual(PersonIdentityGender.Male, identity.CalculatedGender);

            Print(identity);
        }
    }
}
