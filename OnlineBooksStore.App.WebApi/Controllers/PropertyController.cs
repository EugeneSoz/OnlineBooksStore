using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OnlineBooksStore.App.WebApi.Data.DTO;
using OnlineBooksStore.App.WebApi.Models;

namespace OnlineBooksStore.App.WebApi.Controllers
{
    [Route("api/properties")]
    [Produces("application/json")]
    public class PropertyController : ControllerBase
    {
        [HttpGet("pub_filter")]
        public List<FilterSortingProps> GetPublisherFilterProps()
        {
            FilterProperties filterProperties = new FilterProperties();

            return filterProperties.GetPublisherFilterProps();
        }

        [HttpGet("cat_filter")]
        public List<FilterSortingProps> GetCategoryFilterProps()
        {
            FilterProperties filterProperties = new FilterProperties();

            return filterProperties.GetCategoryFilterProps();
        }

        [HttpGet("book_filter")]
        public List<FilterSortingProps> GetBookFilterProps()
        {
            FilterProperties filterProperties = new FilterProperties();

            return filterProperties.GetBookFilterProps();
        }

        [HttpGet("pub_sorting")]
        public List<FilterSortingProps> GetPublisherSortingProps()
        {
            SortingProperties sortingProperties = new SortingProperties();

            return sortingProperties.GetPublisherSortingProps();
        }

        [HttpGet("cat_sorting")]
        public List<FilterSortingProps> GetCategorySortingProps()
        {
            SortingProperties sortingProperties = new SortingProperties();

            return sortingProperties.GetCategorySortingProps();
        }

        [HttpGet("book_sorting")]
        public List<FilterSortingProps> GetBookSortingProps()
        {
            SortingProperties sortingProperties = new SortingProperties();

            return sortingProperties.GetBooksSortingProps();
        }

        [HttpGet("dropdown")]
        public Dropdown GetDropdowns()
        {
            return new Dropdown();
        }
    }
}
