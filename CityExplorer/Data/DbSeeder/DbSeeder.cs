using CityExplorer.Enums;
using CityExplorer.Models;
using Microsoft.AspNetCore.Identity;

namespace CityExplorer.Data.DbSeeder
{
    public static class DbSeeder
    {
        public static void Seed(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync(Roles.Admin.ToString()).Result)
            {
                var role = new IdentityRole
                {
                    Name = Roles.Admin.ToString()
                };

                var result = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync(Roles.User.ToString()).Result)
            {
                var role = new IdentityRole
                {
                    Name = Roles.User.ToString()
                };

                var result = roleManager.CreateAsync(role).Result;
            }
        }
        private static void SeedUsers(UserManager<AppUser> userManager)
        {
            if (userManager.FindByEmailAsync("admin@admin.com").Result is null)
            {
                var user = new AppUser
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    FirstName = "Super",
                    LastName = "Admin",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };


                var result = userManager.CreateAsync(user, "Abecadlo123!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Roles.Admin.ToString()).Wait();
                    var token = userManager.GenerateEmailConfirmationTokenAsync(user);
                    userManager.ConfirmEmailAsync(user, token.Result);
                    user.EmailConfirmed = true;
                }
            }
        }
    }
}
