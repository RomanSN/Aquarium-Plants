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


        public PlantController()
        {
            
        }


        //// GET: Plant Names
        public async Task<JsonResult> Autocomplete(string term)
        {
            repo = new PlantRepo();
            return  await repo.Autocomplete(term);
        }

        public ActionResult Calculator()
        {
            return View(new PlantViewModel());
        }
        //[HttpPost]
        public async Task<ActionResult> SelectedPlants(PlantViewModel viewModel, int? pageIndex, int? pageSize, int? Temp)
        {
            repo = new PlantRepo();
            var pvm = await repo.GetAllPlants(viewModel, pageIndex, pageSize);

            return View(pvm);
        }
        // GET: Plants
        public async Task<ActionResult> Index(PlantViewModel viewModel,  int? pageIndex, int? pageSize)
        {
            repo = new PlantRepo();
            var pvm = await repo.GetAllPlants(viewModel, pageIndex, pageSize);
            
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
            //IEnumerable<Species> species = await repo.GetSpecies();
            ViewBag.PlantSpeciesNames = new SelectList(await repo.GetSpecies(), "id", "Name");
            return View(new Plant());
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
                Plant created = await repo.GetCreatedPlant();
                return RedirectToAction("Details", new { id = created.Id });
            }
            IEnumerable<Species> species = await repo.GetSpecies();
            ViewBag.PlantSpeciesNames = new SelectList(species, "id", "Name", plant.SpeciesId);
            return View(plant);
        }


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
            return View(plant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,PlantName, Description, Light, MinTemp, MaxTemp, MinPh, MaxPh, MinGh, MaxGh, Hight, GrowthSpeed, " +
            "Coloration, Area, Location, Usage, Сomplexity, OriginCountry, PlantType, SpeciesId, Image")] Plant plant)
        {
            repo = new PlantRepo();
            
            if (ModelState.IsValid)
            {
                await repo.Edit(plant);
                return RedirectToAction("Details", new {id = plant.Id });
            }
            Plant plantDB = await repo.Edit(plant.Id);
            plant.FileId = plantDB.FileId;
            plant.File = plantDB.File;
            ViewBag.PlantSpeciesNames = new SelectList(await repo.GetSpecies(), "id", "Name");

            return View(plant);
        }

        [HttpGet]
        public async Task<ActionResult> DeleteFileConfirmed(int fileId)
        {
            repo = new PlantRepo();
            await repo.DeleteFileConfirmed(fileId);
            return PartialView();
        }
       

        [HttpGet, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            repo = new PlantRepo();
            await repo.DeleteConfirmed(id);
            return RedirectToAction("Index");
        }

       
    }
}

