using E_Ticaret.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticaret
{
    public class IdentityInitializer
    {
        public static void OlusturAdmin(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            AppUser appUser = new AppUser
            {
                Name="Merve",
                SurName="Kardes",
                UserName="g171210052@sakarya.edu.tr"
            };
            if (userManager.FindByNameAsync("g171210052@sakarya.edu.tr").Result == null)
            {
               var identityResult= userManager.CreateAsync(appUser, "123").Result;
            }
            if (roleManager.FindByNameAsync("Admin").Result == null)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Admin"
                };
                var identityResult = roleManager.CreateAsync(role).Result;

               var result= userManager.AddToRoleAsync(appUser, role.Name).Result;
            }
        }
    }
}
