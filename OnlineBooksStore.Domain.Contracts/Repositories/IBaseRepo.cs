﻿using System.Linq;

namespace OnlineBooksStore.Domain.Contracts.Repositories
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
