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
    public class ArticlesController : Controller
    {
        private ArticlesRepo repo = new ArticlesRepo();

        [HttpGet]
        public async Task<ActionResult> Index(ArticleViewModel viewModel)
        {
            ArticleViewModel avm = new ArticleViewModel();
            IEnumerable<Article> result = await repo.Index();
            if (viewModel.Section != null)
            {
                result = result.Where(p => p.Section == viewModel.Section).ToList();
                avm.Section = viewModel.Section;
            }

            var pIndex = viewModel.PageInfo.PageNumber ?? 1;

            avm.PageInfo = new PageInfo()
            {
                PageSize = 5,
                PageNumber = pIndex
            };

            return View(avm);
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Section,Content,Likes,FileId,Image")] Article article)
        {
            if (ModelState.IsValid)
            {
                await repo.Create(article);
                return RedirectToAction("Index");
            }

            ViewBag.FileId = new SelectList(await repo.GetFiles(), "Id", "FileName");
            return View(article);
        }

        // GET: Articles/Edit/5
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
        public async Task<ActionResult> Edit([Bind(Include = "Id,Section,Content,Likes,FileId, Image")] Article article)
        {
            if (ModelState.IsValid)
            {
                await repo.Edit(article);
                return RedirectToAction("Details", new { id = article.Id });
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await repo.DeleteConfirmed(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<ActionResult> DeleteFileConfirmed(int fileId)
        {
            await repo.DeleteFileConfirmed(fileId);
            return PartialView();
        }
       
    }
}
