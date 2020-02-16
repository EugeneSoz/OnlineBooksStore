namespace OnlineBooksStore.Persistence.Entities
{
    /// <summary>
    /// Represents a book
    /// </summary>
    public class BookEntity : EntityBase
    {
        /// <summary>
        /// Gets or sets the book title.
        /// </summary>
        /// <value>
        /// The book title.
        /// </value>
        public virtual string Title { get; set; }

        /// <summary>
        /// Gets or sets the authors.
        /// </summary>
        /// <value>
        /// The authors.
        /// </value>
        public virtual string Authors { get; set; }

        //дата публикации        
        /// <summary>
        /// Gets or sets the publication year.
        /// </summary>
        /// <value>
        /// The publication year.
        /// </value>
        public virtual int Year { get; set; }

        /// <summary>
        /// Gets or sets the book language.
        /// </summary>
        /// <value>
        /// The book language.
        /// </value>
        public virtual string Language { get; set; }

        /// <summary>
        /// Gets or sets the book page count.
        /// </summary>
        /// <value>
        /// The book page count.
        /// </value>
        public virtual int PageCount { get; set; }

        /// <summary>
        /// Gets or sets the book description.
        /// </summary>
        /// <value>
        /// The book description.
        /// </value>
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets or sets the purchase book price.
        /// </summary>
        /// <value>
        /// The book purchase price.
        /// </value>
        public virtual decimal PurchasePrice { get; set; }

        /// <summary>
        /// Gets or sets the retail book price.
        /// </summary>
        /// <value>
        /// The retail book price.
        /// </value>
        public virtual decimal RetailPrice { get; set; }

        /// <summary>
        /// Gets or sets a link to this book cover.
        /// </summary>
        /// <value>
        /// A link to this book cover.
        /// </value>
        public virtual string BookCover { get; set; }
        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        /// <value>
        /// The category identifier.
        /// </value>
        public virtual long? CategoryId { get; set; }
        /// <summary>
        /// Gets or sets the publisher identifier.
        /// </summary>
        /// <value>
        /// The publisher identifier.
        /// </value>
        public virtual long? PublisherId { get; set; }
        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public virtual CategoryEntity Category { get; set; }
        /// <summary>
        /// Gets or sets the publisher.
        /// </summary>
        /// <value>
        /// The publisher.
        /// </value>
        public virtual PublisherEntity Publisher { get; set; }
    }
}
