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

    }
}
