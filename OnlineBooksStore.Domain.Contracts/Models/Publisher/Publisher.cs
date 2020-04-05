using System.Collections.Generic;

namespace OnlineBooksStore.Domain.Contracts.Models.Publisher
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
