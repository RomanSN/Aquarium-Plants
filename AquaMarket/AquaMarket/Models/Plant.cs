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
        public string Light { get; set; } // semi-shady to sunny, shady, shady to semi-shady, shady to sunny, sunny (w/100 litres)
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
        public string GrowthSpeed { get; set; } //hight, 

        [Required]
        public string Coloration { get; set; }

        [Required]
        public string Area { get; set; } //Background, midground, foreground

        [Required]
        public string Location { get; set; } //Location in tank: soil, water, water surface

        [Required]
        public string Usage { get; set; } //Aquascaping, cichlid aquarium, community aquarium, nano cube, discus fish aquarium, solitary aquarium 

        [Required]
        public virtual string Сomplexity { get; set; } //demanding, average, easy

        //taxonomy
        [Required]
        public string  OriginCountry { get; set; }  

        [Required]
        public virtual string PlantType { get; set; } //moss, tuber, runners (побег), fern, arrangement,rhizome, rosette,floating plant, stem plant
        
        [Required]
        public int? SpeciesId { get; set; }
        //[Required]
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
            File = new File();
           
        }
    }
}