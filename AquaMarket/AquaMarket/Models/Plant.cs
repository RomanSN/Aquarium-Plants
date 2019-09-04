using AquaMarket.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;


namespace AquaMarket.Models
{
    public class Plant
    {

        //metadata
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Required]
        public string PlantName { get; set; }
        [Required]
        public string Description { get; set; }

        //environment
        [Required]
        public string Light { get; set; } //(w/100 litres)
        [Required]
        [Range(typeof(int), "10", "35", ErrorMessage = "Min value is 10, max value is 35.")]
        public int? MinTemp { get; set; }
        [Required]
        [Range(typeof(int), "10", "35", ErrorMessage = "Min value is 10, max value is 35.")]
        public int? MaxTemp { get; set; }
        [Required]
        //[UIHint("NullableMinPhTemplate")]
        [Range(typeof(decimal), "1.0", "15.0", ErrorMessage = "Min value is 1, max value is 15. Double separator is '.'")]
        public decimal? MinPh { get; set; }
        //[UIHint("NullableMaxPhTemplate")]
        [Range(typeof(decimal), "1.0", "15.0", ErrorMessage = "Min value is 1, max value is 15. Double separator is '.'")]
        public decimal? MaxPh { get; set; }
        [Required]
        //[UIHint("NullableMinGhTemplate")]
        [Range(typeof(decimal), "1.0", "30.0", ErrorMessage = "Min value is 1, max value is 30. Double separator is '.'")]
        public decimal? MinGh { get; set; }
        [Required]
        //[UIHint("NullableMaxGhTemplate")]
        [Range(typeof(decimal), "1.0", "30.0", ErrorMessage = "Min value is 1, max value is 30. Double separator is '.'")]
        public decimal? MaxGh { get; set; }


        //characteristics
        [Required]
        public int? Hight { get; set; }
        [Required]
        public string GrowthSpeed { get; set; }

        [Required]
        public string Coloration { get; set; }

        [Required]
        public string Area { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Usage { get; set; }

        [Required]
        public virtual string Сomplexity { get; set; }

        //taxonomy
        [Required]
        public string OriginCountry { get; set; }

        [Required]
        public virtual string PlantType { get; set; }

        [Required]
        [Display(Name = "Species")]
        public int? SpeciesId { get; set; }

        public virtual Species PlantSpecies { get; set; }

        //resources
        public int? FileId { get; set; }
        //entity for store
        public virtual File File { get; set; }
        //entity for download
        //[FileType("JPG,JPEG,PNG")]   //Custom validator be used instead of attribute accept="image/*" 
        //[FileExists] //Actually unic File name = File name + Date.DateTime.Now (in controller)
        [NotMapped]
        public HttpPostedFileBase Image { get; set; }


        //select lists for editors
        public  SelectList GrowthSpeedValues;
        public  SelectList Areas;
        public  SelectList Locations;
        public  SelectList PlantUsages;
        public  SelectList LightRequirements;
        public  SelectList PlantComplexityValues;
        public  SelectList Types;
        public  SelectList Colorations;


        public Plant()
        {
            GrowthSpeedValues = new SelectList(new List<string>(){
                    "hight",
                    "middle",
                    "low"
            });
            Areas = new SelectList(new List<string>() {
                "background",
                "midground",
                "foreground"
            });
            Locations = new SelectList(new List<string>() {
                "soil",
                "water",
                "surface"
            });
            PlantUsages = new SelectList(new List<string>() {
                "aquascaping",
                "cichlid aquarium",
                "community aquarium",
                "nano cube",
                "discus fish aquarium",
                "solitary aquarium"
            });
            LightRequirements = new SelectList(new List<string>() {
                "semi shady to sunny",
                "shady",
                "shady to semi shady",
                "shady to sunny",
                "sunny"
            });
            PlantComplexityValues = new SelectList(new List<string>() {
                "demanding",
                "average",
                "easy"
            });
            Types = new SelectList(new List<string>() {
                "moss",
                "tuber",
                "runners",
                "fern",
                "arrangement",
                "rhizome",
                "rosette",
                "floating",
                "stem"
            });
            Colorations = new SelectList(new List<string>() {
                "light_green",
                "green",
                "dark_green",
                "red",
                "brown"
            });
        }
      
    }
}