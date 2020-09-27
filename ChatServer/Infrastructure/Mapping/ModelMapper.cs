namespace ChatServer.Infrastructure.Mapping
{
    using System.Collections.Generic;
    using AutoMapper;
    using Domain.ViewModels;
    using Interfaces;
    using Repository.Models;

    public class ModelMapper : IModelMapper
    {
        private readonly IMapper _mapper;

        public ModelMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public List<T> MapTo<T>(IList<Message> model)
        {
            return _mapper.Map<List<T>>(model);
        }

        public T MapTo<T>(MessageViewModel model)
        {
            return MapObjectTo<T>(model);
        }

        public T MapTo<T>(Message model)
        {
            return _mapper.Map<T>(model);
        }

        public T MapObjectTo<T>(object model)
        {
            return _mapper.Map<T>(model);
        }
    }
}
