using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AquaMarket.Models;

namespace AquaMarket.Controllers
{
    public class ArticlesRepo : Controller
    {
        private AquaDBContext db = new AquaDBContext();

        public async Task<IEnumerable<Article>> Index()
        {
            var articles = await db.Articles.Include(a => a.File).ToListAsync();
            return articles;
        }

        public async Task<Article> Details(int? id)
        {
            Article article = await db.Articles.FindAsync(id);
            return article;
        }

        public async Task<Article> Create(Article article)
        {
            db.Articles.Add(article);
            await db.SaveChangesAsync();

            ICollection<Article> allArticles = await db.Articles.ToListAsync();
            Article createdArticle = allArticles.Last();

            if (article.Image != null)
            {
                string fileName = System.IO.Path.GetFileName(article.Image.FileName);
                string path = System.Web.HttpContext.Current.Server.MapPath("~/Files/" + fileName);
                article.Image.SaveAs(path);
                File NewFile = new File
                {
                    FileName = fileName,
                    FileType = System.IO.Path.GetExtension(article.Image.FileName),
                    ArticleId = createdArticle.Id
                };
                db.Files.Add(NewFile);
                await db.SaveChangesAsync();
            }
            ICollection<File> allfiles = await db.Files.ToListAsync();
            File createdfile = allfiles.Last();
            createdArticle.FileId = createdfile.Id;
            db.Entry(createdArticle).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return createdArticle;
        }

        public async Task<Article> Edit(int? id)
        {
            Article article = await db.Articles.FindAsync(id);
            return article;
        }

        public async Task Edit(Article article)
        {

            if (article.Image != null)
            {
                string fileName = System.IO.Path.GetFileNameWithoutExtension(article.Image.FileName) + DateTime.Now.Millisecond.ToString() + System.IO.Path.GetExtension(article.Image.FileName);
                string path = System.Web.HttpContext.Current.Server.MapPath("~/Files/" + fileName);
                article.Image.SaveAs(path);
                File NewFile = new File
                {
                    FileName = fileName,
                    FileType = System.IO.Path.GetExtension(article.Image.FileName),
                    ArticleId = article.Id
                };
                db.Files.Add(NewFile);
                await db.SaveChangesAsync();
                ICollection<File> allfiless = await db.Files.ToListAsync();
                File created = allfiless.Last();
                article.FileId = created.Id;
            }

            db.Entry(article).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
       
        public async Task<Article> DeleteConfirmed(int id)
        {
            Article article = await db.Articles.FindAsync(id);
            db.Articles.Remove(article);
            await db.SaveChangesAsync();
            return article;
        }

        public async Task DeleteFileConfirmed(int id)
        {
            db = new AquaDBContext();
            File file = await db.Files.FindAsync(id);
            if (file != null)
            {
                db.Files.Remove(file);
                await db.SaveChangesAsync();
            }
           
            var path = System.Web.HttpContext.Current.Server.MapPath("~/Files/" + file.FileName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }

        public async Task<IEnumerable<File>> GetFiles()
        {
            return await db.Files.ToListAsync();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
