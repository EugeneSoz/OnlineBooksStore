using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBooksStore.Domain.Contracts.Entities
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
        //страна происхождения        
        /// <summary>
        /// Gets or sets the publisher country of origin.
        /// </summary>
        /// <value>
        /// The country of origin.
        /// </value>
        public string Country { get; set; }
    }
}
