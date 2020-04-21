using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBooksStore.App.Contracts.Command;
using OnlineBooksStore.App.Contracts.Query;
using OnlineBooksStore.App.Handlers.Command;
using OnlineBooksStore.App.Handlers.Query;
using OnlineBooksStore.Domain.Contracts.Models.Orders;

namespace OnlineBooksStore.App.WebApi.Areas.Orders
{
    [Route("api/orders")]
    [Produces("application/json")]
    [Authorize(Roles = "Administrator")]
    [AutoValidateAntiforgeryToken]
    public class OrderController : ControllerBase
    {
        private readonly OrderQueryHandler _queryHandler;
        private readonly OrderCommandHandler _commandHandler;

        public OrderController(OrderQueryHandler queryHandler, OrderCommandHandler commandHandler)
        {
            _queryHandler = queryHandler ?? throw new ArgumentNullException(nameof(queryHandler));
            _commandHandler = commandHandler ?? throw new ArgumentNullException(nameof(commandHandler));
        }

        [HttpGet]
        public IEnumerable<Order> GetOrders()
        {
            return _queryHandler.Handle(new OrderQuery());
        }

        [HttpPost("{id}")]
        public void MarkShipped([FromQuery] OrderIdQuery query)
        {
            _queryHandler.Handle(query);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult CreateOrder([FromBody] CreateOrderCommand command)
        {
            if (ModelState.IsValid)
            {
                var payment = _commandHandler.Handle(command);
                if (payment != null)
                {
                    return Ok(payment);
                }
                return BadRequest("Платёж отклонён");
            }
            return BadRequest(ModelState);
        }
    }
}
