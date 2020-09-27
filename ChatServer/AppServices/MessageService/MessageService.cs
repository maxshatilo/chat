namespace ChatServer.AppServices.MessageService
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.ViewModels;
    using Infrastructure.Mapping.Interfaces;
    using Interfaces;
    using Repository.Interfaces;
    using Repository.Models;

    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IModelMapper _modelMapper;

        public MessageService(IMessageRepository messageRepository, IModelMapper modelMapper)
        {
            _messageRepository = messageRepository;
            _modelMapper = modelMapper;
        }

        public async Task<IList<MessageViewModel>> GetAll()
        {
            var messages = await _messageRepository.GetAll();
            return _modelMapper.MapTo<MessageViewModel>(messages);
        }

        public async Task<MessageViewModel> Add(MessageViewModel message)
        {
            var messageItem = _modelMapper.MapTo<Message>(message);
            var addedMessage = await _messageRepository.Add(messageItem);
            return _modelMapper.MapTo<MessageViewModel>(addedMessage);
        }

        public async Task<IList<AggregatedMessagesViewModel.MessagesByDate>> GetAllAggregatedMessages()
        {
            var messages = await _messageRepository.GetAll();
            var results = messages.GroupBy(g => g.DateTime.Date)
            .Select(s => new AggregatedMessagesViewModel.MessagesByDate()
            {
                Date = s.Key.Date,
                MessagesByHour = s.GroupBy(g => g.DateTime.Hour)
                    .Select(s => new AggregatedMessagesViewModel.MessagesByHour()
                    {
                        Hour = s.Key,
                        Messages = s.Select(p => new Message()
                        {
                            Id = p.Id,
                            DateTime = p.DateTime,
                            UserName = p.UserName,
                            MessageText = p.MessageText,
                            MessageType = p.MessageType
                        }).GroupBy(g => g.MessageType)
                            .Select(s => Tuple.Create(s.Key, s.Count()))
                            .Select(s => new AggregatedMessagesViewModel.HourStatistics()
                            {
                                Name = s.Item1,
                                Count = s.Item2
                            }).ToList()
                    }).ToList()
            }).ToList();

            return results;
        }
    }
}
