namespace ChatServer.Infrastructure.Mapping.Interfaces
{
    using System.Collections.Generic;
    using Domain.ViewModels;
    using Repository.Models;

    public interface IModelMapper
    {
        List<T> MapTo<T>(IList<Message> model);

        T MapTo<T>(MessageViewModel model);

        T MapTo<T>(Message model);
    }
}
