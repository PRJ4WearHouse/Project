using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using WearHouse_WebApp.Data;
using WearHouse_WebApp.Models.Entities;
using WearHouse_WebApp.Persistence.Interfaces;
using WearHouse_WebApp.Persistence.Repositories;
using WearHouse_WebApp.Test.Unit.HelperFunctions;

namespace WearHouse_WebApp.Test.Unit.PersistenceTests
{
    [TestFixture]
    class WearablesRepositoryTests
    {
        private IUserRepository UserRepos;
        private IWearableRepository WearableRepos;
        
        [SetUp]
        public void SetUp()
        {
            // Creates a new in memory database
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "database_test_name")
                .Options;
            var inMemDbContext = new ApplicationDbContext(options);
            
            // Creates a preset dbset
            var mockDbSet = Fakes.GetQueryableMockDbSet<dbWearable>(fakeDbWearables);
            inMemDbContext.dbWearables = mockDbSet;

            // Creates a wearableRepository using the in memory dbcontext
            WearableRepos = new WearableRepository(inMemDbContext);
        }

        [Test]
        public void GetAllWearablesWithUsers_ReturnsIntendedValues()
        {
            var r = WearableRepos.GetWearableWithComments(0).Result;

            Assert.IsTrue(r == fakeDbWearables[0]);
        }

        public List<dbWearable> fakeDbWearables = new List<dbWearable>
        {
            new dbWearable { Title = "Mock1" },
            new dbWearable { Title = "Mock2" },
            new dbWearable { Title = "Mock3" }
        };
    }

}
