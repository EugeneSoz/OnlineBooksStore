using System.Linq;
using System.Threading.Tasks;

namespace OnlineBooksStore.App.WebApi.Models.Repo
{
    //интерфейс для базового класса репозитория
    public interface IBaseRepo<T>
    {
        IQueryable<T> GetEntities();
        Task<T> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
    }
}
