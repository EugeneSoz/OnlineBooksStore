using System.ComponentModel.DataAnnotations;

namespace OnlineBooksStore.App.WebApi.Data.DTO
{
    //класс определяющий издателя книг
    public class PublisherDTO : EntityBase
    {
        //название издательства
        [Required(ErrorMessage = "Укажите название издательства")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Название должно быть не меньше 2 и не больше 100 символов")]
        public string Name { get; set; } = string.Empty;
        //страна происхождения
        [Required(ErrorMessage = "Укажите название страны нахождения издательства")]
        public string Country { get; set; }
    }
}
