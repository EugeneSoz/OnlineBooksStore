using System.ComponentModel.DataAnnotations;

namespace OnlineBooksStore.App.WebApi.Data.DTO
{
    //класс, описывающий категорию книги
    public class CategoryDTO : EntityBase
    {
        //название категории основной категории книги
        [Required(ErrorMessage = "Укажите название категории/подкатегории")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Название должно быть не меньше 2 и не больше 100 символов")]
        public string Name { get; set; } = string.Empty;

        //если свойство не равно null, тогда категория является подкатегорией
        public long? ParentCategoryID { get; set; }
    }
}
