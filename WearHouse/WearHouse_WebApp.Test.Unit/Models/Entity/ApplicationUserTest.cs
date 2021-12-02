using NUnit.Framework;
using System;
using System.Collections.Generic;
using WearHouse_WebApp.Models.Domain;
using WearHouse_WebApp.Models.Entities;

namespace WearHouse_WebApp.Test.Unit.Models
{
    [TestFixture]
    public class ApplicationUserTest
    {
        private ApplicationUser uut;

        [SetUp]
        public void SetUp()
        {
            uut = new ApplicationUser();
        }

        [Test]
        public void DefaultConstructor_ConvertToUserModel_ThrowsNull()
        {
            // Arrange takes place in SetUp()

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => uut.ConvertToUserModel());
        }

        [Test]
        public void DefaultConstructor_ConvertToUserModelWithoutWearables_Succeeds()
        {
            // Arrange takes place in SetUp()

            // Act
            var actual = uut.ConvertToUserModelWithoutWearables();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(null, actual.FirstName);
                Assert.AreEqual(null, actual.LastName);
                Assert.AreEqual(null, actual.ProfileImageUrl);
                Assert.AreEqual(null, actual.Wearables);
            });
        }

        [Test]
        public void AmbiguousConstructor_WearablesInitializedWithOneElement_ConvertToUserModel_Succeeds()
        {
            // Arrange
            var wearables = new List<dbWearable>();
            wearables.Add(new dbWearable()
            {
                WearableId = 1,
                Title = "Test",
                Description = "Desc",
                UserId = "1",
                UserContactInfo = "clever@tester.org",
                State = "Inactive",
                ApplicationUser = new ApplicationUser()
                {
                    Id = "1",
                    UserName = "Tester",
                },
            });

            uut = new ApplicationUser()
            {
                Location = "San Francisco",
                FirstName = "Clever",
                LastName = "Tester",
                ProfileImageUrl = "profile.png",
                Wearables = wearables,
            };

            // Act 
            var actual = uut.ConvertToUserModel();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual("Clever", actual.FirstName);
                Assert.AreEqual("Tester", actual.LastName);
                Assert.AreEqual("profile.png", actual.ProfileImageUrl);
                Assert.AreEqual(1, actual.Wearables.Count);
                Assert.AreEqual(1, actual.Wearables[0].ID);
                Assert.AreEqual(WearableState.Inactive, actual.Wearables[0].State);
                Assert.AreEqual("Test", actual.Wearables[0].Title);
                Assert.AreEqual("Desc", actual.Wearables[0].Description);
            });
        }

        [Test]
        public void AmbiguousConstructor_WearablesNotInitialized_ConvertToUserModel_Throws()
        {
            // Arrange
            uut = new ApplicationUser()
            {
                Location = "San Francisco",
                FirstName = "Clever",
                LastName = "Tester",
                ProfileImageUrl = "profile.png",
            };

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => uut.ConvertToUserModel());
        }

        [Test]
        public void AmbiguousConstructor_ConvertToUserModelWithOutWearables_Succeeds()
        {
            // Arrange
            uut = new ApplicationUser()
            {
                Location = "San Francisco",
                FirstName = "Clever",
                LastName = "Tester",
                ProfileImageUrl = "profile.png",
            };

            // Act
            var actual = uut.ConvertToUserModelWithoutWearables();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual("Clever", actual.FirstName);
                Assert.AreEqual("Tester", actual.LastName);
                Assert.AreEqual("profile.png", actual.ProfileImageUrl);
                Assert.AreEqual(null, actual.Wearables);
            });
        }

    }
}
