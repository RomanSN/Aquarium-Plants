using AquaMarket.Models;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Collections;
using PagedList;
using System.Web;
using System.Collections.Generic;
using System;

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

        // GET: Plants
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
            ViewBag.PlantSpeciesNames = new SelectList(species,"id", "Name");
            ViewBag.PlantUsages = new SelectList(Plant.PlantUsage);
            ViewBag.LightRequirements = new SelectList(Plant.LightRequirements);
            ViewBag.GrowthSpeedValues = new SelectList(Enum.GetValues(typeof(Plant.GrowthSpeedValues)));
            ViewBag.Areas = new SelectList(Enum.GetValues(typeof(Plant.Areas)));
            ViewBag.Locations = new SelectList(Enum.GetValues(typeof(Plant.Locations)));
            ViewBag.PlantComplexity = new SelectList(Enum.GetValues(typeof(Plant.PlantComplexity)));
            ViewBag.Types = new SelectList(Enum.GetValues(typeof(Plant.Types)));
            ViewBag.Colorations = new SelectList(Enum.GetValues(typeof(Plant.Colorations)));
            return View();
        }

        // POST: Plants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PlantName, Description, Light, MinTemp, MaxTemp, MinPh, MaxPh, MinGh, MaxGh, Hight, GrowthSpeed, " +
            "Coloration, Area, Location, Usage, Сomplexity, OriginCountry, PlantType, SpeciesId, Image")] Plant plant)
        {
            repo = new PlantRepo();
            if (ModelState.IsValid)
            {
                await repo.Create(plant);
                return RedirectToAction("Index");
            }
            IEnumerable<Species> species = await repo.GetSpecies();
            ViewBag.PlantSpeciesNames = new SelectList(species, "id", "Name", plant.SpeciesId);
            ViewBag.PlantUsages = new SelectList(Plant.PlantUsage, plant.Usage);
            ViewBag.LightRequirements = new SelectList(Plant.LightRequirements, plant.Light);
            ViewBag.GrowthSpeedValues = new SelectList(Enum.GetValues(typeof(Plant.GrowthSpeedValues)), plant.GrowthSpeed);
            ViewBag.Areas = new SelectList(Enum.GetValues(typeof(Plant.Areas)),plant.Area);
            ViewBag.Locations = new SelectList(Enum.GetValues(typeof(Plant.Locations)), plant.Location);
            ViewBag.PlantComplexity = new SelectList(Enum.GetValues(typeof(Plant.PlantComplexity)), plant.Сomplexity);
            ViewBag.Types = new SelectList(Enum.GetValues(typeof(Plant.Types)), plant.PlantType);
            ViewBag.Colorations = new SelectList(Enum.GetValues(typeof(Plant.Colorations)), plant.Coloration);
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
            IEnumerable<Species> species = await repo.GetSpecies();
            ViewBag.PlantSpeciesNames = new SelectList(species, "id", "Name");
            ViewBag.PlantUsages = new SelectList(Plant.PlantUsage);
            ViewBag.LightRequirements = new SelectList(Plant.LightRequirements);
            ViewBag.GrowthSpeedValues = new SelectList(Enum.GetValues(typeof(Plant.GrowthSpeedValues)));
            ViewBag.Areas = new SelectList(Enum.GetValues(typeof(Plant.Areas)));
            ViewBag.Locations = new SelectList(Enum.GetValues(typeof(Plant.Locations)));
            ViewBag.PlantComplexity = new SelectList(Enum.GetValues(typeof(Plant.PlantComplexity)));
            ViewBag.Types = new SelectList(Enum.GetValues(typeof(Plant.Types)));
            ViewBag.Colorations = new SelectList(Enum.GetValues(typeof(Plant.Colorations)));
            return View(plant);
        }

        // POST: Plants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PlantName, Description, Light, MinTemp, MaxTemp, MinPh, MaxPh, MinGh, MaxGh, Hight, GrowthSpeed, " +
            "Coloration, Area, Location, Usage, Сomplexity, OriginCountry, PlantType, SpeciesId, Image")] Plant plant)
        {
            repo = new PlantRepo();
            
            if (ModelState.IsValid)
            {
                await repo.Edit(plant);
                return RedirectToAction("Index");
            }
            Plant plantDB = await repo.Edit(plant.Id);
            plant.FileId = plantDB.FileId;
            plant.File = plantDB.File;
            IEnumerable<Species> species = await repo.GetSpecies();
            ViewBag.PlantSpeciesNames = new SelectList(species, "id", "Name");
            ViewBag.PlantUsages = new SelectList(Plant.PlantUsage);
            ViewBag.LightRequirements = new SelectList(Plant.LightRequirements);
            ViewBag.GrowthSpeedValues = new SelectList(Enum.GetValues(typeof(Plant.GrowthSpeedValues)));
            ViewBag.Areas = new SelectList(Enum.GetValues(typeof(Plant.Areas)));
            ViewBag.Locations = new SelectList(Enum.GetValues(typeof(Plant.Locations)));
            ViewBag.PlantComplexity = new SelectList(Enum.GetValues(typeof(Plant.PlantComplexity)));
            ViewBag.Types = new SelectList(Enum.GetValues(typeof(Plant.Types)));
            ViewBag.Colorations = new SelectList(Enum.GetValues(typeof(Plant.Colorations)));

            return View(plant);
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

