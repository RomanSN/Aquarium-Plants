using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AquaMarket.Models
{
    public class PlantViewModel
    {
        public IEnumerable<Plant> Plants { get; set; }

        Plant Plant { get; set; }
        [Required]
        public int? Temp { get; set; }
        [Required]
        public decimal? Ph { get; set; }
        [Required]
        public decimal? Gh { get; set; }

        public string Area { get; set; }
        public string Light { get; set; }
        public string Complexity { get; set; }

        public SelectList Areas { get; set; }
        public SelectList LightRequirements { get; set; }
        public SelectList ComplexityValues { get; set; }

        public PlantViewModel()
        {
            Plant = new Plant();
            Areas = Plant.Areas;
            LightRequirements = Plant.LightRequirements;
            ComplexityValues = Plant.PlantComplexityValues;
        }
        
    }

   
}