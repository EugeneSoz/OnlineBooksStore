using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineBooksStore.Domain.Contracts.Models.Pages
{
    public class PagedList<T>
    {
        //кол-во отображаемых страниц
        private readonly int _displayedPagesCount = 5;
        //кол-во страниц слева от текущей
        private readonly int _leftPagesCount;
        private readonly int _rightPagesCount;
        //кол-во страниц, добавляемых к левой границе в случае выбора крайнего значения правой границы
        private int _leftDelta;
        private int _rightDelta;

        public PagedList(IQueryable<T> query, QueryOptions options = null)
        {
            _leftPagesCount = _rightPagesCount = _displayedPagesCount / 2;
            CurrentPage = options.CurrentPage;
            PageSize = options.PageSize;
            TotalPages = (int)Math.Ceiling(query.Count() / (double)PageSize);
            Entities = new List<T>();
            Entities.AddRange(query.Skip((CurrentPage - 1) * PageSize).Take(PageSize));
        }

        public PagedList()
        {
            
        }

        public List<T> Entities { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
        //значение левой границы
        public int LeftBoundary
        {
            get
            {
                //то, что нужно добавить к левой границе
                _leftDelta = CurrentPage + _rightPagesCount > TotalPages
                    ? CurrentPage + _rightPagesCount - TotalPages
                    : 0;
                int leftBoundary = CurrentPage - _leftPagesCount - _leftDelta;
                return leftBoundary <= 0 ? 1 : leftBoundary;
            }
        }
        //значение правой границы
        public int RightBoundary
        {
            get
            {
                _rightDelta = CurrentPage - _leftPagesCount < 1
                    ? 1 - (CurrentPage - _leftPagesCount)
                    : 0;
                int rightBoundary = CurrentPage + _rightPagesCount + _rightDelta;
                return rightBoundary > TotalPages ? TotalPages : rightBoundary;
            }
        }
    }
}
