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
using PagedList;
using System.Threading;

namespace AquaMarket.Controllers
{
    public class ArticlesController : Controller
    {
        private ArticlesRepo repo = new ArticlesRepo();

        [HttpGet]
        public async Task<ActionResult> Index(ArticleViewModel viewModel,  int pageInd = 1)
        {
            ArticleViewModel avm = new ArticleViewModel();
            IEnumerable<Article> result = await repo.Index();
            if (viewModel.Section != null)
            {
                result = result.Where(p => p.Section == viewModel.Section).ToList();
                avm.Section = viewModel.Section;
            }
            else
            {
                avm.Section = "All sections";
            }

            int pSize = 5;
            var responsecookies = System.Web.HttpContext.Current.Response.Cookies;
            if (responsecookies["ArticlePageIndex"] == null)
            {
                responsecookies.Set(new HttpCookie("ArticlePageIndex"));
            }
            responsecookies["ArticlePageIndex"].Value = pageInd.ToString();
            result = result.ToPagedList(pageInd, pSize);
            
            avm.Articles = result;

            return View(avm);
        }

        [HttpGet]
        public async Task<JsonResult> Autocomplete(string term)
        {
            return await repo.Autocomplete(term);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = await repo.Details(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            ViewBag.FileIds = new SelectList(await repo.GetFiles(), "Id", "FileName");
            return View(new Article());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Section,Title,Content,Likes,Image")] Article article)
        {
            if (ModelState.IsValid)
            {
                
                Article created = await repo.Create(article);
                return RedirectToAction("Details", new { id = created.Id });
            }

            ViewBag.FileId = new SelectList(await repo.GetFiles(), "Id", "FileName");
            return View(article);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = await repo.Edit(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Section,Title,Content,Likes,FileId, Image")] Article article)
        {
            if (ModelState.IsValid)
            {
                await repo.Edit(article);
                return RedirectToAction("Details", new { id = article.Id });
            }
            return View(article);
        }

        [HttpGet]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Article deletedArticle = await repo.DeleteConfirmed(id);
            return RedirectToAction("Index", new {Section = deletedArticle.Section});
        }
        [HttpGet]
        public async Task<ActionResult> DeleteFileConfirmed(int fileId)
        {
            await repo.DeleteFileConfirmed(fileId);
            return PartialView();
        }
        [ChildActionOnly]
        public ActionResult Sections()
        {
            Article article = new Article();

            return PartialView("_ArticleSectionsPartial", article);
        }
    }
}
