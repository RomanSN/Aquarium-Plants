using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AquaMarket.Models
{
    public class PlantViewModel
    {
        public IEnumerable<Plant> Plants { get; set; }

        public SelectList Areas { get; set; }
        public SelectList Light { get; set; }
        public SelectList Complexity { get; set; }
        
        public PageInfo PageInfo { get; set; }

    }

   
}