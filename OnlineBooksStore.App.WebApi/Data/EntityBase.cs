namespace OnlineBooksStore.App.WebApi.Data
{
    //базовый класс, от которого наследуются классы модели, имеющие id
    public class EntityBase
    {
        public long Id { get; set; }
    }
}
