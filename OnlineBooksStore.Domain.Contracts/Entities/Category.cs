using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBooksStore.Domain.Contracts.Entities
{
    /// <summary>
    /// Represents a book category
    /// </summary>
    /// <seealso cref="EntityBase" />
    public class Category : EntityBase
    {
        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        /// <value>
        /// The name of the category.
        /// </value>
        public string NameEng { get; set; }

        public IEnumerable<Book> Books { get; set; }
    }
}
