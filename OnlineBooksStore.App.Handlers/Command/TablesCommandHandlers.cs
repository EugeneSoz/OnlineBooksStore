using OnlineBooksStore.App.Contracts.Command;
using OnlineBooksStore.App.Handlers.Interfaces;
using OnlineBooksStore.Domain.Contracts.Models.Database;
using OnlineBooksStore.Domain.Contracts.Services;
using OnlineBooksStore.Persistence.EF;

namespace OnlineBooksStore.App.Handlers.Command
{
    public class TablesCommandHandlers : 
        ICommandHandler<CreateTablesCommand, DbMessageResponse>,
        ICommandHandler<DeleteTablesCommand, DbMessageResponse>,
        ICommandHandler<FillTablesCommand, DbMessageResponse>
    {
        private readonly StoreDbContext _context;
        private readonly IDbDataService _dbDataService;

        public TablesCommandHandlers(StoreDbContext context, IDbDataService dbDataService)
        {
            _context = context;
            _dbDataService = dbDataService;
        }

        public DbMessageResponse Handle(CreateTablesCommand command)
        {
            throw new System.NotImplementedException();
        }

        public DbMessageResponse Handle(DeleteTablesCommand command)
        {
            throw new System.NotImplementedException();
        }

        public DbMessageResponse Handle(FillTablesCommand command)
        {
            return _dbDataService.SeedDatabase(_context);
        }
    }
}