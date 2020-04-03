using System.Collections.Generic;
using OnlineBooksStore.Domain.Contracts.Entities;

namespace OnlineBooksStore.Domain.Contracts.Models
{
    /// <summary>
    /// Represents a book publisher
    /// </summary>
    /// <seealso cref="EntityBase" />
    public class Publisher : EntityBase
    {
        /// <summary>
        /// Gets or sets the publisher name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the publisher country of origin.
        /// </summary>
        /// <value>
        /// The country of origin.
        /// </value>
        public string Country { get; set; }
        /// <summary>
        /// Gets or sets the related books.
        /// </summary>
        /// <value>
        /// The related books.
        /// </value>
        public List<RelatedBook> Books { get; set; }
    }
}
