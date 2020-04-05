using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OnlineBooksStore.Domain.Contracts.Models;
using OnlineBooksStore.Domain.Contracts.Models.Category;
using OnlineBooksStore.Domain.Contracts.Models.Pages;
using OnlineBooksStore.Domain.Contracts.Repositories;
using OnlineBooksStore.Persistence.Entities;
using Book = OnlineBooksStore.Domain.Contracts.Models.Book;

namespace OnlineBooksStore.Persistence.EF
{
    public class CategoriesRepository : BaseRepo<CategoryEntity>, ICategoriesRepository
    {
        public CategoriesRepository(StoreDbContext ctx) : base(ctx) { }
        public PagedList<CategoryEntity> GetCategories(QueryOptions options)
        {
            if (string.IsNullOrEmpty(options.SortPropertyName))
            {
                options.SortPropertyName = nameof(CategoryEntity.Name);
            }

            var processing = new QueryProcessing<CategoryEntity>(options);

            var entities = GetEntities();
            var query = from subcategories in entities
                join categories in entities
                    on subcategories.ParentId equals categories.Id into categoriesWithParent
                from cwp in categoriesWithParent.DefaultIfEmpty()
                select new CategoryEntity
                {
                    Id = subcategories.Id,
                    Name = subcategories.Name,
                    ParentId = subcategories.ParentId,
                    ParentAndChildName = subcategories.ParentId == null
                        ? subcategories.Name
                        : subcategories.ParentCategory.Name + " <=> " + subcategories.Name
                };

            IQueryable<CategoryEntity> processedCategories;

            if (options.SortPropertyName == $"{nameof(CategoryEntity.Name)}")
            {
                options.SortPropertyName = $"{nameof(CategoryEntity.ParentAndChildName)}";
                processedCategories = processing.ProcessQuery(query);
            }
            else
            {
                processedCategories = processing.ProcessQuery(query);
            }

            var pagedList = new PagedList<CategoryEntity>(processedCategories, options);

            return pagedList;
        }

        public CategoryEntity GetCategory(long id)
        {
            var categories = GetEntities();
            IQueryable<BookEntity> books = Context.Books.OrderBy(b => b.Title);

            var result = categories
                .GroupJoin(books, c => c.Id, b => b.CategoryId, (c, relbooks) =>
                    new CategoryEntity
                    {
                        Id = c.Id,
                        Name = c.Name,
                        ParentId = c.ParentId,
                        Books = relbooks.ToList()
                    })
                .SingleOrDefault(p => p.Id == id);

            return result;
        }

        public List<CategoryEntity> GetStoreCategories()
        {
            var query = GetEntities();
            var categories = query.Where(c => c.ParentId == null).OrderBy(c => c.Id).ToList();
            var subCategories = query.Where(s => s.ParentId != null)
                .OrderBy(s => s.Name)
                .ThenBy(s => s.ParentId).ToList();

            foreach (var category in categories)
            {
                category.ChildrenCategories = new List<CategoryEntity>();
                foreach (var subcategory in subCategories)
                {
                    if (subcategory.ParentId == category.Id)
                    {
                        category.ChildrenCategories.Add(subcategory);
                    }
                }
            }

            return categories;
        }

        public List<CategoryEntity> GetParentCategories()
        {
            IQueryable<CategoryEntity> query = GetEntities()
                .Where(c => c.ParentId == null)
                .OrderBy(c => c.Name);

            return query.ToList();
        }

        public CategoryEntity AddCategory(CategoryEntity category)
        {
            return Add(category);
        }

        public bool UpdateCategory(CategoryEntity category)
        {
            return Update(category);
        }

        public bool DeleteChildrenCategories(long parentId)
        {
            var query = GetEntities().Where(c => c.ParentId == parentId);
            var categories = query.ToList();
            if (categories.Any())
            {
                Context.RemoveRange(categories);
                Context.SaveChanges();
                return true;
            }

            return false;
        }

        public bool DeleteCategory(CategoryEntity category)
        {
            return Delete(category);
        }
    }
}
