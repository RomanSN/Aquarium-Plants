using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AquaMarket.Models;

namespace AquaMarket.Controllers
{
    public class PlantRepo : Controller
    {
        private AquaDBContext db;

        public async Task<ICollection<Plant>> getAllPlants()
        {
            db = new AquaDBContext();
            return await db.Plants.Include(p => p.Files).ToListAsync();
        }

        public async Task <JsonResult>  Autocomplete(string term)
        {
            db = new AquaDBContext();
            db.Configuration.ProxyCreationEnabled = false;
            IEnumerable<Plant> plants = await db.Plants.ToListAsync();
            List<Plant> filteredPlants = new List<Plant>();
            foreach (Plant p in plants){
                string name = p.PlantName.ToLower();
                string t = term;
                if (name.Contains(t.ToLower()))
                {
                    filteredPlants.Add(p);
                }
            }
            //IEnumerable<Plant> filteredPlants = plants.Where(p=>p.Name.ToLower().Contains(term.ToLower())==true);
            //List<Plant> filteredItems = (List<Plant>)plants.Where
            //(item => item.Name.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0);
           
            var result = Json(filteredPlants, JsonRequestBehavior.AllowGet);                    
            return result;
        }

        public async Task<Plant> Details(int? id)
        {
            db = new AquaDBContext();
            Plant plant = await db.Plants.Include(p => p.PlantSpecies).FirstOrDefaultAsync(t => t.Id == id);
            return plant;
        }

        public async Task Create(Plant plant)
        {
            db = new AquaDBContext();
            db.Plants.Add(plant);
            await db.SaveChangesAsync();
            ICollection<Plant> allplants = await db.Plants.ToListAsync();
            Plant created = allplants.Last();
            foreach (var file in plant.Images)
            {
                if (file != null)
                {
                    string fileName = System.IO.Path.GetFileName(file.FileName);
                    string path = System.Web.HttpContext.Current.Server.MapPath("~/Files/" + fileName);
                    file.SaveAs(path);
                    File NewFile = new File
                    {
                        FileName = fileName,
                        FileType = System.IO.Path.GetExtension(file.FileName),
                        PlantId = plant.Id
                    };
                    db.Files.Add(NewFile);
                    await db.SaveChangesAsync();
                }
            }
        }

        public async Task<Plant> Edit(int? id)
        {
            db = new AquaDBContext();
            Plant plant = await db.Plants.Include(p=>p.Files).FirstOrDefaultAsync(t => t.Id == id);
            return plant;
        }

        public async Task Edit(Plant plant)
        {
            db = new AquaDBContext();
            db.Entry(plant).State = EntityState.Modified;
            await db.SaveChangesAsync();

            foreach(var image in plant.Images)
            {
                if (image != null)
                {
                    string fileName = System.IO.Path.GetFileName(image.FileName) + DateTime.Now.ToString();
                    string path = System.Web.HttpContext.Current.Server.MapPath("~/Files/" + fileName);
                    image.SaveAs(path);
                    File NewFile = new File
                    {
                        FileName = fileName,
                        FileType = System.IO.Path.GetExtension(image.FileName),
                        PlantId = plant.Id
                    };
                    db.Files.Add(NewFile);
                    await db.SaveChangesAsync();
                }
            }
            

        }

        public bool CheckFileName(HttpPostedFileBase file)
        {
            db = new AquaDBContext();
            IEnumerable<File> files = db.Files.ToList();
            ICollection<string> fileNames = new HashSet<string>();
            foreach (var item in files)
            {
                fileNames.Add(item.FileName);
            }
            return fileNames.Contains(System.IO.Path.GetFileName(file.FileName));
        }


        public async Task<File> DeleteFile(int? id)
        {
            db = new AquaDBContext();
            File file = await db.Files.FindAsync(id);
            return file;
        }

        public async Task DeleteFileConfirmed(int id)
        {
            db = new AquaDBContext();
            File file = await db.Files.FindAsync(id);
            db.Files.Remove(file);
            await db.SaveChangesAsync();
            var path = System.Web.HttpContext.Current.Server.MapPath("~/Files/" + file.FileName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }

        public async Task<Plant> Delete(int? id)
        {
            db = new AquaDBContext();
            Plant plant = await db.Plants.FindAsync(id);
            return plant;
        }

        public async Task DeleteConfirmed(int id)
        {
            db = new AquaDBContext();
            Plant plant = await db.Plants.FindAsync(id);
            db.Plants.Remove(plant);
            await db.SaveChangesAsync();
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


