using System.Collections.Generic;

namespace OnlineBooksStore.App.WebApi.Data.DTO
{
    public class StoreCategoryResponse : CategoryDTO
    {
        //id элемента html разметки
        public string ControlId { get; set; }
        //родительская категория
        public bool IsParent { get; set; }
        //имеются ли дочернии категории
        public bool HasChildren { get; set; }
        //скрыты ли дочернии категории
        public bool IsCollapsed { get; set; } = true;
        public List<StoreCategoryResponse> Children { get; set; }
    }
}
