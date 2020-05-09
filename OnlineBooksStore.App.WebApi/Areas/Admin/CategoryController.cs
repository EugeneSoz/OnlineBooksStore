using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineBooksStore.App.Contracts.Command;
using OnlineBooksStore.App.Contracts.Query;
using OnlineBooksStore.App.Handlers.Command;
using OnlineBooksStore.App.Handlers.Query;
using OnlineBooksStore.Domain.Contracts.Models.Categories;
using OnlineBooksStore.Domain.Contracts.Models.Pages;
using OnlineBooksStore.Persistence.Entities;

namespace OnlineBooksStore.App.WebApi.Areas.Admin
{
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [AutoValidateAntiforgeryToken]
    public class CategoryController : Controller
    {
        private readonly CategoryQueryHandler _queryHandler;
        private readonly CategoryCommandHandler _commandHandler;

        public CategoryController(CategoryQueryHandler queryHandler, CategoryCommandHandler commandHandler)
        {
            _queryHandler = queryHandler ?? throw new ArgumentNullException(nameof(queryHandler));
            _commandHandler = commandHandler ?? throw new ArgumentNullException(nameof(commandHandler));
        }

        [HttpGet("category/{id}")]
        public Category GetCategory([FromRoute] CategoryIdQuery query)
        {
            return _queryHandler.Handle(query);
        }
        
        [HttpPost("categories")]
        public PagedResponse<CategoryResponse> GetCategories([FromBody] PageFilterQuery query)
        {
            return _queryHandler.Handle(query);
        }

        [HttpGet("parentcategories")]
        public List<Category> GetParentCategories(ParentCategoryCategoryQuery query)
        {
            return _queryHandler.Handle(query);
        }

        [HttpPost("categoriesforselection")]
        public List<CategoryResponse> GetCategoriesForSelection([FromBody] SearchTermQuery query)
        {
            return _queryHandler.Handle(query);
        }

        [HttpPost("create")]
        public ActionResult CreateCategory([FromBody] CreateCategoryCommand command)
        {
            if (!ModelState.IsValid)
            {
                return Ok(GetServerErrors(ModelState));
            }

            CategoryEntity category;
            try
            {
                category = _commandHandler.Handle(command);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $@"Невозможно создать запись: {ex.Message}");
                return BadRequest(GetServerErrors(ModelState));
            }

            return Created("", category);
        }

        [HttpPut("update")]
        public ActionResult UpdateCategory([FromBody] UpdateCategoryCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetServerErrors(ModelState));
            }

            bool isOk;
            try
            {
                isOk = _commandHandler.Handle(command);
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
        public ActionResult DeleteCategory([FromBody] DeleteCategoryCommand command)
        {
            //если у категории есть дочерние, тогда удалить их
            bool isOk = _commandHandler.Handle(command);
            //в случае успеха - удалить саму родительскую категорию
            if (isOk)
            {
                return Delete(command, _commandHandler.Handle);
            }
            //удалить любую категорию, у которой нет дочерних
            return Delete(command, _commandHandler.Handle);
        }

        private ActionResult Delete(DeleteCategoryCommand command, Func<DeleteCategoryCommand, bool> commandHandler)
        {
            bool isOk;

            try
            {
                isOk = commandHandler.Invoke(command);
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
