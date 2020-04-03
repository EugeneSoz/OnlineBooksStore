using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineBooksStore.App.WebApi.Data;
using OnlineBooksStore.App.WebApi.Data.DTO;
using OnlineBooksStore.App.WebApi.Infrastructure;
using OnlineBooksStore.App.WebApi.Models;
using OnlineBooksStore.App.WebApi.Models.Repo;

namespace OnlineBooksStore.App.WebApi.Areas.Admin
{
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [AutoValidateAntiforgeryToken]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepo _repo;

        public CategoryController(ICategoryRepo repo) => _repo = repo;

        [HttpGet("category/{id}")]
        public async Task<Category> GetCategoryAsync(long id)
        {
            return await _repo.GetCategoryAsync(id);
        }

        [HttpPost("categories")]
        public async Task<PagedResponse<CategoryResponse>> GetCategoriesAsync([FromBody] QueryOptions options)
        {
            PagedList<CategoryResponse> categories = await _repo.GetCategoriesAsync(options);

            return categories.MapPagedResponse();
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
            if (!ModelState.IsValid)
            {
                return Ok(GetServerErrors(ModelState));
            }

            Category category;
            try
            {
                category = categoryDTO.MapCategory();
                await _repo.AddAsync(category);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $@"Невозможно создать запись: {ex.Message}");
                return BadRequest(GetServerErrors(ModelState));
            }

            return Created("", category);
        }

        [HttpPut("update")]
        public async Task<ActionResult> UpdateCategoryAsync([FromBody] CategoryDTO categoryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetServerErrors(ModelState));
            }

            bool isOk;
            try
            {
                Category category = categoryDTO.MapCategory();
                isOk = await _repo.UpdateAsync(category);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $@"Невозможно сохранить запись: {ex.Message}");
                return BadRequest(GetServerErrors(ModelState));
            }

            if (!isOk)
            {
                return NotFound();
            }
            return Ok();
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

        private async Task<ActionResult> DeleteAsync<T>(T entity, Func<T, Task<bool>> deleteMethod)
        {
            bool isOk;

            try
            {
                isOk = await deleteMethod?.Invoke(entity);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $@"Невозможно удалить запись: {ex.Message}");
                return BadRequest(GetServerErrors(ModelState));
            }

            if (!isOk)
            {
                return NotFound();
            }
            return NoContent();
        }

        private List<string> GetServerErrors(ModelStateDictionary modelstate)
        {
            List<string> errors = new List<string>();
            foreach (ModelStateEntry error in modelstate.Values)
            {
                foreach (ModelError e in error.Errors)
                {
                    errors.Add(e.ErrorMessage);
                }
            }

            return errors;
        }
    }
}
