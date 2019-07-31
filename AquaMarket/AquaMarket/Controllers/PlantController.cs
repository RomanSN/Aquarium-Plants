using AquaMarket.Models;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Collections;
using PagedList;
using System.Web;
using System.Collections.Generic;

namespace AquaMarket.Controllers
{
    public class PlantController : Controller
    {
        private PlantRepo repo;

        public PageInfo PageInfo { get; private set; }

        public PlantController()
        {
            
        }


        //// GET: Plant Names
        public async Task<JsonResult> Autocomplete(string term)
        {
            repo = new PlantRepo();
            string t = term;
            return  await repo.Autocomplete(term);
        }

        // GET: Plant
        public async Task<ActionResult> Index(string area, string light, string complexity, int? t, decimal? ph, decimal? gh,
             int? pageIndex, int? pageSize)
        {
            repo = new PlantRepo();
            var pvm = await repo.getAllPlants(area, light, complexity, t, ph, gh, pageIndex, pageSize);

            return View(pvm);
        }

        // GET: Plant/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            repo = new PlantRepo();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plant plant = await repo.Details(id);
            if (plant == null)
            {
                return HttpNotFound();
            }
            return View(plant);
        }

        // GET: Plants/Create
        public async Task<ActionResult> Create()
        {
            repo = new PlantRepo();
            IEnumerable<Species> species = await repo.GetSpecies();
            ViewBag.PlantSpeciesNames = new SelectList(species);
            return View();
        }

        // POST: Plants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PlantName,Description,Light,MinTemp,MaxTemp,MinPh,MaxPh,MinGh,MaxGh," +
                                "GrowthSpeed,Hight,Coloration,Area,Location,Usage,Сomplexity,OriginCountry,Type,PlantSpecies,Image")] Plant plant)
        {
            repo = new PlantRepo();
            if (ModelState.IsValid)
            {
                await repo.Create(plant);
                return RedirectToAction("Index");
            }

            return View(plant);
        }

        // GET: Plants/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            repo = new PlantRepo();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plant plant = await repo.Edit(id);
            if (plant == null)
            {
                return HttpNotFound();
            }
            return View(plant);
        }

        // POST: Plants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( Plant plant)
        {
            repo = new PlantRepo();
            Plant plantDB = await repo.Edit(plant.Id);
            plant.FileId = plantDB.FileId;
            
            
            if (ModelState.IsValid)
            {
                await repo.Edit(plant);
                return RedirectToAction("Index");
            }
            plant.File = plantDB.File;

            return View(plant);//RedirectToAction("Edit", new { id =plant.Id });
        }
       
       
        [HttpGet]
        public async Task<ActionResult> DeleteFile(int? FileId)
        {
            repo = new PlantRepo();
            if (FileId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            File file = await repo.DeleteFile(FileId);
             
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteFileConfirmed([Bind(Include = "Id, FileName, PlantId")]File file)
        {
            repo = new PlantRepo();
            await repo.DeleteFileConfirmed(file.Id);
            return RedirectToAction("Edit", new {id = file.PlantId });
        }
        // GET: Plants/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            repo = new PlantRepo();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plant plant = await repo.Delete(id);
            if (plant == null)
            {
                return HttpNotFound();
            }
            return View(plant);
        }

        // POST: Plants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            repo = new PlantRepo();
            await repo.DeleteConfirmed(id);
            return RedirectToAction("Index");
        }
    }
}

