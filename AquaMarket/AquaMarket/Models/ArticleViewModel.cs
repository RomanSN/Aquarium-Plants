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
        public PageInfo PageInfo { get; set; }

        public ArticleViewModel()
        {
            Article = new Article();
            Sections = Article.Sections;
            PageInfo = new PageInfo();
        }

    }
}