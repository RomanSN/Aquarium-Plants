using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AquaMarket.Models
{
    public class Family
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Display(Name = "Family")]
        public string Name { get; set; }

        public string Description { get; set; }

        public HashSet<int> SpeciesIds { get; set; }    

        public HashSet<Species> Species { get; set; }



        public Family()    
        {
            SpeciesIds = new HashSet<int>();
            Species = new HashSet<Species>();
        }
    }
}