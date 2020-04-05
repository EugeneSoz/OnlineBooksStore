using System;
using System.Collections.Generic;
using OnlineBooksStore.Domain.Contracts.Entities;
using OnlineBooksStore.Domain.Contracts.Models;
using OnlineBooksStore.Domain.Contracts.Models.Pages;
using OnlineBooksStore.Domain.Contracts.Repositories;

namespace OnlineBooksStore.Persistence.EF.Mvc
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly DataContext _context;

        public CategoriesRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Category> Categories => _context.Categories;

        public PagedList<Category> GetCategories(QueryOptions options)
        {
            return new PagedList<Category>(_context.Categories, options);
        }

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        public void DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}
