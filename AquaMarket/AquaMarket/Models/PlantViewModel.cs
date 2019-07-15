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

        public SelectList Locations { get; set; }
        public SelectList Positions { get; set; }
        public SelectList Light { get; set; }
        public SelectList GrowthSpeed { get; set; }
        public SelectList Hight { get; set; }
        public SelectList Temp { get; set; }
        public SelectList Ph { get; set; }
        public SelectList Gh { get; set; }
        public SelectList Kh { get; set; }

        public PageInfo PageInfo { get; set; }

    }

   
}