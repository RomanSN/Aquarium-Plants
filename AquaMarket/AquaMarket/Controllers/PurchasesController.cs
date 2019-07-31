using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using AquaMarket.Models;
using PagedList;

namespace AquaMarket.Controllers
{
    public class PurchasesController : Controller
    {
        private PurchasesService service = new PurchasesService();

        // GET: Purchases
        public async Task<ActionResult> Index(int? pageIn, int? pageSi)
        {
            if (pageSi==null)
            {
                if (int.TryParse(HttpContext.Request.Cookies["ItemCount"].Value, out int result))
                {
                    pageSi = result;
                }
                else
                {
                    pageSi = 3;
                }
            }
           
            HttpContext.Response.Cookies["ItemCount"].Value = pageSi.ToString();

            var pageIndex = pageIn ?? 1;
            var pageSize = int.Parse(HttpContext.Response.Cookies["ItemCount"].Value);
            ICollection<Purchase> purchases = await service.Index();
            
            var PurchasesAsIPagedList = purchases.Reverse().ToPagedList(pageIndex,pageSize);

            return View(PurchasesAsIPagedList);
        }

        // GET: Purchases/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = await service.Details(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // GET: Purchases/Create
        public ActionResult Create()
        {
            ViewBag.Plants = service.CreatePlantSelectList();
            
            return View();
        }

        // POST: Purchases/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CustomerId,Amount,Status,SelectedPlantIDs")] Purchase purchase)
        {
            if (ModelState.IsValid&&purchase.SelectedPlantIDs!=null)
            {
                await service.Create(purchase);
                return RedirectToAction("Index");
            }
            ViewBag.Plants = service.CreatePlantSelectList();
            return View(purchase);
        }

       

        // GET: Purchases/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = await service.Edit(id);
           
            if (purchase == null)
            {
                return HttpNotFound();
            }

            int[] s = new int[purchase.Plants.Count];
            for (int i = 0; i < purchase.Plants.Count; i++)
            {
                s[i] = purchase.Plants.ElementAt(i).Id;
            }
            purchase.SelectedPlantIDs = s;

            ViewBag.Plants = service.CreatePlantList();
            
            return View(purchase);
        }

        // POST: Purchases/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CustomerId,Amount,Status,PurchaseDate")] Purchase purchase, int[] selectedPlants)
        {
            if (ModelState.IsValid)
            {
                purchase.SelectedPlantIDs = selectedPlants;
                await service.Edit(purchase);
                return RedirectToAction("Index");
            }
            purchase.SelectedPlantIDs = selectedPlants;
            ViewBag.Plants = service.CreatePlantList();
            return View(purchase);
        }

        // GET: Purchases/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = await service.Delete(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await service.DeleteConfirmed(id);
            return RedirectToAction("Index");
        }
    }
}
