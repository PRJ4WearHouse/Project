using System;
using System.Collections.Generic;
using NUnit.Framework;
using WearHouse_WebApp.Models.Domain;
using WearHouse_WebApp.Models.Entities;

namespace WearHouse_WebApp.Test.Unit.Models
{
    [TestFixture]
    public class dbCommentsTest
    {
        private dbComments uut;

        [SetUp]
        public void SetUp()
        {
            uut = new dbComments();
        }

        [Test]
        public void DefaultConstructor_ConvertToDomainCommentModel_AllNull_Succeeds()
        {
            // Arrange takes place in SetUp()

            // Act
            var actual = uut.ConvertToCommentModel();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(new DateTime(), actual.Moment);
                Assert.IsNull(actual.Comment);
                Assert.IsNull(actual.Author);
                Assert.AreEqual(0, actual.WearableId);
            });
        }

        [Test]
        public void AmbiguousConstructor_AuthorIsNull_ConvertToDomainCommentModel_Succeeds()
        {
            // Arrange
            uut = new dbComments()
            {
                Comments = "Test Comment",
                Moment = DateTime.Today,
            };

            // Act
            var actual = uut.ConvertToCommentModel();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual("Test Comment", actual.Comment);
                Assert.AreEqual(DateTime.Today, actual.Moment);
                Assert.IsNull(actual.Author);
            });
        }

        [Test]
        public void AmbiguousConstructor_AuthorIsInitialized_ConvertToDomainCommentModel_Succeeds()
        {
            // Arrange
            uut = new dbComments()
            {
                Comments = "Test Comment",
                Moment = DateTime.Today,
                Author = new ApplicationUser()
                {
                    Location = "San Francisco",
                    FirstName = "Clever",
                    LastName = "Tester",
                    ProfileImageUrl = "profile.png",
                    Wearables = new List<dbWearable>(),
                    UserName = "Tester",
                    Id = "1",
                },
            };

            // Act
            var actual = uut.ConvertToCommentModel();

            // Assert
            Assert.Multiple(() =>
                {
                    Assert.AreEqual("Test Comment", actual.Comment);
                    Assert.AreEqual(DateTime.Today, actual.Moment);
                    Assert.AreEqual("profile.png", actual.Author.ProfileImageUrl);
                    Assert.AreEqual("1", actual.Author.UserId);
                    Assert.AreEqual("Clever", actual.Author.FirstName);
                    Assert.AreEqual("Tester", actual.Author.LastName);
                    Assert.AreEqual("Tester", actual.Author.Username);
                });
        }
    }
}
