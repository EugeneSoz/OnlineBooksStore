using Microsoft.EntityFrameworkCore;
using OnlineBooksStore.Domain.Contracts.Entities;

namespace OnlineBooksStore.Persistence.EF
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
    }
}
