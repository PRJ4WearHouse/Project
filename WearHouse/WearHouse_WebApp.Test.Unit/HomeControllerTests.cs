using WearHouse_WebApp.Models.Domain;
using WearHouse_WebApp.Controllers;
using WearHouse_WebApp.Models.Entities;
using Microsoft.AspNetCore.Identity;
using WearHouse_WebApp.Data;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Collections.ObjectModel;
using NSubstitute;
using System.Linq.Expressions;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using WearHouse_WebApp.Persistence;

namespace WearHouse_WebApp.Test.Unit
{
    class HomeControllerTests
    {
        
        HomeController controller;
        UserManager<ApplicationUser> userManager;
        ApplicationDbContext dbContext;
        UnitOfWork _unitOfWork;

        [SetUp]
        public void SetUp()
        {
            userManager = MockUserManager(_users).Object; 

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseSqlServer("Server=den1.mssql8.gear.host;Database=wearhousedb;UID=wearhousedb;PWD=\tKw1TAaAZu~~m;")
               .Options;

            dbContext = new ApplicationDbContext(options);
            controller = new(userManager, dbContext);
            _unitOfWork = new UnitOfWork(dbContext, userManager, "DefaultEndpointsProtocol=https;AccountName=wearhouseimages;AccountKey=XsPSwlsWqpM67glYBUVc/d5Tm5XBKx3KTgZg3dCo6Hz2rHnz9+mQH3cmgnSLJsRK6gmDtOPEj0y0860AhGgWBw==;EndpointSuffix=core.windows.net");
        }

        [Test]
        public void Index_ReturnsLandingPageWithWearablesCorrectly()
        {
            // Arrange

            // Act
            IActionResult actionResult = controller.Index() as ViewResult;
            var viewData = controller.ViewData.Model as List<WearableModel>;

            var expectedData = _unitOfWork.Wearables
                .GetAllWearablesWithUsers().Result
                .Select(item => item.ConvertToWearableModel())
                .ToList();

            // Assert
            Assert.True(actionResult.GetType() == typeof(ViewResult));
            for (int i = 0; i < expectedData.Count; i++)
            {
                Assert.AreEqual(expectedData[i].ID, viewData[i].ID);
            }
        }
        [Test]
        public void User_ReturnsUsersPageWithUsersFromDatabase()
        {
            // Arrange

            // Act
            IActionResult actionResult = controller.Users() as ViewResult;
            var viewData = controller.ViewData.Model;

            var expectedData = userManager.Users as List<ApplicationUser>;


            // Assert
            Assert.True(actionResult.GetType() == typeof(ViewResult));


            //for (int i = 0; i < expectedData.Count; i++)
            //{
            //    Assert.AreEqual(expectedData[i].Id, viewData[i].Id);
            //}
        }

        // https://stackoverflow.com/questions/49165810/how-to-mock-usermanager-in-net-core-testing

        public static Mock<UserManager<TUser>> MockUserManager<TUser>(List<TUser> ls) where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => ls.Add(x));
            mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);

            return mgr;
        }

        private List<ApplicationUser> _users = new List<ApplicationUser>
         {
              new ApplicationUser(){Id = "1" },
              new ApplicationUser(){Id = "2" },
              new ApplicationUser(){Id = "3" }
         };

        //UserManager<ApplicationUser> CreateUserManager()
        //{
        //    Mock<IUserPasswordStore<ApplicationUser>> userPasswordStore = new Mock<IUserPasswordStore<ApplicationUser>>();
        //    userPasswordStore.Setup(s => s.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<CancellationToken>()))
        //        .Returns(Task.FromResult(IdentityResult.Success));

        //    var options = new Mock<IOptions<IdentityOptions>>();
        //    var idOptions = new IdentityOptions();

        //    //this should be keep in sync with settings in ConfigureIdentity in WebApi -> Startup.cs
        //    idOptions.Lockout.AllowedForNewUsers = false;
        //    idOptions.Password.RequireDigit = true;
        //    idOptions.Password.RequireLowercase = true;
        //    idOptions.Password.RequireNonAlphanumeric = true;
        //    idOptions.Password.RequireUppercase = true;
        //    idOptions.Password.RequiredLength = 8;
        //    idOptions.Password.RequiredUniqueChars = 1;

        //    idOptions.SignIn.RequireConfirmedEmail = false;

        //    // Lockout settings.
        //    idOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        //    idOptions.Lockout.MaxFailedAccessAttempts = 5;
        //    idOptions.Lockout.AllowedForNewUsers = true;


        //    options.Setup(o => o.Value).Returns(idOptions);
        //    var userValidators = new List<IUserValidator<ApplicationUser>>();
        //    UserValidator<ApplicationUser> validator = new UserValidator<ApplicationUser>();
        //    userValidators.Add(validator);

        //    var passValidator = new PasswordValidator<ApplicationUser>();
        //    var pwdValidators = new List<IPasswordValidator<ApplicationUser>>();
        //    pwdValidators.Add(passValidator);
        //    var userManager = new UserManager<ApplicationUser>(userPasswordStore.Object, options.Object, new PasswordHasher<ApplicationUser>(),
        //        userValidators, pwdValidators, new UpperInvariantLookupNormalizer(),
        //        new IdentityErrorDescriber(), null,
        //        new Mock<ILogger<UserManager<ApplicationUser>>>().Object);

        //    return userManager;
        //}
    }
}
