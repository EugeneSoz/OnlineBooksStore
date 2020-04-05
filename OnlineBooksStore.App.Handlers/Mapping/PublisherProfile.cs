using AutoMapper;
using OnlineBooksStore.App.Contracts.Command;
using OnlineBooksStore.Domain.Contracts.Entities;
using OnlineBooksStore.Domain.Contracts.Models;
using OnlineBooksStore.Domain.Contracts.Models.Publishers;

namespace OnlineBooksStore.App.Handlers.Mapping
{
    public class PublisherProfile : Profile
    {
        public PublisherProfile()
        {
            CreateMap<PublisherCommand, Publisher>();
        }
    }
}