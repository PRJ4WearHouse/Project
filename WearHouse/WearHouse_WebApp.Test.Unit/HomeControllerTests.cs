using WearHouse_WebApp.Controllers;
using WearHouse_WebApp.Models.Entities;
using Microsoft.AspNetCore.Identity;
using WearHouse_WebApp.Data;
using NUnit.Framework;
using NSubstitute;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Moq;
using System.Threading;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using WearHouse_WebApp.Models.Domain;
using System.Linq;

namespace WearHouse_WebApp.Test.Unit
{
    class HomeControllerTests
    {
        
        HomeController controller;
        UserManager<ApplicationUser> userManager;
        Mock<ApplicationDbContext> dbContext;

        [SetUp]
        public void SetUp()
        {
            userManager = CreateUserManager();

            // var options = Substitute.For<DbContextOptions<ApplicationDbContext>>();

            // var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            //    .UseSqlServer("Server=den1.mssql8.gear.host;Database=wearhousedb;UID=wearhousedb;PWD=\tKw1TAaAZu~~m;")
            //    .Options;

            dbContext = new Mock<ApplicationDbContext>();
            //dbContext.Setup(db => db.dbWearables).Returns

            //controller = new(userManager, dbContext.Object);
        }

        [Test]
        public void Index_ReturnsLandingPageElementsCorrectly()
        {
            // Mock some dbWearables for dbcontext/dbSet
            var dbwearablesMock = CreateDbSetMock(GetFakeListOfDbWearables());
            dbContext.Setup(db => db.dbWearables).Returns(dbwearablesMock.Object);

            // Call Index and save return value
            IActionResult actionResult = controller.Index();

            Assert.True(actionResult.GetType() == typeof(ViewResult));
            
            
        }

        private IEnumerable<dbWearable> GetFakeListOfDbWearables()
        {
            var dbWearables = new List<dbWearable>
        {
                new dbWearable{Title = "Mock1"},
                new dbWearable{Title = "Mock2"},
                new dbWearable{Title = "Mock3"}
        };

            return dbWearables;
        }

        private static Mock<DbSet<T>> CreateDbSetMock<T>(IEnumerable<T> elements) where T : class
        {
            var elementsAsQueryable = elements.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());

            return dbSetMock;
        }

        UserManager<ApplicationUser> CreateUserManager()
        {
            Mock<IUserPasswordStore<ApplicationUser>> userPasswordStore = new Mock<IUserPasswordStore<ApplicationUser>>();
            userPasswordStore.Setup(s => s.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(IdentityResult.Success));

            var options = new Mock<IOptions<IdentityOptions>>();
            var idOptions = new IdentityOptions();

            //this should be keep in sync with settings in ConfigureIdentity in WebApi -> Startup.cs
            idOptions.Lockout.AllowedForNewUsers = false;
            idOptions.Password.RequireDigit = true;
            idOptions.Password.RequireLowercase = true;
            idOptions.Password.RequireNonAlphanumeric = true;
            idOptions.Password.RequireUppercase = true;
            idOptions.Password.RequiredLength = 8;
            idOptions.Password.RequiredUniqueChars = 1;

            idOptions.SignIn.RequireConfirmedEmail = false;

            // Lockout settings.
            idOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            idOptions.Lockout.MaxFailedAccessAttempts = 5;
            idOptions.Lockout.AllowedForNewUsers = true;


            options.Setup(o => o.Value).Returns(idOptions);
            var userValidators = new List<IUserValidator<ApplicationUser>>();
            UserValidator<ApplicationUser> validator = new UserValidator<ApplicationUser>();
            userValidators.Add(validator);

            var passValidator = new PasswordValidator<ApplicationUser>();
            var pwdValidators = new List<IPasswordValidator<ApplicationUser>>();
            pwdValidators.Add(passValidator);
            var userManager = new UserManager<ApplicationUser>(userPasswordStore.Object, options.Object, new PasswordHasher<ApplicationUser>(),
                userValidators, pwdValidators, new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(), null,
                new Mock<ILogger<UserManager<ApplicationUser>>>().Object);

            return userManager;
        }
    }
}
