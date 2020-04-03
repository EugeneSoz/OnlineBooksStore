using System;
using System.Collections.Generic;

namespace OnlineBooksStore.Persistence.Entities
{
    /// <summary>
    /// Represents a book publisher
    /// </summary>
    /// <seealso cref="EntityBase" />
    public class PublisherEntity : EntityBase
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
        public List<BookEntity> Books { get; set; }
    }
}
