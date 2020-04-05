namespace OnlineBooksStore.App.Handlers.Interfaces
{
    public interface ICommandHandler<in TCommand, out TResult> where TCommand : Contracts.Command.Command
    {
        TResult Handle(TCommand command);
    }
}