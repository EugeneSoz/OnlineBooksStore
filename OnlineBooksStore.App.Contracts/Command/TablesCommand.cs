using OnlineBooksStore.Domain.Contracts.Models.Database;

namespace OnlineBooksStore.App.Contracts.Command
{
    public abstract class TablesCommand : Command
    {
        public AreaGroup AreaGroup { get; set; }
    }

    public sealed class CreateTablesCommand : TablesCommand { }
    public sealed class DeleteTablesCommand : TablesCommand { }
    public sealed class FillTablesCommand : TablesCommand { }
}