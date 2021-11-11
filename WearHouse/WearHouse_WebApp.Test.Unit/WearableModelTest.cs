using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using NUnit.Framework;
using WearHouse_WebApp.Models.Domain;
using WearHouse_WebApp.Models.Entities;

namespace WearHouse_WebApp.Test.Unit
{
    public class WearableModelTest
    {
        private WearableModel uut;
        

        [SetUp]
        public void Setup()
        {
            uut = new WearableModel();
        }

        [TestCase(WearableState.Inactive, "Inactive")]
        [TestCase(WearableState.Selling, "Selling")]
        [TestCase(WearableState.Giving, "Giving")]
        public void SavingStateAsString_SetProperty_StateSavedAsString(WearableState StateAsEnum, string StateAsString)
        {
            uut.State = StateAsEnum;
            Assert.That(uut.dbModel.State, Is.EqualTo(StateAsString));
        }

        //OBS Need null tests
        [TestCase("Inactive", WearableState.Inactive)]
        [TestCase("Selling", WearableState.Selling)]
        [TestCase("Giving", WearableState.Giving)]
        public void ConvertingStateFromString_ConverterConstructor_StateSaved(string StateAsString,
            WearableState StateAsEnum)
        {
            //Arrange
            var dbModel = new dbWearable() { State = StateAsString };

            //Act
            uut = new WearableModel(dbModel);

            //Assert
            Assert.That(uut.State, Is.EqualTo(StateAsEnum));
        }
    }
}