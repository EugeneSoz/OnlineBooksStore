using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineBooksStore.App.WebApi.Data;
using OnlineBooksStore.App.WebApi.Data.DTO;
using OnlineBooksStore.App.WebApi.Infrastructure;

namespace OnlineBooksStore.App.WebApi.Models.Repo
{
    public class CategoryRepo : BaseRepo<Category>, ICategoryRepo
    {
        public CategoryRepo(StoreDbContext ctx) : base(ctx) { }

        public async Task<Category> GetCategoryAsync(long id)
        {
            IQueryable<Category> categories = GetEntities();
            IQueryable<Book> books = Context.Books.OrderBy(b => b.Title);

            Category result = await categories
                .GroupJoin(books, c => c.Id, b => b.CategoryID, (c, relbooks) =>
                new Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    ParentCategoryID = c.ParentCategoryID,
                    Books = relbooks.ToList()
                })
                .SingleOrDefaultAsync(p => p.Id == id);

            return result;
        }

        public async Task<PagedList<CategoryResponse>> GetCategoriesAsync(QueryOptions options)
        {
            if (string.IsNullOrEmpty(options.SortPropertyName))
            {
                options.SortPropertyName = nameof(Category.Name);
            }

            QueryProcessing<Category> processing = new QueryProcessing<Category>(options);

            IQueryable<Category> entities = GetEntities();
            IQueryable<Category> query = entities
                .GroupJoin(entities, subcategory => subcategory.ParentCategoryID,
                    category => category.Id, (Category subcategory, IEnumerable<Category> categories) =>
                 new
                 {
                     subcategory,
                     categories
                 })
                .SelectMany(newCategory => newCategory.categories.DefaultIfEmpty(),
                  (temp0, categoryWithDisplayedName) =>
                     new Category
                     {
                         Id = temp0.subcategory.Id,
                         Name = temp0.subcategory.Name,
                         ParentCategoryID = temp0.subcategory.ParentCategoryID,
                         DisplayedName = (temp0.subcategory.ParentCategoryID == null)
                         ? temp0.subcategory.Name
                         : ((temp0.subcategory.ParentCategory.Name + " <=> ") + temp0.subcategory.Name)
                     }
               );
            //from subcategories in entities
            //join categories in entities
            //on subcategories.ParentCategoryID equals categories.Id into categoriesWithParent
            //from cwp in categoriesWithParent.DefaultIfEmpty()
            //select new Category
            //{
            //    Id = subcategories.Id,
            //    Name = subcategories.Name,
            //    ParentCategoryID = subcategories.ParentCategoryID,
            //    DisplayedName = subcategories.ParentCategoryID == null
            //        ? subcategories.Name
            //        : subcategories.ParentCategory.Name + " <=> " + subcategories.Name
            //};

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

            PagedList<CategoryResponse> pagedList = await Task.Run(() => 
                new PagedList<CategoryResponse>(processedCategories, options));

            return pagedList;
        }

        public async Task<List<StoreCategoryResponse>> GetStoreCategoriesAsync()
        {
            IQueryable<Category> query = GetEntities();
            IQueryable<Category> processedCategories = query
                .Where(c => c.ParentCategoryID == null)
                .GroupJoin(query.OrderBy(s => s.Name),
                    c => c.Id, s => s.ParentCategoryID, 
                    (c, subcategories) => new { c, subcategories })
                .OrderBy(joined => joined.c.Name)
                .Select(joined => new Category {
                    Id = joined.c.Id,
                    Name = joined.c.Name,
                    ChildrenCategories = joined.subcategories.ToList()
                });

            List<Category> categories = await processedCategories.ToListAsync();
            
            return CreateStoreCategoryResponses(categories);
        }

        public async Task<List<Category>> GetParentCategoriesAsync()
        {
            IQueryable<Category> query = GetEntities()
                .Where(c => c.ParentCategoryID == null)
                .OrderBy(c => c.Name);

            return await query.ToListAsync();
        }

        //метод для удаления дочерних категорий
        public async Task<bool> DeleteAsync(long parentCategoryId)
        {
            IQueryable<Category> query = GetEntities().Where(c => c.ParentCategoryID == parentCategoryId);
            List<Category> categories = await query.ToListAsync();
            if (categories.Any())
            {
                Context.RemoveRange(categories);
                await Context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        private List<StoreCategoryResponse> CreateStoreCategoryResponses(List<Category> categories)
        {
            List<StoreCategoryResponse> storeCategories = new List<StoreCategoryResponse>();

            foreach (Category category in categories)
            {
                bool hasChildren = category.ChildrenCategories.Any();
                StoreCategoryResponse storeCategory = new StoreCategoryResponse
                {
                    Id = category.Id,
                    ControlId = $"c_{category.Id}",
                    Name = category.Name,
                    IsParent = hasChildren,
                    HasChildren = hasChildren,
                };

                if (hasChildren)
                {
                    List<StoreCategoryResponse> children = new List<StoreCategoryResponse>();
                    foreach (Category child in category.ChildrenCategories)
                    {
                        StoreCategoryResponse storeChildCategory = new StoreCategoryResponse
                        {
                            Id = child.Id,
                            ControlId = $"c_{child.Id}",
                            ParentCategoryID = category.Id,
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
