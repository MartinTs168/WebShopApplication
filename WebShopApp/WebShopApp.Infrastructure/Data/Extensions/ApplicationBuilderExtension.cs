using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebShopApp.Infrastructure.Data.Entities;

namespace WebShopApp.Infrastructure.Data.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app)
        {

            using var serviceScope = app.ApplicationServices.CreateScope();

            var services = serviceScope.ServiceProvider;

            await RoleSeeder(services);
            await SeedAdministrator(services);

            var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await SeedCategories(context);
            await SeedBrands(context);

            return app;
        }

        private static async Task SeedBrands(ApplicationDbContext context)
        {
            if (context.Brands.Any())
            {
                return;
            }

            await context.Brands.AddRangeAsync(new[]
            {
                new Brand {BrandName = "Acer"},
                new Brand {BrandName = "Asus"},
                new Brand {BrandName = "Apple"},
                new Brand {BrandName = "Dell"},
                new Brand {BrandName = "HP"},
                new Brand {BrandName = "Huawei"},
                new Brand {BrandName = "Lenovo"},
                new Brand {BrandName = "Samsung"},
            });

            await context.SaveChangesAsync();
        }

        private static async Task SeedCategories(ApplicationDbContext context)
        {
            if(context.Categories.Any())
            {
                return;
            }

            await context.Categories.AddRangeAsync(new[]
            {
                new Category {CategoryName = "Laptop"},
                new Category {CategoryName = "Computer"},
                new Category {CategoryName = "Monitor"},
                new Category {CategoryName = "Accessory"},
                new Category {CategoryName = "TV"},
                new Category {CategoryName = "Mobile Phone"},
                new Category {CategoryName = "Smart watch"},
        
            });

            await context.SaveChangesAsync();
        }

        private static async Task RoleSeeder(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Administrator", "Client" };


            foreach(var role in roleNames)
            {
                var roleExists = await roleManager.RoleExistsAsync(role);

                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        private static async Task SeedAdministrator(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (await userManager.FindByNameAsync("admin") == null)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    FirstName = "admin",
                    LastName = "admin",
                    UserName = "admin",
                    Email = "admin@admin.com",
                    Address = "admin address",
                    PhoneNumber = "0888888888"
                };

                var result = await userManager.CreateAsync(user, "Admin123456");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Administrator");
                }
            }
        }
    }
}
