using System.Collections.Generic;
using System.Linq;
using OnlineBooksStore.Domain.Contracts.Entities;
using OnlineBooksStore.Domain.Contracts.Models.Books;

namespace OnlineBooksStore.Domain.Contracts.Models
{
    public class Cart
    {
        private List<OrderLine> _selections = new List<OrderLine>();

        public Cart AddItem(Book book, int quantity)
        {
            var line = _selections.FirstOrDefault(l => l.BookId == book.Id);
            if (line != null)
            {
                line.Quantity += quantity;
            }
            else
            {
                _selections.Add(new OrderLine
                {
                    BookId = book.Id,
                    Book = book,
                    Quantity = quantity
                });
            }
            return this;
        }

        public Cart RemoveItem(long bookId)
        {
            _selections.RemoveAll(l => l.BookId == bookId);
            return this;
        }
        public void Clear() => _selections.Clear();
        public IEnumerable<OrderLine> Selections => _selections;
    }
}