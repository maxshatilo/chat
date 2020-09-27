namespace ChatServer.AppServices.MessageService.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.ViewModels;

    public interface IMessageService
    {
        Task<IList<MessageViewModel>> GetAll();

        Task<MessageViewModel> Add(MessageViewModel message);

        Task<IList<AggregatedMessagesViewModel.MessagesByDate>> GetAllAggregatedMessages();
    }
}
