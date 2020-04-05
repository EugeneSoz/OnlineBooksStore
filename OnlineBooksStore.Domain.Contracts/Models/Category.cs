using System.Collections.Generic;

namespace OnlineBooksStore.Domain.Contracts.Models
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
        public string Name { get; set; }
        public long? ParentId { get; set; }
        public string DisplayedName { get; set; }
        public Category ParentCategory { get; set; }
        public List<Category> ChildrenCategories { get; set; }

        public IEnumerable<RelatedBook> Books { get; set; }
    }
}
