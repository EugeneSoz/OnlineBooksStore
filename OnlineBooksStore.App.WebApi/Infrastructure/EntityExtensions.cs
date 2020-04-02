using System.Collections.Generic;
using OnlineBooksStore.App.WebApi.Data;
using OnlineBooksStore.App.WebApi.Data.DTO;
using OnlineBooksStore.App.WebApi.Models;

namespace OnlineBooksStore.App.WebApi.Infrastructure
{
    public static class EntityExtensions
    {
        public static BookResponse MapBookResponse(this Book book)
        {
            return new BookResponse
            {
                Id = book.Id,
                Title = book.Title,
                Authors = book.Authors,
                Year = book.Year,
                Language = book.Language,
                PageCount = book.PageCount,
                Description = book.Description,
                Price = book.Price,
                BookCover = book.BookCover,
                CategoryID = book.CategoryID,
                PublisherID = book.PublisherID,
                CategoryName = book.Category.ParentCategory == null 
                    ? string.Empty 
                    : book.Category.ParentCategory.Name,
                SubcategoryName = book.Category.Name,
                PublisherName = book.Publisher.Name
            };
        }

        public static Book MapBook(this BookDTO bookDTO)
        {
            return new Book
            {
                Id = bookDTO.Id,
                Title = bookDTO.Title,
                Authors = bookDTO.Authors,
                Year = bookDTO.Year,
                Language = bookDTO.Language,
                PageCount = bookDTO.PageCount,
                Description = bookDTO.Description,
                Price = bookDTO.Price,
                BookCover = bookDTO.BookCover,
                CategoryID = bookDTO.CategoryID,
                PublisherID = bookDTO.PublisherID
            };
        }

        public static CategoryResponse MapCategoryResponse(this Category category)
        {
            return new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                ParentCategoryID = category.ParentCategory?.Id ?? null,
                ParentCategoryName = category.ParentCategory?.Name ?? "",
                DisplayedName = category.DisplayedName
            };
        }

        public static Category MapCategory(this CategoryDTO categoryDTO)
        {
            return new Category
            {
                Id = categoryDTO.Id,
                Name = categoryDTO.Name,
                ParentCategoryID = categoryDTO.ParentCategoryID
            };
        }

        public static Publisher MapPublisher(this PublisherDTO publisherDTO)
        {
            return new Publisher
            {
                Id = publisherDTO.Id,
                Name = publisherDTO.Name,
                Country = publisherDTO.Country
            };
        }

        public static PagedResponse<T> MapPagedResponse<T>(this PagedList<T> response)
        {
            List<int> numbers = new List<int>();
            for (int i = response.LeftBoundary; i <= response.RightBoundary; i++)
            {
                numbers.Add(i);
            }

            return new PagedResponse<T>
            {
                Entities = response.Entities ?? new List<T>(),
                Pagination = new Pagination
                {
                    CurrentPage = response.CurrentPage,
                    PageSize = response.PageSize,
                    TotalPages = response.TotalPages,
                    HasPreviousPage = response.HasPreviousPage,
                    HasNextPage = response.HasNextPage,
                    LeftBoundary = response.LeftBoundary,
                    RightBoundary = response.RightBoundary
                },
                PageNumbers = numbers
            };
        }
    }
}
