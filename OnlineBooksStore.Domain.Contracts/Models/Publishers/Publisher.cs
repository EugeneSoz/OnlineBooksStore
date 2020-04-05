﻿using System.Collections.Generic;
using OnlineBooksStore.Domain.Contracts.Models.Books;

namespace OnlineBooksStore.Domain.Contracts.Models.Publishers
{
    /// <summary>
    /// Represents a book publisher
    /// </summary>
    public class Publisher : PublisherResponse
    {
        /// <summary>
        /// Gets or sets the related books.
        /// </summary>
        /// <value>
        /// The related books.
        /// </value>
        public List<RelatedBook> Books { get; set; }
    }
}
