using AquaMarket.Models;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

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
            string t = term;
            return  await repo.Autocomplete(term);
        }

        // GET: Plant
        public async Task<ActionResult> Index()
        {
            repo = new PlantRepo();
            return View(await repo.getAllPlants());
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
        public ActionResult Create()
        {
            ViewBag.PlantSpeciesNames = new string[3] { "test", "test", "test" };
            return View();
        }

        // POST: Plants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,Description,Location,Light,GrowthSpeed,Hight,Temp,Ph,Gh,Kh,Price,Count,Images")]
        Plant plant)
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
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,Description,Location,Light,GrowthSpeed,Hight,Temp,Ph,Gh,Kh,Price,Count,Images")]
                                              Plant plant)
        {
            repo = new PlantRepo();
            if (ModelState.IsValid)
            {
                await repo.Edit(plant);
                return RedirectToAction("Index");
            }
            Plant plantDB = await repo.Edit(plant.Id);
            plant.Files = plantDB.Files;
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

