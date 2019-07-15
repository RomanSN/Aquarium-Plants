﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AquaMarket.Models
{
    public class Species
    {
        public int Id { get; set; }  

        public string SpeciesName { get; set; }

        public string Description { get; set; }
        [Required]
        public int FamilyId { get; set; }
        [Required]
        public Family PlantFamily { get; set; }

        public HashSet<int> PlantsIds { get; set; }

        public HashSet<Plant> Plants { get; set; }

        public Species()
        {
            PlantsIds = new HashSet<int>();
            Plants = new HashSet<Plant>();
        }
    }
}