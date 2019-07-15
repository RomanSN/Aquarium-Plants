using AquaMarket.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


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
        [Required]
        public string Location { get; set; }


        //environment
        [Required]
        public string Light { get; set; }
        [Required]
        public int? MinTemp { get; set; }
        [Required]
        public int? MaxTemp { get; set; }
        [Required]
        [UIHint("NullablePhTemplate")]
        [Range(typeof(decimal), "1,0", "15,0", ErrorMessage = "Min value is 1, max value is 15. Double separator is ','")]
        public decimal? MinPh { get; set; }
        [UIHint("NullablePhTemplate")]
        [Range(typeof(decimal), "1,0", "15,0", ErrorMessage = "Min value is 1, max value is 15. Double separator is ','")]
        public decimal? MaxPh { get; set; }
        [Required]
        [UIHint("NullableGhTemplate")]
        [Range(typeof(decimal), "1,0", "15,0", ErrorMessage = "Min value is 1, max value is 15. Double separator is ','")]
        public decimal? MinGh { get; set; } 
        [Required]
        [UIHint("NullableGhTemplate")]
        [Range(typeof(decimal), "1,0", "15,0", ErrorMessage = "Min value is 1, max value is 15. Double separator is ','")]
        public decimal? MaxGh { get; set; }
        [Required]
        public string Usage { get; set; }


        //characteristics
        [Required]
        public int? Hight { get; set; }
        [Required]
        public string GrowthSpeed { get; set; }

        [Required]
        public string Coloration { get; set; }

        [Required]
        public string Сomplexity { get; set; }

        [Required]
        public string Area { get; set; }

        //taxonomy
        [Required]
        public string  OriginCountry { get; set; }  

        [Required]
        public string Type { get; set; }
        
        [Required]
        public int SpeciesId { get; set; }
        [Required]
        public Species PlantSpecies { get; set; }

        //resources

        //entity for DB
        public virtual IList<File> Files { get; set; }
        //entity for server
        //[FileType("JPG,JPEG,PNG")]   //Can be used instead of attribute accept="image/*"
        //[FileExists] //Actually unic File name = File name + Date.DateTime.Now (in controller)
        public IEnumerable<HttpPostedFileBase> Images { get; set; }


        public Plant()
        {
            Files = new List<File>();
        }
    }
}