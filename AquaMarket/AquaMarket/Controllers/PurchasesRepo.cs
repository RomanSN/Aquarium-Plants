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
    public class PurchasesRepo : Controller
    {
        private AquaDBContext db = new AquaDBContext();

        public async Task<ICollection<Purchase>> Index()
        {
            var purchases = db.Purchases.Include(p=>p.Plants);
            return await purchases.ToListAsync();
        }

        public async Task<Purchase> Details(int? id)
        {
            return await db.Purchases.Include(t=>t.Plants).FirstOrDefaultAsync(p=>p.Id==id);
        }


        

        public IEnumerable<Plant> CreatePlantList()
        {
            return db.Plants.ToList();
        }

        public SelectList CreatePlantList(Purchase purchase)
        {
            var list = new SelectList(db.Plants, "Id", "Name", purchase.SelectedPlantIDs);
            return list;
        }

        public MultiSelectList CreatePlantSelectList()
        {
            return new  MultiSelectList(db.Plants, "Id","Name");
        }

        //[HttpPost]
        public async Task Create(Purchase purchase)
        {
            int[] selectedPlantIDs = purchase.SelectedPlantIDs;
            ICollection<Plant> plants = await db.Plants.Where(p=> selectedPlantIDs.Contains(p.Id)).ToListAsync();
            foreach(var plant in plants)
            {
                purchase.Plants.Add(plant);
            }
            purchase.PurchaseDate = DateTime.Now;
            db.Purchases.Add(purchase);
            await db.SaveChangesAsync();
        }

        //[HttpGet]
        public async Task<Purchase> Edit(int? id)
        {
            Purchase purchase = await db.Purchases.Include(p=>p.Plants).FirstOrDefaultAsync(t=>t.Id==id);
            return purchase;
        }
        //[HttpPost]
        public async Task Edit(Purchase purchase)
        {
            Purchase newPurchase = await db.Purchases.FirstOrDefaultAsync(p=>p.Id==purchase.Id);
            
            newPurchase.Amount = purchase.Amount;
            newPurchase.Status = purchase.Status;
            ICollection<Plant> plants = await db.Plants.Where(p => purchase.SelectedPlantIDs.Contains(p.Id)).ToListAsync();
            newPurchase.Plants.Clear();
            foreach (var plant in plants)
            {
                newPurchase.Plants.Add(plant);
            }
            //newPurchase.PurchaseDate = DateTime.Now;
            db.Entry(newPurchase).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
        //[HttpGet]
        public async Task<Purchase> Delete(int? id)
        {
            Purchase purchase = await db.Purchases.FirstOrDefaultAsync(o=>o.Id==id);
            return purchase;
        }
        //[HttpPost]
        public async Task DeleteConfirmed(int id)
        {
            Purchase purchase = await db.Purchases.FindAsync(id);
            db.Purchases.Remove(purchase);
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