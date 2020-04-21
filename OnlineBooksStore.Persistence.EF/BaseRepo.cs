using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OnlineBooksStore.Domain.Contracts.Repositories;
using OnlineBooksStore.Persistence.Entities;

namespace OnlineBooksStore.Persistence.EF
{
    //базовый класс хранилища
    //используется для взаимодействия с базой данных с помощью CRUD операций
    public class BaseRepo<T> : IDisposable, IBaseRepo<T> where T : BaseEntity
    {
        protected BaseRepo(StoreDbContext ctx) => Context = ctx;

        //ссылка на контекст бд
        protected StoreDbContext Context { get; }

        public void Dispose() => Context?.Dispose();

        public IQueryable<T> GetEntities()
        {
            return Context.Database.GetAppliedMigrations().Any() ? Context.Set<T>() : null;
        }

        //создать запись в бд
        public T Add(T entity)
        {
            Context.Add(entity);
            Context.SaveChanges();

            return entity;
        }

        //обновить запись в бд
        public bool Update(T entity)
        {
            long id = entity.Id;
            bool isExist = EntityExist(id);
            if (!isExist)
            {
                return false;
            }
            Context.Update(entity);

            Context.SaveChanges();

            return true;
        }

        //удалить запись в бд по id
        public bool Delete(T entity)
        {
            long id = entity.Id;
            bool isExist = EntityExist(id);
            if (!isExist)
            {
                return false;
            }
            Context.Remove(entity);

            Context.SaveChanges();

            return true;
        }
        private bool EntityExist(long id) => Context.Set<T>().Any(e => e.Id == id);
    }
}
