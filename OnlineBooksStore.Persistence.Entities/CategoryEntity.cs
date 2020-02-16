using System.Collections.Generic;

namespace OnlineBooksStore.Persistence.Entities
{
    /// <summary>
    /// Represents a book category
    /// </summary>
    /// <seealso cref="EntityBase" />
    public class CategoryEntity : EntityBase
    {
        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        /// <value>
        /// The name of the category.
        /// </value>
        public virtual string NameEng { get; set; }
        //если свойство не равно null, тогда категория является подкатегорией        
        /// <summary>
        /// Gets or sets the parent category identifier.
        /// </summary>
        /// <value>
        /// The parent category identifier.
        /// </value>
        public virtual long? ParentCategoryId { get; set; }
        //для сортировки по имени родительской и текущей категории        
        /// <summary>
        /// Gets or sets the name in "Parent category name => category name" format.
        /// </summary>
        /// <value>
        /// The name of the displayed category.
        /// </value>
        public virtual string DisplayedName { get; set; }
        /// <summary>
        /// Gets or sets the parent category.
        /// </summary>
        /// <value>
        /// The parent category.
        /// </value>
        public virtual CategoryEntity ParentCategory { get; set; }
        /// <summary>
        /// Gets or sets the children categories.
        /// </summary>
        /// <value>
        /// The children categories.
        /// </value>
        public virtual List<CategoryEntity> ChildrenCategories { get; set; }
        /// <summary>
        /// Gets or sets the related books.
        /// </summary>
        /// <value>
        /// The related books.
        /// </value>
        public virtual List<BookEntity> Books { get; set; }
    }
}
