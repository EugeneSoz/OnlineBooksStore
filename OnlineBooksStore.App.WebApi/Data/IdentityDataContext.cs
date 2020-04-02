using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OnlineBooksStore.App.WebApi.Data
{
    public class IdentityDataContext : IdentityDbContext<IdentityUser>
    {
        public IdentityDataContext(DbContextOptions<IdentityDataContext> options)
        : base(options) { }
    }
}
