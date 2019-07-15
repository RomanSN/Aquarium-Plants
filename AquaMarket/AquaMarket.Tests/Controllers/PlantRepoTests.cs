using Microsoft.VisualStudio.TestTools.UnitTesting;
using AquaMarket.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AquaMarket.Models;


namespace AquaMarket.Controllers.Tests
{
    [TestClass()]
    public class PlantRepoTests
    {
        [TestMethod()]
        public  void  AutocompleteTest()
        {
            PlantRepo repo = new PlantRepo();

            var plants =  repo.Autocomplete("Plant");

            Assert.IsNotNull(plants);
        }
    }
}