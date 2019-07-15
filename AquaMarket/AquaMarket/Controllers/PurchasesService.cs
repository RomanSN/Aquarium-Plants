using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using AquaMarket.Models;
using System.Web.Mvc;

namespace AquaMarket.Controllers
{
    public class PurchasesService
    {
        private PurchasesRepo repo = new PurchasesRepo();

        public async Task<ICollection<Purchase>> Index()
        {
           return await repo.Index();
        }

        public async Task<Purchase> Details(int? id)
        {
            return await repo.Details(id);
        }

        

        public async Task Create(Purchase purchase)
        {
           await repo.Create(purchase);
        }

        public IEnumerable<Plant> CreatePlantList()
        {
            return repo.CreatePlantList();
        }

        public SelectList CreatePlantList(Purchase purchase)
        {
            return  repo.CreatePlantList(purchase);
        }

        public MultiSelectList CreatePlantSelectList()
        {
            return repo.CreatePlantSelectList();
        }

       

        public async Task<Purchase> Edit(int? id)
        {
            return await repo.Edit(id);
        }

        public async Task Edit(Purchase purchase)
        {
             await repo.Edit(purchase);
        }

        public async Task<Purchase> Delete(int? id)
        {
            return await repo.Delete(id);
        }

        public async Task DeleteConfirmed(int id)
        {
            await repo.DeleteConfirmed(id);
        }

    }

}