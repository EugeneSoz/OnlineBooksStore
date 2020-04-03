using AutoMapper;
using OnlineBooksStore.App.Contracts.Command;
using OnlineBooksStore.Domain.Contracts.Entities;
using OnlineBooksStore.Domain.Contracts.Models;

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