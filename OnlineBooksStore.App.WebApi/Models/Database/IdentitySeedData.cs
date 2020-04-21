using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineBooksStore.Persistence.EF;

namespace OnlineBooksStore.App.WebApi.Models.Database
{
    public static class IdentitySeedData
    {
        private const string adminUser = "admin";
        private const string adminPassword = "Secret123$";
        private const string adminRole = "Administrator";

        public static async Task SeedDatabase(IdentityDataContext context, 
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            if (context.Database.GetMigrations().Count() > 0
                    && context.Database.GetPendingMigrations().Count() == 0)
            {
                IdentityRole role = await roleManager.FindByNameAsync(adminRole);
                IdentityUser user = await userManager.FindByNameAsync(adminUser);

                if (role == null)
                {
                    role = new IdentityRole(adminRole);
                    IdentityResult result = await roleManager.CreateAsync(role);
                    if (!result.Succeeded)
                    {
                        throw new Exception("Невозможно создать роль: " + result.Errors.FirstOrDefault());
                    }
                }

                if (user == null)
                {
                    user = new IdentityUser(adminUser);
                    IdentityResult result = await userManager.CreateAsync(user, adminPassword);

                    if (!result.Succeeded)
                    {
                        throw new Exception("Невозможно создать пользователя: " 
                            + result.Errors.FirstOrDefault());
                    }
                }

                if (!await userManager.IsInRoleAsync(user, adminRole))
                {
                    IdentityResult result
                        = await userManager.AddToRoleAsync(user, adminRole);
                    if (!result.Succeeded)
                    {
                        throw new Exception("Невозможно добавить пользователя к роли: "
                            + result.Errors.FirstOrDefault());
                    }
                }
            }
        }
    }
}
