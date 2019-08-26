using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AquaMarket.Models;
using PagedList;

namespace AquaMarket.Controllers
{
    public class PlantRepo : Controller
    {
        private AquaDBContext db;

        //[HttpGet]
        public async Task<PlantViewModel> getAllPlants(string area, string light,
             string complexity, int? t, decimal? ph, decimal? gh, int? pageIndex, int? pageSize)
        {
           
            db = new AquaDBContext();
            IEnumerable<Plant> result = await db.Plants.Include(p => p.File).Include(p => p.PlantSpecies).ToListAsync();

            if (area != null && !area.Equals("Area"))
            {
                result = result.Where(p => p.Area == area);
            }
            if (light != null && !light.Equals("Lighting"))
            {
                result = result.Where(p => p.Light == light);
            }
            if (complexity != null && !complexity.Equals("Complexity"))
            {
                result = result.Where(p => p.Сomplexity == complexity);
            }
            if (t!=null)
            {
                result = result.Where(p => t >= p.MinTemp && t<= p.MaxTemp);
            }
            if (ph != null)
            {
                result = result.Where(p => t >= p.MinPh && t <= p.MaxPh);
            }
            if (gh != null)
            {
                result = result.Where(p => t >= p.MinGh && t <= p.MaxGh);
            }

            //pageing section


            if (pageSize == null)
            {

                var cookies = System.Web.HttpContext.Current.Request.Cookies;
                if (cookies["ItemCount"] == null)
                {
                    cookies.Set(new HttpCookie("ItemCount"));
                }

                if (int.TryParse(cookies["ItemCount"].Value, out int count))
                {
                    pageSize = count;
                }
                else
                {
                    pageSize = 3;
                }
            }

            var responsecookies = System.Web.HttpContext.Current.Response.Cookies;

            if (responsecookies["ItemCount"] == null)
            {
                responsecookies.Set(new HttpCookie("ItemCount"));
            }

            responsecookies["ItemCount"].Value = pageSize.ToString();

            if (responsecookies["PageIndex"] == null)
            {
                responsecookies.Set(new HttpCookie("PageIndex"));
            }

            var pIndex = pageIndex ?? 1;

            responsecookies["PageIndex"].Value = pIndex.ToString();

            var pSize = int.Parse(System.Web.HttpContext.Current.Response.Cookies["ItemCount"].Value);

            result = result.ToPagedList(pIndex, pSize);
            
            PlantViewModel pvm = new PlantViewModel
            {
                PageInfo = new PageInfo{
                    PageSize = pSize,
                    PageNumber = pIndex
                },

                Plants = result
            };

            return pvm;
        }

        

        public async Task <JsonResult>  Autocomplete(string term)
        {
            db = new AquaDBContext();
            db.Configuration.ProxyCreationEnabled = false;

            IEnumerable<Plant> plants = await db.Plants.ToListAsync();
            List<Plant> filteredPlants = new List<Plant>();
            foreach (Plant p in plants){
                string name = p.PlantName.ToLower();
                string t = term.ToLower();
                if (name.Contains(t))
                {
                    filteredPlants.Add(p);
                }
            }
           
            var result = Json(filteredPlants, JsonRequestBehavior.AllowGet);                    
            return result;
        }

        public async Task<Plant> Details(int? id)
        {
            db = new AquaDBContext();
            
            Plant plant = await db.Plants.Include(p => p.File).Include(p => p.PlantSpecies).FirstOrDefaultAsync(t => t.Id == id);
            return plant;
        }

        public async Task Create(Plant plant)
        {
            db = new AquaDBContext();
            db.Plants.Add(plant);
            await db.SaveChangesAsync();

            ICollection<Plant> allplants = await db.Plants.ToListAsync();
            Plant createdplant = allplants.Last();

            if (plant.Image != null)
                {
                    string fileName = System.IO.Path.GetFileName(plant.Image.FileName);
                    string path = System.Web.HttpContext.Current.Server.MapPath("~/Files/" + fileName);
                    plant.Image.SaveAs(path);
                    File NewFile = new File
                    {
                        FileName = fileName,
                        FileType = System.IO.Path.GetExtension(plant.Image.FileName),
                        PlantId = createdplant.Id
                    };
                    db.Files.Add(NewFile);
                    await db.SaveChangesAsync();
                }
            ICollection<File> allfiles = await db.Files.ToListAsync();
            File createdfile = allfiles.Last();
            createdplant.FileId = createdfile.Id;
            db.Entry(createdplant).State = EntityState.Modified;
            await db.SaveChangesAsync();

        }

        public async Task<Plant> Edit(int? id)
        {
            db = new AquaDBContext();
            Plant plant = await db.Plants.Include(p=>p.File).Include(p=>p.PlantSpecies).FirstOrDefaultAsync(t => t.Id == id);
            plant.PlantSpecies.PlantFamily =  db.Families.FirstOrDefault(f=>f.Id == plant.PlantSpecies.FamilyId);
            return plant;
        }

        public async Task Edit(Plant plant)
        {
            db = new AquaDBContext();
           
                if (plant.Image != null)
                {
                    string fileName = System.IO.Path.GetFileNameWithoutExtension(plant.Image.FileName)+ DateTime.Now.Millisecond.ToString()+ System.IO.Path.GetExtension(plant.Image.FileName);
                    string path = System.Web.HttpContext.Current.Server.MapPath("~/Files/" + fileName);
                    plant.Image.SaveAs(path);
                    File NewFile = new File
                    {
                        FileName = fileName,
                        FileType = System.IO.Path.GetExtension(plant.Image.FileName),
                        PlantId = plant.Id
                    };
                    db.Files.Add(NewFile);
                    await db.SaveChangesAsync();
                    ICollection<File> allfiless = await db.Files.ToListAsync();
                    File created = allfiless.Last();
                    plant.FileId = created.Id;
                }
              
            db.Entry(plant).State = EntityState.Modified;
            await db.SaveChangesAsync();
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

        public async Task DeleteConfirmed(int id)
        {
            db = new AquaDBContext();
            Plant plant = await db.Plants.FindAsync(id);
            db.Plants.Remove(plant);
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Species>> GetSpecies()
        {
            db = new AquaDBContext();
            return await db.Species.ToListAsync();
        }

        internal async Task<Plant> getCreatedPlant()
        {
            db = new AquaDBContext();
            ICollection<Plant> plants = await db.Plants.ToListAsync();
            return plants.Last();
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


