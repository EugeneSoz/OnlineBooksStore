using OnlineBooksStore.Domain.Contracts.Models;
using System.Threading.Tasks;

namespace OnlineBooksStore.Integration.Contracts.Store
{
    public interface IAccountClientService
    {
        Task LoginAsync(Login creds);
        Task LogoutAsync();
    }
}