using System;
using System.Collections.Generic;
using System.Linq;
using OnlineBooksStore.App.WebApi.Data.DTO;

namespace OnlineBooksStore.App.WebApi.Models
{
    public class PagedList<T>
    {
        //кол-во отображаемых страниц
        private int displayedPagesCount = 5;
        //кол-во страниц слева от текущей
        private int leftPagesCount = 0;
        private int rightPagesCount = 0;
        //кол-во страниц, добавляемых к левой границе в случае выбора крайнего значения правой границы
        private int leftDelta = 0;
        private int rightDelta = 0;

        public PagedList(IQueryable<T> query, QueryOptions options = null)
        {
            leftPagesCount = rightPagesCount = displayedPagesCount / 2;
            CurrentPage = options.CurrentPage;
            PageSize = options.PageSize;
            TotalPages = (int)Math.Ceiling(query.Count() / (double)PageSize);
            Entities = new List<T>();
            Entities.AddRange(query.Skip((CurrentPage - 1) * PageSize).Take(PageSize));
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
                leftDelta = CurrentPage + rightPagesCount > TotalPages
                    ? CurrentPage + rightPagesCount - TotalPages
                    : 0;
                int leftBoundary = CurrentPage - leftPagesCount - leftDelta;
                return leftBoundary <= 0 ? 1 : leftBoundary;
            }
        }
        //значение правой границы
        public int RightBoundary
        {
            get
            {
                rightDelta = CurrentPage - leftPagesCount < 1
                    ? 1 - (CurrentPage - leftPagesCount)
                    : 0;
                int rightBoundary = CurrentPage + rightPagesCount + rightDelta;
                return rightBoundary > TotalPages ? TotalPages : rightBoundary;
            }
        }
    }
}
