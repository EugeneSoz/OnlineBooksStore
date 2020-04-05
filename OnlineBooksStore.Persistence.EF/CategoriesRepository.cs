using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OnlineBooksStore.Domain.Contracts.Models;
using OnlineBooksStore.Domain.Contracts.Models.Pages;
using OnlineBooksStore.Domain.Contracts.Repositories;
using Book = OnlineBooksStore.Domain.Contracts.Models.Book;

namespace OnlineBooksStore.Persistence.EF
{
    public class CategoriesRepository : BaseRepo<Category>, ICategoriesRepository
    {
        public CategoriesRepository(StoreDbContext ctx) : base(ctx) { }
        public IEnumerable<Category> Categories { get; }
        public Domain.Contracts.Models.Pages.PagedList<Category> GetCategories(QueryOptions options)
        {
            if (string.IsNullOrEmpty(options.SortPropertyName))
            {
                options.SortPropertyName = nameof(Category.Name);
            }

            var processing = new QueryProcessing<Category>(options);

            var entities = GetEntities();
            var query = from subcategories in entities
                join categories in entities
                    on subcategories.ParentId equals categories.Id into categoriesWithParent
                from cwp in categoriesWithParent.DefaultIfEmpty()
                select new Category
                {
                    Id = subcategories.Id,
                    Name = subcategories.Name,
                    ParentId = subcategories.ParentId,
                    DisplayedName = subcategories.ParentId == null
                        ? subcategories.Name
                        : subcategories.ParentCategory.Name + " <=> " + subcategories.Name
                };

            IQueryable<CategoryResponse> processedCategories;

            if (options.SortPropertyName == $"{nameof(Category.Name)}")
            {
                options.SortPropertyName = $"{nameof(Category.DisplayedName)}";
                processedCategories = processing.ProcessQuery(query)
                    .Select(e => e.MapCategoryResponse());
            }
            else
            {
                processedCategories = processing.ProcessQuery(query)
                    .Select(e => e.MapCategoryResponse());
            }

            var pagedList = new PagedList<CategoryResponse>(processedCategories, options);

            return pagedList;
        }

        public Category GetCategory(long id)
        {
            var categories = GetEntities();
            IQueryable<Book> books = Context.Books.OrderBy(b => b.Title);

            var result = await categories
                .GroupJoin(books, c => c.Id, b => b.CategoryID, (c, relbooks) =>
                    new Category
                    {
                        Id = c.Id,
                        Name = c.Name,
                        ParentId = c.ParentId,
                        Books = relbooks.ToList()
                    })
                .SingleOrDefaultAsync(p => p.Id == id);

            return result;
        }

        public List<StoreCategoryResponse> GetStoreCategories()
        {
            var query = GetEntities();
            var categories = query.Where(c => c.ParentId == null).OrderBy(c => c.Id).ToList();
            var subCategories = query.Where(s => s.ParentId != null)
                .OrderBy(s => s.Name)
                .OrderBy(s => s.ParentId).ToListAsync();

            foreach (var category in categories)
            {
                category.ChildrenCategories = new List<Category>();
                foreach (var subcategory in subCategories)
                {
                    if (subcategory.ParentCategoryID == category.Id)
                    {
                        category.ChildrenCategories.Add(subcategory);
                    }
                }
            }

            return CreateStoreCategoryResponses(categories);
        }

        public List<Category> GetParentCategories()
        {
            IQueryable<Category> query = GetEntities()
                .Where(c => c.ParentId == null)
                .OrderBy(c => c.Name);

            return await query.ToListAsync();
        }

        public Category AddCategory(Category category)
        {
            return Add(category);
        }

        public bool UpdateCategory(Category category)
        {
            return Update(category);
        }

        public bool DeleteCategory(Category category)
        {
            var query = GetEntities().Where(c => c.ParentId == parentCategoryId);
            var categories = query.ToList();
            if (categories.Any())
            {
                Context.RemoveRange(categories);
                Context.SaveChanges();
                return true;
            }

            return false;
        }

        private List<StoreCategoryResponse> CreateStoreCategoryResponses(List<Category> categories)
        {
            var storeCategories = new List<StoreCategoryResponse>();

            foreach (var category in categories)
            {
                bool hasChildren = category.ChildrenCategories.Any();
                var storeCategory = new StoreCategoryResponse
                {
                    Id = category.Id,
                    ControlId = $"c_{category.Id}",
                    Name = category.Name,
                    IsParent = hasChildren,
                    HasChildren = hasChildren,
                };

                if (hasChildren)
                {
                    var children = new List<StoreCategoryResponse>();
                    foreach (var child in category.ChildrenCategories)
                    {
                        var storeChildCategory = new StoreCategoryResponse
                        {
                            Id = child.Id,
                            ControlId = $"c_{child.Id}",
                            ParentId = category.Id,
                            Name = child.Name,
                            IsParent = false,
                            HasChildren = false,
                        };

                        children.Add(storeChildCategory);
                    }
                    storeCategory.Children = children;
                }

                storeCategories.Add(storeCategory);
            }

            return storeCategories;
        }
    }
}
