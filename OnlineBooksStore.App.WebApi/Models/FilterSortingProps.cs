﻿using System.Collections.Generic;
using OnlineBooksStore.App.WebApi.Data;
using OnlineBooksStore.App.WebApi.Data.DTO;

namespace OnlineBooksStore.App.WebApi.Models
{
    public class FilterSortingProps
    {
        public FilterSortingProps(string propertyName, string displayName)
        {
            PropertyName = propertyName;
            DisplayName = displayName;
        }

        public string PropertyName { get; }
        public string DisplayName { get; }
    }

    public class FilterProperties
    {
        public List<FilterSortingProps> GetPublisherFilterProps()
        {
            return new List<FilterSortingProps>
            {
                new FilterSortingProps(nameof(Publisher.Name), "Издательство"),
                new FilterSortingProps(nameof(Publisher.Country), "Страна нахождения")
            };
        }

        public List<FilterSortingProps> GetCategoryFilterProps()
        {
            return new List<FilterSortingProps>
            {
                new FilterSortingProps(nameof(Category.Name), "Категория"),
            };
        }

        public List<FilterSortingProps> GetBookFilterProps()
        {
            return new List<FilterSortingProps>
            {
                new FilterSortingProps(nameof(BookResponse.Title), "Название"),
                new FilterSortingProps(nameof(BookResponse.CategoryName), "Категория"),
                new FilterSortingProps(nameof(BookResponse.SubcategoryName), "Подкатегория"),
                new FilterSortingProps(nameof(BookResponse.PublisherName), "Издательство"),
                new FilterSortingProps(nameof(BookResponse.Price), "Цена")
            };
        }
    }

    public class SortingProperties
    {
        public List<FilterSortingProps> GetPublisherSortingProps()
        {
            return new List<FilterSortingProps>
            {
                new FilterSortingProps(nameof(Publisher.Id), "ID"),
                new FilterSortingProps(nameof(Publisher.Name), "Издательство"),
                new FilterSortingProps(nameof(Publisher.Country), "Страна нахождения издательства")
            };
        }

        public List<FilterSortingProps> GetCategorySortingProps()
        {
            return new List<FilterSortingProps>
            {
                new FilterSortingProps(nameof(Category.Id), "ID"),
                new FilterSortingProps(nameof(Category.Name), "Категория")
            };
        }

        public List<FilterSortingProps> GetBooksSortingProps()
        {
            return new List<FilterSortingProps>
            {
                new FilterSortingProps(nameof(BookResponse.Id), "ID"),
                new FilterSortingProps(nameof(BookResponse.Title), "Название"),
                new FilterSortingProps(nameof(BookResponse.CategoryName), "Категория"),
                new FilterSortingProps(nameof(BookResponse.SubcategoryName), "Подкатегория"),
                new FilterSortingProps(nameof(BookResponse.PublisherName), "Издательство"),
                new FilterSortingProps(nameof(BookResponse.Price), "Цена")
            };
        }
    }
}