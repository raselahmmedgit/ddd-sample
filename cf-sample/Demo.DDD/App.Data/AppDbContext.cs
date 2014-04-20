using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using App.Domain;

namespace App.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

    }

    #region Initial data

    // Change the base class as follows if you want to drop and create the database during development:
    //public class DbInitializer : DropCreateDatabaseAlways<AppDbContext>
    //public class DbInitializer : CreateDatabaseIfNotExists<AppDbContext>
    public class DbInitializer : DropCreateDatabaseIfModelChanges<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            // Create default catrgories.
            var categories = new List<Category>
                            {
                                new Category {Id=1, Name = "Fruit"},
                                new Category {Id=2, Name = "Cloth"},
                                new Category {Id=3, Name = "Car"},
                                new Category {Id=4, Name = "Book"},
                                new Category {Id=5, Name = "Shoe"},
                                new Category {Id=6, Name = "Wipper"},
                                new Category {Id=7, Name = "Laptop"},
                                new Category {Id=8, Name = "Desktop"},
                                new Category {Id=9, Name = "Notebook"},
                                new Category {Id=10, Name = "AC"}
                            };

            categories.ForEach(c => context.Categories.Add(c));

            context.SaveChanges();

            // Create some products.
            var products = new List<Product> 
                        {
                            new Product {Id=1, Name="Apple", Price=15, CategoryId=1},
                            new Product {Id=2, Name="Mango", Price=20, CategoryId=1},
                            new Product {Id=3, Name="Orange", Price=15, CategoryId=1},
                            new Product {Id=4, Name="Banana", Price=20, CategoryId=1},
                            new Product {Id=5, Name="Licho", Price=15, CategoryId=1},
                            new Product {Id=6, Name="Jack Fruit", Price=20, CategoryId=1},

                            new Product {Id=7, Name="Toyota", Price=15000, CategoryId=2},
                            new Product {Id=8, Name="Nissan", Price=20000, CategoryId=2},
                            new Product {Id=9, Name="Tata", Price=50000, CategoryId=2},
                            new Product {Id=10, Name="Honda", Price=20000, CategoryId=2},
                            new Product {Id=11, Name="Kimi", Price=50000, CategoryId=2},
                            new Product {Id=12, Name="Suzuki", Price=20000, CategoryId=2},
                            new Product {Id=13, Name="Ferrari", Price=50000, CategoryId=2},

                            new Product {Id=14, Name="T Shirt", Price=20000, CategoryId=3},
                            new Product {Id=15, Name="Polo Shirt", Price=50000, CategoryId=3},
                            new Product {Id=16, Name="Shirt", Price=200, CategoryId=3},
                            new Product {Id=17, Name="Panjabi", Price=500, CategoryId=3},
                            new Product {Id=18, Name="Fotuya", Price=200, CategoryId=3},
                            new Product {Id=19, Name="Shari", Price=500, CategoryId=3},
                            new Product {Id=19, Name="Kamij", Price=400, CategoryId=3},

                            new Product {Id=20, Name="History", Price=20000, CategoryId=4},
                            new Product {Id=21, Name="Fire Out", Price=50000, CategoryId=4},
                            new Product {Id=22, Name="Apex", Price=200, CategoryId=5},
                            new Product {Id=23, Name="Bata", Price=500, CategoryId=5},
                            new Product {Id=24, Name="Acer", Price=200, CategoryId=8},
                            new Product {Id=25, Name="Dell", Price=500, CategoryId=8},
                            new Product {Id=26, Name="HP", Price=400, CategoryId=8}

                        };

            products.ForEach(p => context.Products.Add(p));

            context.SaveChanges();

        }
    }

    #endregion
}
