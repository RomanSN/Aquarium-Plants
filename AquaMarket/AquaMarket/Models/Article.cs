using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AquaMarket.Models
{
    public class Article
    {
        public int Id { get; set; }
        [Required]
        public string Section { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public int? Likes { get; set; }
        public int? FileId { get; set; }
        //entity for store
        public virtual File File { get; set; }
        //entity for download
        [NotMapped]
        public HttpPostedFileBase Image { get; set; }

        public SelectList Sections = new SelectList(new List<string>(){
                    "Section_1",
                    "Section_2",
                    "Section_3"
         });

        public Article()
        {
            
        }
    }
}