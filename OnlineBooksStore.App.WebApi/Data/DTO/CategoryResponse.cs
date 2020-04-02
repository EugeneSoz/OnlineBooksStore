namespace OnlineBooksStore.App.WebApi.Data.DTO
{
    public class CategoryResponse : CategoryDTO
    {
        public string ParentCategoryName { get; set; }
        public string DisplayedName { get; set; }
    }
}
