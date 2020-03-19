namespace OnlineBooksStore.App.Contracts.Command
{
    public abstract class PublisherCommand : Command
    {
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

    public sealed class CreatePublisherCommand : PublisherCommand { }
    public sealed class UpdatePublisherCommand : PublisherCommand { }
    public sealed class DeletePublisherCommand : PublisherCommand { }
}