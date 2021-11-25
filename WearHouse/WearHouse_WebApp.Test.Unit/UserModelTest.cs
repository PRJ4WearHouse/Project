using WearHouse_WebApp.Models.Domain;
using WearHouse_WebApp.Models.Entities;
using NUnit.Framework;
using System.Collections.Generic;

namespace WearHouse_WebApp.Test.Unit
{
    [TestFixture]
    public class UserModelTest
    {
        private UserModel uut;

        [SetUp]
        public void SetUp()
        {
            uut = new UserModel();
        }

        [Test]
        public void ConstructorWithNoWearables_WearablesAreNull()
        {
            // Arrange
            var user = new ApplicationUser()
            {
                Id = "1",
                ProfileImageUrl = "profile.png",
                UserName = "TestUser",
                FirstName = "Clever",
                LastName = "Tester",
                Email = "clever@tester.org"
            };

            // Act
            uut = new UserModel(user, false);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(user.ProfileImageUrl, uut.ProfileImageUrl);
                Assert.AreEqual(user.UserName, uut.Username);
                Assert.AreEqual(user.Id, uut.UserId);
                Assert.AreEqual(null, uut.Address);
                Assert.AreEqual(user.FirstName, uut.FirstName);
                Assert.AreEqual(user.LastName, uut.LastName);
                Assert.AreEqual(user.Email, uut.ContactInfo);
            });
        }

        [Test]
        public void ConstructorWithOneWearable_Succeeds()
        {
            // Arrange
            var user = new ApplicationUser()
            {
                Id = "1",
                ProfileImageUrl = "profile.png",
                UserName = "TestUser",
                FirstName = "Clever",
                LastName = "Tester",
                Email = "clever@tester.org",
                Wearables = new List<dbWearable>(),
            };

            var dbModel = new dbWearable()
            {
                WearableId = 1,
                Title = "Test",
                Description = "Desc",
                UserId = "1",
                UserContactInfo = "clever@tester.org",
                State = "Inactive",
                ApplicationUser = user,
            };

            user.Wearables.Add(dbModel);

            var expected_wearable = new WearableModel(dbModel, true);

            // Act
            uut = new UserModel(user, true);

            // Assert
            Assert.Multiple(() =>
                {
                    Assert.AreEqual(user.ProfileImageUrl, uut.ProfileImageUrl);
                    Assert.AreEqual(user.UserName, uut.Username);
                    Assert.AreEqual(user.Id, uut.UserId);
                    Assert.AreEqual(null, uut.Address);
                    Assert.AreEqual(user.FirstName, uut.FirstName);
                    Assert.AreEqual(user.LastName, uut.LastName);
                    Assert.AreEqual(user.Email, uut.ContactInfo);
                    Assert.AreEqual(1, uut.Wearables.Count);
                    Assert.AreEqual(expected_wearable.Title, uut.Wearables[0].Title);
                    Assert.AreEqual(expected_wearable.Description, uut.Wearables[0].Description);
                    Assert.AreEqual(expected_wearable.ID, uut.Wearables[0].ID);
                    Assert.AreEqual(expected_wearable.State, uut.Wearables[0].State);
                });
        }
    }
}
