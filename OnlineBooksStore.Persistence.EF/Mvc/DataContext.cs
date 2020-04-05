using Microsoft.EntityFrameworkCore;
using OnlineBooksStore.Domain.Contracts.Entities;
using OnlineBooksStore.Domain.Contracts.Models;
using OnlineBooksStore.Domain.Contracts.Models.Category;
using OnlineBooksStore.Domain.Contracts.Models.Publisher;

namespace OnlineBooksStore.Persistence.EF.Mvc
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasIndex(p => p.Title);
            modelBuilder.Entity<Book>().HasIndex(p => p.PurchasePrice);
            modelBuilder.Entity<Book>().HasIndex(p => p.RetailPrice);
            modelBuilder.Entity<Category>().HasIndex(p => p.NameEng);
            modelBuilder.Entity<Publisher>().HasIndex(p => p.Name);
        }
    }
}
