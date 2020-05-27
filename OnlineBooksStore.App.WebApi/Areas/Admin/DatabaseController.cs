using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBooksStore.App.Contracts.Command;
using OnlineBooksStore.App.Contracts.Query;
using OnlineBooksStore.App.Handlers.Command;
using OnlineBooksStore.Domain.Contracts.Models.Database;

namespace OnlineBooksStore.App.WebApi.Areas.Admin
{
    [Route("api/options")]
    [ApiController]
    [Produces("application/json")]
    public class DatabaseController : ControllerBase
    {
        private readonly TablesCommandHandlers _tablesCommandHandlers;
        public DatabaseController(TablesCommandHandlers tablesCommandHandlers)
        {
            _tablesCommandHandlers = tablesCommandHandlers ?? throw new ArgumentNullException(nameof(tablesCommandHandlers));
        }

        [HttpPost("create")]
        public DbMessageResponse CreateTables([FromBody] CreateTablesCommand command)
        {
            return new DbMessageResponse();
        }

        [HttpPost("delete")]
        public DbMessageResponse DeleteTables([FromBody] DeleteTablesCommand command)
        {
            return new DbMessageResponse();
        }

        [HttpPost("fill")]
        public DbMessageResponse FillTablesWithData([FromBody] FillTablesCommand command)
        {
            return _tablesCommandHandlers.Handle(command);
        }
    }
}
