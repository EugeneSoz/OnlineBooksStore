namespace OnlineBooksStore.Domain.Contracts.Models.Publisher
{
    /// <summary>
    /// Represents a book publisher
    /// </summary>
    /// <seealso cref="EntityBase" />
    public class PublisherResponse : EntityBase
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
    }
}