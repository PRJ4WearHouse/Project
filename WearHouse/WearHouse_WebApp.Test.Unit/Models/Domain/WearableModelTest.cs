using NUnit.Framework;
using WearHouse_WebApp.Models.Domain;
using WearHouse_WebApp.Models.Entities;
using System.Collections.Generic;
using System;

namespace WearHouse_WebApp.Test.Unit.Models
{
    public class WearableModelTest
    {
        private WearableModel uut;

        [SetUp]
        public void Setup()
        {
            uut = new WearableModel();
        }

        [TestCase("Inactive", WearableState.Inactive)]
        [TestCase("Selling", WearableState.Selling)]
        [TestCase("Giving", WearableState.Giving)]
        [TestCase("Renting", WearableState.Renting)]
        [TestCase("Borrowing", WearableState.Borrowing)]
        public void ConvertingStateFromString_ConverterConstructor_StateSaved(string StateAsString,
            WearableState StateAsEnum)
        {
            //Arrange
            var dbModel = new dbWearable() { State = StateAsString };

            //Act
            uut = new WearableModel(dbModel, false);

            //Assert
            Assert.AreEqual(uut.State, StateAsEnum);
        }

        /// Null-test for Converting State From String
        [Test]
        public void ConvertingStateFromString_Constructor_ThrowsWhenNull()
        {
            // Arrange 
            var dbModel = new dbWearable() { State = null };

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new WearableModel(dbModel, false));
        }

        [Test]
        public void ConvertingUserModelFromApplicationUser()
        {
            // Arrange
            var user = new ApplicationUser() { Id = "1", Email = "test@test.org" };

            var dbModel = new dbWearable()
            {
                Description = "Test",
                Title = "Title",
                State = "Inactive",
                UserId = "1",
                WearableId = 1,
                ApplicationUser = user,
                ImageUrls = "/test1\n/test2",
            };

            // Act
            uut = new WearableModel(dbModel, true);

            // Assert
            var expected_image_url_list = new List<string>() { "/test1", "/test2" };

            Assert.Multiple(() =>
                {
                    Assert.AreEqual(uut.State, WearableState.Inactive);
                    Assert.AreEqual(uut.Title, "Title");
                    Assert.AreEqual(uut.ID, 1);
                    Assert.AreEqual(uut.Owner.UserId, "1");
                    Assert.AreEqual(uut.Owner.ContactInfo, "test@test.org");
                    Assert.AreEqual(uut.Owner.Address, null);
                    Assert.AreEqual(uut.Description, "Test");
                    Assert.AreEqual(uut.ImageUrls.Count, 2);
                });
        }

        [Test]
        public void ConvertToDbWearable_Succeeds()
        {
            // Arrange
            var user = new ApplicationUser() { Id = "1", Email = "test@test.org" };

            var dbModel = new dbWearable()
            {
                Description = "Test",
                Title = "Title",
                State = "Inactive",
                UserId = "1",
                ApplicationUser = user,
                ImageUrls = "/test1\n/test2",
            };

            var expected = dbModel;

            // Act
            uut = new WearableModel(dbModel, true);
            var actual = uut.ConvertToDbWearable();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(expected.Description, actual.Description);
                Assert.AreEqual(expected.Title, actual.Title);
                Assert.AreEqual(expected.State, actual.State);
                Assert.AreEqual(expected.UserId, actual.UserId);
                Assert.AreEqual(user.Email, actual.UserContactInfo);
            });
        }

        [Test]
        public void ConvertToDbWearable_NoOwner_Throws()
        {
            // Arrange
            var user = new ApplicationUser() { Id = "1", Email = "test@test.org" };

            var dbModel = new dbWearable()
            {
                Description = "Test",
                Title = "Title",
                State = "Inactive",
                UserId = "1",
                ApplicationUser = user,
                ImageUrls = "/test1\n/test2",
            };

            uut = new WearableModel(dbModel, false);

            Assert.Throws<Exception>(() => uut.ConvertToDbWearable());
        }
    }
}
