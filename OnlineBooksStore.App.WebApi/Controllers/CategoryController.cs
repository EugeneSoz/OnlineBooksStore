using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBooksStore.App.WebApi.Data;
using OnlineBooksStore.App.WebApi.Data.DTO;
using OnlineBooksStore.App.WebApi.Infrastructure;
using OnlineBooksStore.App.WebApi.Models;
using OnlineBooksStore.App.WebApi.Models.Repo;

namespace OnlineBooksStore.App.WebApi.Controllers
{
    [Authorize(Roles = "Administrator")]
    [AutoValidateAntiforgeryToken]
    public class CategoryController : BaseController
    {
        private readonly ICategoryRepo _repo;

        public CategoryController(ICategoryRepo repo) => _repo = repo;

        [HttpGet("category/{id}")]
        public async Task<Category> GetCategoryAsync(long id)
        {
            return await _repo.GetCategoryAsync(id);
        }

        [HttpPost("categories")]
        public async Task<PagedResponse<CategoryResponse>> GetCategoriesAsync(
            [FromBody] QueryOptions options)
        {
            PagedList<CategoryResponse> categories = await _repo.GetCategoriesAsync(options);

            return categories.MapPagedResponse();
        }

        [HttpGet("storecategories")]
        [AllowAnonymous]
        public async Task<List<StoreCategoryResponse>> GetStoreCategoriesAsync()
        {
            return await _repo.GetStoreCategoriesAsync();
        }

        [HttpGet("parentcategories")]
        public async Task<List<Category>> GetParentCategoriesAsync()
        {
            return await _repo.GetParentCategoriesAsync();
        }

        [HttpPost("categoriesforselection")]
        public async Task<List<CategoryResponse>> GetCategoriesForSelectionAsync([FromBody] SearchTerm term)
        {
            QueryOptions options = new QueryOptions
            {
                SearchTerm = term.Value,
                SearchPropertyNames = new string[] { nameof(Publisher.Name) },
                SortPropertyName = nameof(Publisher.Name),
                PageSize = 10
            };

            PagedList<CategoryResponse> pagedCategories = await _repo.GetCategoriesAsync(options);

            return pagedCategories.Entities;
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateCategoryAsync([FromBody] CategoryDTO categoryDTO)
        {
            Category category = categoryDTO.MapCategory();
            return await CreateAsync(category, _repo.AddAsync);
        }

        [HttpPut("update")]
        public async Task<ActionResult> UpdateCategoryAsync([FromBody] CategoryDTO categoryDTO)
        {
            Category category = categoryDTO.MapCategory();
            return await UpdateAsync(category, _repo.UpdateAsync);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteCategoryAsync([FromBody] CategoryDTO categoryDTO)
        {
            Category category = categoryDTO.MapCategory();
            //если у категории есть дочерние, тогда удалить их
            if (category.ParentCategoryID == null)
            {
                bool isOk = await _repo.DeleteAsync(category.Id);
                //в случае успеха - удалить саму родительскую категорию
                if (isOk)
                {
                    return await DeleteAsync(category, _repo.DeleteAsync);
                }
            }
            //удалить любую категорию, у которой нет дочерних
            return await DeleteAsync(category, _repo.DeleteAsync);
        }
    }
}
