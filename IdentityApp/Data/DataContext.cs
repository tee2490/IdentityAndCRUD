using IdentityApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityApp.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.\\SqlExpress; Database=TestIdentityXX04; Trusted_connection=true; TrustServerCertificate=true");
        }

        //สร้างข้อมูลเริ่มต้นให้กับ Role
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>()
            .HasData(
                new IdentityRole { Name = "Member", NormalizedName = "MEMBER" },
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" }
            );

            builder.Entity<Product>()
               .HasData(
               new Product { Id = 1, Name = "Product01", Price = 10, QuantityInStock = 1, Description = "Test", Type = "food" },
               new Product { Id = 2, Name = "Product02", Price = 10, QuantityInStock = 1, Description = "Test", Type = "food" },
               new Product { Id = 3, Name = "Product03", Price = 10, QuantityInStock = 1, Description = "Test", Type = "food" },
               new Product { Id = 4, Name = "Product04", Price = 10, QuantityInStock = 1, Description = "Test", Type = "food" },
               new Product { Id = 5, Name = "Product05", Price = 10, QuantityInStock = 1, Description = "Test", Type = "food" }
               );
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

    }
}
