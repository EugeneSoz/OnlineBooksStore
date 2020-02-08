using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBooksStore.Domain.Contracts.Entities
{
    /// <summary>
    /// Base entity class with the identifier
    /// </summary>
    public class EntityBase
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public long Id { get; set; }
    }
}
