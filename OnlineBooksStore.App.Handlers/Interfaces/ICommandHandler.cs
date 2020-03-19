using OnlineBooksStore.App.Contracts.Command;

namespace OnlineBooksStore.App.Handlers.Interfaces
{
    public interface ICommandHandler<in TCommand> where TCommand : Contracts.Command.Command
    {
        void Handle(TCommand command);
    }
}