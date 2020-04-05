using Microsoft.EntityFrameworkCore;
using OnlineBooksStore.Domain.Contracts.Entities;
using OnlineBooksStore.Persistence.Entities;

namespace OnlineBooksStore.Persistence.EF
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryEntity>().HasMany(c => c.Books).WithOne(b => b.Category)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<PublisherEntity>().HasMany(p => p.Books).WithOne(p => p.Publisher)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<CategoryEntity>().HasMany(c => c.ChildrenCategories)
                .WithOne(cc => cc.ParentCategory).HasForeignKey(cc => cc.ParentId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        //таблица книг в бд
        public DbSet<BookEntity> Books { get; set; }

        //таблица категорий в бд
        public DbSet<CategoryEntity> Categories { get; set; }

        //таблица издателей в бд
        public DbSet<PublisherEntity> Publishers { get; set; }

        //таблица заказов в бд
        public DbSet<Order> Orders { get; set; }
    }
}
