using Microsoft.EntityFrameworkCore;
using OnlineBooksStore.Domain.Contracts.Models.Database;

namespace OnlineBooksStore.Domain.Contracts.Services
{
    public interface IDbDataService
    {
        DbMessageResponse SeedDatabase(DbContext context);
    }
}