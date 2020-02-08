using System;
using System.Collections.Generic;
using System.Text;
using OnlineBooksStore.Domain.Contracts.Entities;
using OnlineBooksStore.Domain.Contracts.Repositories;

namespace OnlineBooksStore.Persistence.EF
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<Category> Categories => throw new NotImplementedException();

        public void AddCategory(Category category)
        {
            var vs = new List<string>();
            throw new NotImplementedException();
        }

        public void DeleteCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public void UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
