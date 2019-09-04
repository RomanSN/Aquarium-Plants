using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace AquaMarket.Models
{
    public class AquaDBContext : DbContext
    {
        public AquaDBContext() : base("AquaDBConnection")
        { }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<Species> Species { get; set; }
        public DbSet<Article> Articles { get; set; }

    }

}