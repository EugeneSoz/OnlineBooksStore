using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using OnlineBooksStore.Domain.Contracts.Models;
using OnlineBooksStore.Domain.Contracts.Models.Orders;

namespace OnlineBooksStore.App.Blazor.Server.Admin.OrdersSection
{
    [Route(AppNavLink.Orders)]
    public partial class OrdersComponent
    {
        private List<Order> _orders;
        private List<FilterSortingProps> _properties;

        protected override void OnInitialized()
        {
            _properties = new List<FilterSortingProps>()
            {
                new FilterSortingProps("Id", "Id"),
                new FilterSortingProps("Address", "Адрес"),
                new FilterSortingProps("LinesCount", "Количество"),
                new FilterSortingProps("CustomerName", "Имя клиента"),
                new FilterSortingProps("Total", "Всего"),
            };
            _orders = new List<Order>
            {
                new Order()
                {
                    Id = 1,
                    Address = "Adress",
                    Lines = new OrderLine[1],
                    CustomerName = "Customer",
                    Paynent = new Payment { Total = 200 }
                },
                new Order()
                {
                    Id = 1,
                    Address = "Adress",
                    Lines = new OrderLine[1],
                    CustomerName = "Customer",
                    Paynent = new Payment { Total = 200 }
                },
                new Order()
                {
                    Id = 1,
                    Address = "Adress",
                    Lines = new OrderLine[1],
                    CustomerName = "Customer",
                    Paynent = new Payment { Total = 200 }
                },
                new Order()
                {
                    Id = 1,
                    Address = "Adress",
                    Lines = new OrderLine[1],
                    CustomerName = "Customer",
                    Paynent = new Payment { Total = 200 }
                },
                new Order()
                {
                    Id = 1,
                    Address = "Adress",
                    Lines = new OrderLine[1],
                    CustomerName = "Customer",
                    Paynent = new Payment { Total = 200 }
                }
            };
        }
    }
}