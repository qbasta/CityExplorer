using CityExplorer.Enums;
using CityExplorer.Models;
using Microsoft.AspNetCore.Identity;

namespace CityExplorer.Data.DbSeeder
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
            SeedCategories(context);
            SeedCities(context);

        }
        private static void SeedCategories(ApplicationDbContext context)
        {
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Muzeum" },
                    new Category { Name = "Park" },
                    new Category { Name = "Fontanna" },
                    new Category { Name = "Plac" },
                };

                context.Categories.AddRange(categories);
                context.SaveChanges();
            }
        }

        private static void SeedCities(ApplicationDbContext context)
        {
            if (!context.Cities.Any())
            {
                var cities = new List<City>
            {
                new City
                {
                    Name = "Rzym",
                    Country = "Włochy",
                    Landmarks = new List<Landmark>(),
                },
                new City
                {
                    Name = "Berlin",
                    Country = "Niemcy",
                    Landmarks = new List<Landmark>(),
                },

            };
            }
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
