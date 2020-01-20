using System;
using Billas.Identifier.Builder;
using Billas.Identifier.VGR;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Billas.Identifier.Tests.Local
{
    [TestClass]
    public class VGRTests : BaseTest
    {
        [TestMethod]
        public void CanCreate()
        {
            var identity = new PersonIdentifierBuilder().BornYear(1979).BornMonth(11).BornDay(9).AsFemale.BuildVGRIdentifier();

            Assert.IsInstanceOfType(identity, typeof(VGRIdentifier));

            Assert.IsTrue(identity.CanCalculateBirthDate);
            Assert.AreEqual(40, identity.CalculateAge(new DateTime(2020, 1, 1)));

            Assert.IsTrue(identity.CanCalculateGender);
            Assert.AreEqual(PersonIdentityGender.Female, identity.CalculatedGender);

            Print(identity);
        }

        [TestMethod]
        public void CanLoad_1()
        {
            var identity = PersonIdentifier.Load(VGRIdentifier.Oid, "19810829M070");

            Assert.IsInstanceOfType(identity, typeof(VGRIdentifier));

            Assert.IsTrue(identity.CanCalculateBirthDate);
            Assert.AreEqual(38, identity.CalculateAge(new DateTime(2020, 1, 1)));

            Assert.IsTrue(identity.CanCalculateGender);
            Assert.AreEqual(PersonIdentityGender.Male, identity.CalculatedGender);

            Print(identity);
        }
        [TestMethod]
        public void CanLoad_2()
        {
            var identity = PersonIdentifier.Load(VGRIdentifier.Oid, "19450829K087");

            Assert.IsInstanceOfType(identity, typeof(VGRIdentifier));

            Assert.IsTrue(identity.CanCalculateBirthDate);
            Assert.AreEqual(74, identity.CalculateAge(new DateTime(2020, 1, 1)));

            Assert.IsTrue(identity.CanCalculateGender);
            Assert.AreEqual(PersonIdentityGender.Female, identity.CalculatedGender);

            Print(identity);
        }
        [TestMethod]
        public void CanLoad_3()
        {
            var identity = PersonIdentifier.Load(VGRIdentifier.Oid, "19930829X801");

            Assert.IsInstanceOfType(identity, typeof(VGRIdentifier));

            Assert.IsTrue(identity.CanCalculateBirthDate);
            Assert.AreEqual(26, identity.CalculateAge(new DateTime(2020, 1, 1)));

            Assert.IsTrue(identity.CanCalculateGender);
            Assert.AreEqual(PersonIdentityGender.Unknown, identity.CalculatedGender);

            Print(identity);
        }

        [TestMethod]
        public void CanInstantiate()
        {
            var identity = new VGRIdentifier("19810829M070");

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
            PersonIdentifier.Parse("19810829M070");
        }
    }
}