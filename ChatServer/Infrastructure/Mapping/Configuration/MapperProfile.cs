namespace ChatServer.Infrastructure.Mapping.Configuration
{
    using AutoMapper;
    using Domain.Entities;
    using Domain.ViewModels;
    using Repository.Models;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Message, MessageViewModel>();

            CreateMap<Message, MessageViewModel>().ReverseMap();

            CreateMap<ApplicationUser, User>();

            CreateMap<ApplicationUser, User>().ReverseMap();
        }
    }
}
