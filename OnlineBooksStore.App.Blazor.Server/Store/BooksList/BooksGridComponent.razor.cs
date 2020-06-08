using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OnlineBooksStore.App.Contracts.Query;
using OnlineBooksStore.Domain.Contracts.Models;
using OnlineBooksStore.Domain.Contracts.Models.Books;
using OnlineBooksStore.Domain.Contracts.Models.Pages;
using OnlineBooksStore.Domain.Contracts.Services;
using OnlineBooksStore.Integration.Contracts.Admin;

namespace OnlineBooksStore.App.Blazor.Server.Store.BooksList
{
    public partial class BooksGridComponent
    {
        private List<int> _cols;
        private List<int> _rows;
        private int _displayedBooksCount;
        private List<BookResponse> _books;
        private List<int> _pageNumbers;
        private Pagination _pagination;

        [Inject] private IBookClientService BooksClientService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var query = new PageFilterQuery();
            var response = await BooksClientService.GetBooksAsync(query);

            _displayedBooksCount = response?.Entities.Count ?? 0;
            SetBooksRowsAndCol(4);
            _books = response?.Entities ?? new List<BookResponse>();
            _pageNumbers = response?.PageNumbers ?? new List<int>();
            _pagination = response?.Pagination;
        }

        private void SetBooksRowsAndCol(int cardsCountInRow) {
            _cols = new List<int>();
            _rows = new List<int>();

            for (var i = 0; i < cardsCountInRow; i++)
            {
                _cols.Add(i);
            }
            var row = Math.Ceiling(_displayedBooksCount / (double)cardsCountInRow);
            for (var i = 0; i < row; i++)
            {
                _rows.Add(i);
            }
        }

        protected int GetBookIndex(int row, int column) {
            return 4 * row + column;
        }

        protected bool IsColEmpty(int row, int column) {
            return GetBookIndex(row, column) >= _displayedBooksCount;
        }
    }
}