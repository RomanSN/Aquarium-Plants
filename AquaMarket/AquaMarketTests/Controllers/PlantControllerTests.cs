using Microsoft.VisualStudio.TestTools.UnitTesting;
using AquaMarket.Controllers;
using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaMarket.Controllers.Tests
{
    [TestClass()]
    public class PlantControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            PlantController contr = new PlantController();
            var result =  contr.Index(null, null, null, null, null, null, null, null);
            Assert.IsTrue(result != null);
        }
    }
}