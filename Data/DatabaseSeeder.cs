using WebApplicationNawatech.Models;

namespace WebApplicationNawatech.Data
{
    public class DatabaseSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            // --- Seed Users ---
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User
                    {
                        Name = "User1",
                        Email = "U1@example.com",
                        Password = "password123",
                        ProfilePicture = ""
                    },
                    new User
                    {
                        Name = "User2",
                        Email = "U2@example.com",
                        Password = "password456",
                        ProfilePicture = ""
                    }
                );
                context.SaveChanges();
            }

            // --- Seed Categories ---
            if (!context.ProductCategories.Any())
            {
                context.ProductCategories.AddRange(
                    new ProductCategory { Name = "Electronics" },
                    new ProductCategory { Name = "Clothing" },
                    new ProductCategory { Name = "Books" },
                    new ProductCategory { Name = "Home Appliances" },
                    new ProductCategory { Name = "Toys" }
                );
                context.SaveChanges();
            }

            // --- Seed Products ---
            if (!context.Products.Any())
            {
                var categories = context.ProductCategories.ToList();

                context.Products.AddRange(
                    new Product
                    {
                        Name = "Iphone 16",
                        Price = 7500000,
                        ProductCategoryId = categories.First(c => c.Name == "Electronics").Id,
                        ImageUrl = "/uploads/product1.jpg"
                    },
                    new Product
                    {
                        Name = "Bluetooth Headphones",
                        Price = 250000,
                        ProductCategoryId = categories.First(c => c.Name == "Electronics").Id,
                        ImageUrl = "/uploads/product2.jpg"
                    },
                    new Product
                    {
                        Name = "Men's Jacket",
                        Price = 300000,
                        ProductCategoryId = categories.First(c => c.Name == "Clothing").Id,
                        ImageUrl = "/uploads/product3.jpg"
                    },
                    new Product
                    {
                        Name = "T-Shirt",
                        Price = 100000,
                        ProductCategoryId = categories.First(c => c.Name == "Clothing").Id,
                        ImageUrl = "/uploads/product4.jpg"
                    },
                    new Product
                    {
                        Name = "Novel: The Adventure",
                        Price = 85000,
                        ProductCategoryId = categories.First(c => c.Name == "Books").Id,
                        ImageUrl = "/uploads/product5.jpg"
                    },
                    new Product
                    {
                        Name = "Cookbook Deluxe",
                        Price = 95000,
                        ProductCategoryId = categories.First(c => c.Name == "Books").Id,
                        ImageUrl = "/uploads/product6.jpg"
                    },
                    new Product
                    {
                        Name = "Microwave Oven",
                        Price = 1200000,
                        ProductCategoryId = categories.First(c => c.Name == "Home Appliances").Id,
                        ImageUrl = "/uploads/product7.jpg"
                    },
                    new Product
                    {
                        Name = "Blender Pro",
                        Price = 450000,
                        ProductCategoryId = categories.First(c => c.Name == "Home Appliances").Id,
                        ImageUrl = "/uploads/product8.jpg"
                    },
                    new Product
                    {
                        Name = "Action Figure",
                        Price = 150000,
                        ProductCategoryId = categories.First(c => c.Name == "Toys").Id,
                        ImageUrl = "/uploads/product9.jpg"
                    },
                    new Product
                    {
                        Name = "Puzzle Game",
                        Price = 60000,
                        ProductCategoryId = categories.First(c => c.Name == "Toys").Id,
                        ImageUrl = "/uploads/product10.jpg"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
