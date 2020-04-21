using System.Collections.Generic;
using OnlineBooksStore.Domain.Contracts.Models;
using OnlineBooksStore.Domain.Contracts.Models.Books;

namespace OnlineBooksStore.App.WebApi.Data.DTO
{
    //класс содержит элементы списка в панели Toolbar
    public class ListItem
    {
        public ListItem(string propertyName, string name, 
            bool descendingOrder = false, bool hasDivider = false, string href = "#")
        {
            PropertyName = propertyName;
            Name = name;
            DescendingOrder = descendingOrder;
            HasDivider = hasDivider;
            Href = href;
        }
        public string PropertyName { get; set; }
        public string Name { get; set; }
        public bool DescendingOrder { get; set; }
        public bool HasDivider { get; set; }
        public string Href { get; set; }
    }

    public class Dropdown
    {
        public List<ListItem> SortingProperties => new List<ListItem>
        {
            new ListItem("", "Сортировать по", false),
            new ListItem(nameof(BookResponse.Title), "Названию: А - Я"),
            new ListItem(nameof(BookResponse.Title), "Названию: Я - А", true),
            new ListItem(nameof(BookResponse.RetailPrice), "Цене: мин. - макс."),
            new ListItem(nameof(BookResponse.RetailPrice), "Цене: макс. - мин.", true)
        };
        public List<ListItem> GridSizeProperties => new List<ListItem>
        {
            new ListItem("", "Отобразить", false),
            new ListItem("sixByTwo", "6 x 2 (строка x столбец)"),
            new ListItem("fourByThree", "4 x 3 (строка x столбец)"),
            new ListItem("threeByFour", "3 x 4 (строка x столбец)")
        };
    }
}
