using Microsoft.EntityFrameworkCore;

namespace OnlineBooksStore.App.WebApi.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasMany<Book>(c => c.Books).WithOne(b => b.Category)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Publisher>().HasMany<Book>(p => p.Books).WithOne(p => p.Publisher)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Category>().HasMany<Category>(c => c.ChildrenCategories)
                .WithOne(cc => cc.ParentCategory).HasForeignKey(cc => cc.ParentCategoryID)
                .OnDelete(DeleteBehavior.Restrict);
        }

        //таблица книг в бд
        public DbSet<Book> Books { get; set; }

        //таблица категорий в бд
        public DbSet<Category> Categories { get; set; }

        //таблица издателей в бд
        public DbSet<Publisher> Publishers { get; set; }

        //таблица заказов в бд
        public DbSet<Order> Orders { get; set; }
    }
}
