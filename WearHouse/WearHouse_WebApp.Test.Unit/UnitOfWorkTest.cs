using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WearHouse_WebApp.Data;
using WearHouse_WebApp.Models.Entities;
using WearHouse_WebApp.Persistence;
using WearHouse_WebApp.Persistence.Interfaces;

namespace WearHouse_WebApp.Test.Unit
{
    [TestFixture]
    class UnitOfWorkTest
    {
        private UnitOfWork uut;
        private IAzureImageStorage imageStorage;
        private IUserRepository userRepository;
        //private Mock<ApplicationDbContext> dbMock;

        [SetUp]
        public void SetUp()
        {
            


            //uut = new UnitOfWork();
        }
    }
}
