using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebStore.Domeins;

namespace WebStore.DataAccessLayer
{
    public class DataManager
    {
        public static void SeedData(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices
                .GetRequiredService<ApplicationDbContext>();

            context.Database.Migrate();
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product()
                    {
                        Name = "Доктор Фаустус",
                        Author = "Томас Манн",
                        Genre = "Роман",
                        Description = "",
                        Category = "",
                        Price = 250,
                    },
                    new Product()
                    {
                        Name = "Невыносимая лёгкость бытия",
                        Author = "Милан Кундера",
                        Genre = "Роман",
                        Description = "",
                        Category = "",
                        Price = 210
                    },
                    new Product()
                    {
                        Name = "451 градус по Фаренгейту",
                        Author = "Рэй Брэдбери",
                        Genre = "Научная фантастика, антиутопия",
                        Description = "",
                        Category = "",
                        Price = 300
                    },
                    new Product()
                    {
                        Name = "По ту сторону добра и зла",
                        Author = "Фридрих Ницше",
                        Genre = "Философия",
                        Description = "",
                        Category = "",
                        Price = 397
                    },
                    new Product()
                    {
                        Name = "Превращение",
                        Author = "Франц Кафка",
                        Genre = "Повесть",
                        Description = "",
                        Category = "",
                        Price = 216
                    },
                    new Product()
                    {
                        Name = "Норвежский лес",
                        Author = "Харуки Мураками",
                        Genre = "Роман",
                        Description = "",
                        Category = "",
                        Price = 312
                    });
                context.SaveChanges();
            }
        }
    }
}
