using AutoMapper;
using OnlineBooksStore.App.Contracts.Query;
using OnlineBooksStore.Domain.Contracts.Models.Pages;

namespace OnlineBooksStore.App.Handlers.Mapping
{
    public class PageOptionsProfile : Profile
    {
        public PageOptionsProfile()
        {
            CreateMap<PageFilterQuery, QueryOptions>();
        }
    }
}