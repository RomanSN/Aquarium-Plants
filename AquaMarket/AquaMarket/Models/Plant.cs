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
        [UIHint("NullableMinPhTemplate")]
        [Range(typeof(decimal), "1,0", "15,0", ErrorMessage = "Min value is 1, max value is 15. Double separator is ','")]
        public decimal? MinPh { get; set; }
        [UIHint("NullableMaxPhTemplate")]
        [Range(typeof(decimal), "1,0", "15,0", ErrorMessage = "Min value is 1, max value is 15. Double separator is ','")]
        public decimal? MaxPh { get; set; }
        [Required]
        [UIHint("NullableMinGhTemplate")]
        [Range(typeof(decimal), "1,0", "30,0", ErrorMessage = "Min value is 1, max value is 15. Double separator is ','")]
        public decimal? MinGh { get; set; } 
        [Required]
        [UIHint("NullableMaxGhTemplate")]
        [Range(typeof(decimal), "1,0", "30,0", ErrorMessage = "Min value is 1, max value is 15. Double separator is ','")]
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
        public string  OriginCountry { get; set; }  

        [Required]
        public virtual string PlantType { get; set; }
        
        [Required]
        public int? SpeciesId { get; set; }
        
        public virtual Species PlantSpecies { get; set; }

        //resources
        public int? FileId { get; set; }
        //entity for store
        public virtual File File { get; set; }
        //entity for download
        //[FileType("JPG,JPEG,PNG")]   //Can be used instead of attribute accept="image/*"
        //[FileExists] //Actually unic File name = File name + Date.DateTime.Now (in controller)
        [NotMapped]
        public HttpPostedFileBase Image { get; set; }

        public Plant()
        {
            
        }

        
       
        
        public enum GrowthSpeedValues        
        {
            hight,
            middle,
            low
        }
        public enum Areas     
        {
            background,
            midground,
            foreground
        }
        public enum Locations  
        {
            soil,
            water,
            surface   
        }
        
        public static IEnumerable<string> PlantUsage  = new List<string>(){
            "Aquascaping",
            "cichlid aquarium",
            "community aquarium",
            "nano cube",
            "discus fish aquarium",
            "solitary aquarium"
        };

        public static IEnumerable<string> LightRequirements = new List<string>(){
            "semi shady to sunny",
            "shady",
            "shady to semi shady",
            "shady to sunny",
            "sunny"
            };

        public enum PlantComplexity
        {
            demanding,
            average,
            easy
        }

        public enum Types
        {
            moss,
            tuber,
            runners,
            fern,
            arrangement,
            rhizome,
            rosette,
            floating,   
            stem
        }
        public enum Colorations
        {
            light_green,
            green,
            dark_green,
            red,
            brown
        }
    }
}