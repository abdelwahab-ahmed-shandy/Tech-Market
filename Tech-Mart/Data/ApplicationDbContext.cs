using Tech_Mart.Models;

namespace Tech_Mart.Data
{
    public class ApplicationDbContext : DbContext
    {
        #region Create Table 
        public DbSet<Models.Category> categories { get; set; }
        public DbSet<Models.Product> products { get; set; }
        #endregion
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            #region Connection In DataBase

            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Tech-Mart-DB;Integrated Security=True;TrustServerCertificate=True");

            #endregion
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Configure the relationship between Product and Category

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.products)
                .HasForeignKey(p => p.CategoryId);
            #endregion

            #region Add Data in Products And Category
            modelBuilder.Entity<Models.Category>().HasData(
                new Category { Id = 1, Name = "Mobiles", Description = "Mobile Products", CategoryImg = "mobiles.jpg" },
                new Category { Id = 2, Name = "LabTops", Description = "LabTops Products", CategoryImg = "laptops.jpg", },
                new Category { Id = 3, Name = "Tablets", Description = "Tablets Products", CategoryImg = "tablets.jpg", }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Iphone11",
                    Description = "Flagship smartphone from Apple",
                    Price = 1999.99,
                    Img = "Iphone11.jpg",
                    Quntity = 10,
                    Rate = 4.5,
                    Discount = 0.1,
                    CategoryId = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "Iphone12",
                    Description = "High-performance smartphone from Apple",
                    Price = 2999.99,
                    Img = "Iphone12.jpg",
                    Quntity = 10,
                    Rate = 4.5,
                    Discount = 0.1,
                    CategoryId = 1
                },
                new Product
                {
                    Id = 3,
                    Name = "Iphone13",
                    Description = "Latest smartphone from Apple",
                    Price = 3999.99,
                    Img = "Iphone13.jpg",
                    Quntity = 10,
                    Rate = 4.5,
                    Discount = 0.1,
                    CategoryId = 1
                },
                new Product
                {
                    Id = 4,
                    Name = "Iphone14",
                    Description = "Premium smartphone from Apple",
                    Price = 4999.99,
                    Img = "Iphone14.jpg",
                    Quntity = 10,
                    Rate = 4.5,
                    Discount = 0.1,
                    CategoryId = 1
                },
                new Product
                {
                    Id = 5,
                    Name = "Iphone15",
                    Description = "Next-gen smartphone from Apple",
                    Price = 5999.99,
                    Img = "Iphone15.jpg",
                    Quntity = 10,
                    Rate = 4.5,
                    Discount = 0.1,
                    CategoryId = 1
                }
 );

            #endregion





        }
    }
}
