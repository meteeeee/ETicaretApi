using ETicaretApi.Domain.Entities;
using ETicaretApi.Persistence.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace ETicaretApi.Persistence.Context
{
    public class ProductContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;initial Catalog=ApiETicaretDb;integrated Security=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Gerçekçi Kategori ID'leri
            var catGiyimId = Guid.Parse("7f3a8b12-9c4e-4f81-a623-4d8e7b91c01a");
            var catElektronikId = Guid.Parse("3e8f1a24-7b6c-4d95-8e12-5a7b9c23d45f");
            var catSporId = Guid.Parse("a1c4e789-3d2f-4b6a-9123-6e8a0b1c2d3e");
            var catKozmetikId = Guid.Parse("5d2e9a18-4b7c-4f83-a912-7b8c9d0e1f2a");
            var catSaatId = Guid.Parse("9b1a4c7e-2d5f-4e89-8134-8c9d0e1f2a3b");
            var catEvYasamId = Guid.Parse("4c7e1a9b-5d2f-4f83-b912-9d0e1f2a3b4c");

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryID = catGiyimId, CategoryName = "Moda & Giyim" },
                new Category { CategoryID = catElektronikId, CategoryName = "Elektronik" },
                new Category { CategoryID = catSporId, CategoryName = "Spor & Outdoor" },
                new Category { CategoryID = catKozmetikId, CategoryName = "Kozmetik & Bakım" },
                new Category { CategoryID = catSaatId, CategoryName = "Saat & Aksesuar" },
                new Category { CategoryID = catEvYasamId, CategoryName = "Ev & Yaşam" }
            );

            // Gerçekçi Ürün ID'leri
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductID = Guid.Parse("2f8a1c94-5b7d-4e82-a139-6d8b9e0c1f2a"),
                    ProductName = "Kemerli Şifon Midi Elbise",
                    ProductCategoryID = catGiyimId,
                    ProductPrice = 1250,
                    ProductImageURL = "https://images.unsplash.com/photo-1572804013309-59a88b7e92f1?w=600&auto=format&fit=crop&q=80"
                },
                new Product
                {
                    ProductID = Guid.Parse("8b3d1e92-4a7c-4f81-9214-7e9c0a1b2d3f"),
                    ProductName = "Slim Fit Oxford Erkek Gömlek",
                    ProductCategoryID = catGiyimId,
                    ProductPrice = 850,
                    ProductImageURL = "https://images.unsplash.com/photo-1602810318383-e386cc2a3ccf?w=600&auto=format&fit=crop&q=80"
                },
                new Product
                {
                    ProductID = Guid.Parse("6e1a9b24-3d7f-4c85-8a12-8f0a1b2c3d4e"),
                    ProductName = "iPhone 15 Pro Max 256GB Titanyum",
                    ProductCategoryID = catElektronikId,
                    ProductPrice = 74999,
                    ProductImageURL = "https://images.unsplash.com/photo-1695048133142-1a20484d2569?w=600&auto=format&fit=crop&q=80"
                },
                new Product
                {
                    ProductID = Guid.Parse("1a4c7e92-6d3f-4e81-9b25-9a1b2c3d4e5f"),
                    ProductName = "Sony WH-1000XM5 Kablosuz Kulaklık",
                    ProductCategoryID = catElektronikId,
                    ProductPrice = 12450,
                    ProductImageURL = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=600&auto=format&fit=crop&q=80"
                },
                new Product
                {
                    ProductID = Guid.Parse("9d2f4a18-7b5c-4e83-a314-0b1c2d3e4f5a"),
                    ProductName = "Nike Air Max 270 Koşu Ayakkabısı",
                    ProductCategoryID = catSporId,
                    ProductPrice = 4200,
                    ProductImageURL = "https://images.unsplash.com/photo-1542291026-7eec264c27ff?w=600&auto=format&fit=crop&q=80"
                },
                new Product
                {
                    ProductID = Guid.Parse("3c8e1a74-2d9f-4b85-8219-1c2d3e4f5a6b"),
                    ProductName = "Bleu de Chanel Edp 100ml Parfüm",
                    ProductCategoryID = catKozmetikId,
                    ProductPrice = 5600,
                    ProductImageURL = "https://images.unsplash.com/photo-1523293182086-7651a899d37f?w=600&auto=format&fit=crop&q=80"
                },
                new Product
                {
                    ProductID = Guid.Parse("7a1b4c92-5d3e-4f81-9412-2d3e4f5a6b7c"),
                    ProductName = "Seiko 5 Otomatik Erkek Kol Saati",
                    ProductCategoryID = catSaatId,
                    ProductPrice = 8900,
                    ProductImageURL = "https://images.unsplash.com/photo-1524805444758-089113d48a6d?w=600&auto=format&fit=crop&q=80"
                },
                new Product
                {
                    ProductID = Guid.Parse("5e9a2c14-4d8f-4b73-8319-3e4f5a6b7c8d"),
                    ProductName = "Philips XXL Sıcak Hava Fritözü Airfryer",
                    ProductCategoryID = catEvYasamId,
                    ProductPrice = 6200,
                    ProductImageURL = "https://images.philips.com/is/image/philipsconsumer/vrs_c299b321_1078_497e_935a8765b6f6434c?$png$&wid=632&hei=632&fit=constrain"
                }
            );
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
