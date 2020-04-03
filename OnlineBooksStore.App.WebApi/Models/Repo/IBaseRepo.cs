using System.Linq;
using System.Threading.Tasks;

namespace OnlineBooksStore.App.WebApi.Models.Repo
{
    //интерфейс для базового класса репозитория
    public interface IBaseRepo<T>
    {
        IQueryable<T> GetEntities();
        T Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
    }
}
