using System;
using NUnit.Framework;
using WearHouse_WebApp.Models.Domain;
using WearHouse_WebApp.Models.Entities;

namespace WearHouse_WebApp.Test.Unit.Models
{
    [TestFixture]
    public class dbWearableTest
    {
        private dbWearable uut;

        [SetUp]
        public void SetUp()
        {
            uut = new dbWearable();
        }

        [Test]
        public void DefaultConstructor_ConvertToWearableModelWithoutOwner_Throws()
        {
            // Arrange takes place in SetUp()

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => uut.ConvertToWearableModelWithoutOwner());
        }

        [Test]
        public void DefaultConstructor_ConvertToWearableModel_Throws()
        {
            // Arrange takes place in SetUp()

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => uut.ConvertToWearableModel());
        }

        [Test]
        public void AmbiguousConstructor_InvalidState_ConvertToWearableModelWithoutOwner_Throws()
        {
            // Arrange
            uut = new dbWearable() { State = "Invalid" };

            // Act
            Assert.Throws<ArgumentException>(() => uut.ConvertToWearableModelWithoutOwner());
        }

        [Test]
        public void AmbiguousConstructor_WithState_ConvertToWearableModelWithoutOwner_Succeeds()
        {
            // Arrange
            uut = new dbWearable() { State = "Inactive" };

            // Act
            var actual = uut.ConvertToWearableModelWithoutOwner();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(WearableState.Inactive, actual.State);
                Assert.AreEqual(0, actual.ID);
                Assert.IsNull(actual.Title);
                Assert.IsNull(actual.Description);
                Assert.IsNull(actual.Owner);
                Assert.IsNull(actual.Comments);
                Assert.IsNull(actual.ImageUrls);
                Assert.IsNull(actual.ImageFiles);
            });
        }

        [Test]
        public void AmbiguousConstructor_OwnerIsNull_ValidParameters_ConvertToWearableModelWithoutOwner_Succeeds()
        {
            // Arrange
            uut = new dbWearable()
            {
                Title = "Test",
                Description = "Desc",
                WearableId = 1,
                State = "Inactive",
                ImageUrls = "/test1\n/test2",
            };

            // Act
            var actual = uut.ConvertToWearableModelWithoutOwner();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(WearableState.Inactive, actual.State);
                Assert.AreEqual(1, actual.ID);
                Assert.AreEqual("Test", actual.Title);
                Assert.AreEqual("Desc", actual.Description);
                Assert.IsNull(actual.Owner);
                Assert.IsNull(actual.Comments);
                Assert.IsNotEmpty(actual.ImageUrls);
            });
        }

        [Test]
        public void AmbiguousConstructor_OwnerIsNull_ValidParameters_ConvertToWearableModel_Succeeds()
        {
            // Arrange
            uut = new dbWearable()
            {
                Title = "Test",
                Description = "Desc",
                WearableId = 1,
                State = "Inactive",
                ImageUrls = "/test1\n/test2",
            };

            // Act
            var actual = uut.ConvertToWearableModel();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(WearableState.Inactive, actual.State);
                Assert.AreEqual(1, actual.ID);
                Assert.AreEqual("Test", actual.Title);
                Assert.AreEqual("Desc", actual.Description);
                Assert.IsNull(actual.Owner);
                Assert.IsNull(actual.Comments);
                Assert.IsNotEmpty(actual.ImageUrls);
            });
        }

        [Test]
        public void AmbiguousConstructor_OwnerExists_ValidParameters_ConvertToWearableModel_Succeeds()
        {
            // Arrange
            var user = new ApplicationUser()
            {
                Id = "1",
                UserName = "Tester",
                FirstName = "Clever",
                LastName = "Tester",
                Email = "clever@tester.org",
                ProfileImageUrl = "profile.png"
            };

            uut = new dbWearable()
            {
                Title = "Test",
                Description = "Desc",
                WearableId = 1,
                State = "Inactive",
                ImageUrls = "/test1\n/test2",
                ApplicationUser = user,
            };

            // Act
            var actual = uut.ConvertToWearableModel();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(WearableState.Inactive, actual.State);
                Assert.AreEqual(1, actual.ID);
                Assert.AreEqual("Test", actual.Title);
                Assert.AreEqual("Desc", actual.Description);
                Assert.IsNotNull(actual.Owner);
                Assert.IsNull(actual.Comments);
                Assert.IsNotEmpty(actual.ImageUrls);
            });

        }
    }
}
