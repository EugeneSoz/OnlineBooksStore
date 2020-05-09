using System.Collections.Generic;
using OnlineBooksStore.Domain.Contracts.Models;

namespace OnlineBooksStore.App.Blazor.Server.Admin
{
    public partial class AdminNavComponent
    {
        private List<(string name, string link)> _navLinks;

        protected override void OnInitialized()
        {
            _navLinks = new List<(string name, string link)>
            {
                (name: "База данных", link: AppNavLink.Home),
                (name: "Книги", link: AppNavLink.Books),
                (name: "Категории", link: AppNavLink.Categories),
                (name: "Издательства", link: AppNavLink.Publishers),
                (name: "Заказы", link: AppNavLink.Orders)
            };
        }
    }
}