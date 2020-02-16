namespace OnlineBooksStore.Persistence.Entities
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
        public virtual long Id { get; set; }
    }
}
