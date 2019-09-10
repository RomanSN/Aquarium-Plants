using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AquaMarket.Models
{
    public class ArticleViewModel
    {
        public IEnumerable<Article> Articles { get; set; }

        Article Article { get; set; }
        [Required]
        public string Section { get; set; }
        public SelectList Sections { get; set; }

        public ArticleViewModel()
        {
            Article = new Article();
            Sections = new SelectList(new List<string>(){
                    "All plants",
                    "Section_1",
                    "Section_2",
                    "Section_3"
            }); ;
        }

    }
}