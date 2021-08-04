using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domeins;

namespace WebStore.DataAccessLayer
{
    public class SampleRepository: IProductRepository
    {
        public IQueryable<Product> Products =>
            new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Name = "Доктор Фаустус",
                    Author = "Томас Манн",
                    Genre = "Роман",
                    Description = "",
                    Category = "",
                    Price = 250
                },
                new Product()
                {
                    Id = 2,
                    Name = "Невыносимая лёгкость бытия",
                    Author = "Милан Кундера",
                    Genre = "Роман",
                    Description = "",
                    Category = "",
                    Price = 210
                },
                new Product()
                {
                    Id = 3,
                    Name = "451 градус по Фаренгейту",
                    Author = "Рэй Брэдбери",
                    Genre = "Научная фантастика, антиутопия",
                    Description = "",
                    Category = "",
                    Price = 300
                },
                new Product()
                {
                    Id = 4,
                    Name = "По ту сторону добра и зла",
                    Author = "Фридрих Ницше",
                    Genre = "Философия",
                    Description = "",
                    Category = "",
                    Price = 397
                },
                new Product()
                {
                    Id = 5,
                    Name = "Превращение",
                    Author = "Франц Кафка",
                    Genre = "Повесть",
                    Description = "",
                    Category = "",
                    Price = 216
                },
                new Product()
                {
                    Id = 6,
                    Name = "Норвежский лес",
                    Author = "Харуки Мураками",
                    Genre = "Роман",
                    Description = "",
                    Category = "",
                    Price = 312
                }
            }.AsQueryable();
    }
}
