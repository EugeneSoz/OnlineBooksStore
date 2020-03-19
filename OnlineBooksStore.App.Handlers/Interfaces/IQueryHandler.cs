namespace OnlineBooksStore.App.Handlers.Interfaces
{
    public interface IQueryHandler<in TQuery, out TResult> where TQuery : Contracts.Query.Query
    {
        TResult Handle(TQuery query);
    }
}