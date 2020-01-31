using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AquaMarket.Models
{
    public class AppDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        
        protected override void Seed(ApplicationDbContext context)
        {
            //just test seed method
            bool x = true;
        }
        public void SeedAdmin(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // создаем две роли
            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "user" };
            string adminName = "Admin";
            var admin = new ApplicationUser { Email = "roman.test@ukr.net", UserName = adminName, NickName = adminName, Age = 25 };
            string password = "roman1979snitsar";


            if (!roleManager.RoleExistsAsync("admin").Result)
            {
                roleManager.Create(role1);
            }
            if (!roleManager.RoleExistsAsync("user").Result)
            {
                roleManager.Create(role2);
            }
            IdentityResult result = new IdentityResult();
            ApplicationUser userAdmDB = userManager.FindByName(adminName);
            if (userAdmDB != null)
            {
               result = userManager.Delete(userAdmDB);
               result = userManager.Create(admin, password);
            }
            else
            {
                result = userManager.Create(admin, password);
            }
           
            if (result.Succeeded)
            {
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(admin.Id, role2.Name);
            }
            base.Seed(context);
          
        }
    }
}