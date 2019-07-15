using System.Collections.Generic;

namespace AquaMarket.Models
{
    public class Family
    {
        public int Id { get; set; }

        public string FamilyName { get; set; }

        public string Description { get; set; }

        public HashSet<int> SpeciesIds { get; set; }    

        public HashSet<Plant> Species { get; set; }



        public Family()    
        {
            SpeciesIds = new HashSet<int>();
            Species = new HashSet<Plant>();
        }
    }
}