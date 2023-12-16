using Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Context.Persistence
{
    public partial class RayaDBContext : DbContext
    {
        public RayaDBContext(DbContextOptions<RayaDBContext> options)
         : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Soft Delete Filter

            modelBuilder.Entity<Product>()
                .HasQueryFilter(x => x.Deleted == false);
            modelBuilder.Entity<Category>()
                .HasQueryFilter(x => x.Deleted == false);
            modelBuilder.Entity<InventoryTransaction>()
                .HasQueryFilter(x => x.Deleted == false);

            #endregion

            #region Seeding Data
            modelBuilder.Entity<Category>().HasData(
               new Category { Id = 1, Name = "C01", Description = "Category 01", Created = DateTime.Now, CreatedBy = "efcore seed" },
               new Category { Id = 2, Name = "C02", Description = "Category 02", Created = DateTime.Now, CreatedBy = "efcore seed" },
               new Category { Id = 3, Name = "C03", Description = "Category 03", Created = DateTime.Now, CreatedBy = "efcore seed" }

               );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Code = "P01", Name = "Product 01", Description = "", CategoryId = 1, Created = DateTime.Now, CreatedBy = "efcore seed" },
                new Product { Id = 2, Code = "P02", Name = "Product 02", Description = "", CategoryId = 1, Created = DateTime.Now, CreatedBy = "efcore seed" },
                new Product { Id = 3, Code = "P03", Name = "Product 03", Description = "", CategoryId = 2, Created = DateTime.Now, CreatedBy = "efcore seed" },
                new Product { Id = 4, Code = "P04", Name = "Product 04", Description = "", CategoryId = 3, Created = DateTime.Now, CreatedBy = "efcore seed" }
            );
            #endregion


        }

    }
}
