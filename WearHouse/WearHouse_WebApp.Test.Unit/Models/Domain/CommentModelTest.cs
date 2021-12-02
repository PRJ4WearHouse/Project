using NUnit.Framework;
using System;
using WearHouse_WebApp.Models.Domain;
using WearHouse_WebApp.Models.Entities;

namespace WearHouse_WebApp.Test.Unit.Models
{

    [TestFixture]
    public class CommentModelTest
    {
        private CommentModel uut;

        [SetUp]
        public void SetUp()
        {
            uut = new CommentModel();
        }

        [Test]
        public void DefaultConstructor_ConvertToDbModel_Throws()
        {
            // Arrange takes place in SetUp()

            // Act & Assert
            Assert.Throws<Exception>(() => uut.ConvertToDbModel());
        }

        [Test]
        public void ParameterizedConstructor_Succeeds()
        {
            // Arrange
            var comment = "First Comment";
            var moment = DateTime.Today;
            var author = new UserModel()
            {
                ProfileImageUrl = "profile.png",
                Username = "Tester",
                UserId = "1",
                FirstName = "Clever",
                LastName = "Tester",
                ContactInfo = "clever@tester.org"
            };

            // Act
            uut = new CommentModel(comment, moment, author);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(comment, uut.Comment);
                Assert.AreEqual(moment, uut.Moment);
                Assert.AreEqual(author, uut.Author);
            });
        }

        [Test]
        public void ParameterizedConstructor_ConvertToDbModel_Succeeds()
        {
            // Arrange
            var comment = "First Comment";
            var moment = DateTime.Today;
            var author = new UserModel()
            {
                ProfileImageUrl = "profile.png",
                Username = "Tester",
                UserId = "1",
                FirstName = "Clever",
                LastName = "Tester",
                ContactInfo = "clever@tester.org"
            };

            uut = new CommentModel(comment, moment, author);

            // Act 
            var actual = uut.ConvertToDbModel();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(comment, actual.Comments);
                Assert.AreEqual(moment, actual.Moment);
                Assert.AreEqual("1", actual.userId);
            });
        }

        [Test]
        public void ParameterizedConstructor_AuthorIsNull_Succeeds()
        {
            // Arrange
            var comment = "First Comment";
            var moment = DateTime.Today;

            // Act
            uut = new CommentModel(comment, moment, null);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(comment, uut.Comment);
                Assert.AreEqual(moment, uut.Moment);
                Assert.AreEqual(null, uut.Author);
            });
        }

        [Test]
        public void ParameterizedConstructor_AuthorIsNull_ConvertToDbModel_Throws()
        {
            // Arrange
            var comment = "First Comment";
            var moment = DateTime.Today;

            uut = new CommentModel(comment, moment, null);

            // Act & Assert
            Assert.Throws<Exception>(() => uut.ConvertToDbModel());
        }

        [Test]
        public void DbCommentConstructor_AuthorIsNull_Succeeds()
        {
            // Arrange
            var dbComment = new dbComments()
            {
                Comments = "First Comment",
                Moment = DateTime.Today
            };

            // Act
            uut = new CommentModel(dbComment);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(dbComment.Comments, uut.Comment);
                Assert.AreEqual(dbComment.Moment, uut.Moment);
                Assert.AreEqual(null, uut.Author);
            });
        }

        [Test]
        public void DbCommentConstructor_AuthorIsValid_Succeeds()
        {
            // Arrange
            var dbComment = new dbComments()
            {
                Comments = "First Comment",
                Moment = DateTime.Today,
                Author = new ApplicationUser()
                {
                    ProfileImageUrl = "profile.png",
                    UserName = "Tester",
                    Id = "1",
                    FirstName = "Clever",
                    LastName = "Tester",
                    Email = "clever@tester.org"
                },
            };

            var expected_author = new UserModel()
            {
                ProfileImageUrl = "profile.png",
                Username = "Tester",
                UserId = "1",
                FirstName = "Clever",
                LastName = "Tester",
                ContactInfo = "clever@tester.org"
            };

            // Act
            uut = new CommentModel(dbComment);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(dbComment.Comments, uut.Comment);
                Assert.AreEqual(dbComment.Moment, uut.Moment);
                Assert.AreEqual(expected_author.UserId, uut.Author.UserId);
                Assert.AreEqual(expected_author.ProfileImageUrl, uut.Author.ProfileImageUrl);
                Assert.AreEqual(expected_author.Username, uut.Author.Username);
                Assert.AreEqual(expected_author.FirstName, uut.Author.FirstName);
                Assert.AreEqual(expected_author.LastName, uut.Author.LastName);
                Assert.AreEqual(expected_author.ContactInfo, uut.Author.ContactInfo);
            });
        }

        [Test]
        public void DbCommentConstructor_AuthorIsNull_ConvertToDbModel_Throws()
        {
            // Arrange
            var dbComment = new dbComments()
            {
                Comments = "First Comment",
                Moment = DateTime.Today
            };

            uut = new CommentModel(dbComment);

            // Act & Assert
            Assert.Throws<Exception>(() => uut.ConvertToDbModel());
        }

        [Test]
        public void DbCommentConstructor_AuthorIsValid_ConvertToDbModel_Succeeds()
        {
            // Arrange
            var dbComment = new dbComments()
            {
                Comments = "First Comment",
                Moment = DateTime.Today,
                Author = new ApplicationUser()
                {
                    ProfileImageUrl = "profile.png",
                    UserName = "Tester",
                    Id = "1",
                    FirstName = "Clever",
                    LastName = "Tester",
                    Email = "clever@tester.org"
                },
            };

            var expected = dbComment;
            expected.userId = "1";

            uut = new CommentModel(dbComment);

            // Act
            var actual = uut.ConvertToDbModel();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(expected.Comments, actual.Comments);
                Assert.AreEqual(expected.Moment, actual.Moment);
                Assert.AreEqual(expected.userId, actual.userId);
                Assert.AreEqual(null, actual.Author);
            });
        }
    }
}
