namespace ChatServer.Controllers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using AppServices.MessageService.Interfaces;
    using Domain.ViewModels;
    using Infrastructure.Hubs;
    using Infrastructure.Mapping.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]

    public class MessagesController : ControllerBase
    {
        protected readonly IHubContext<MessageHub> _messageHub;

        private readonly IConfiguration _config;

        private readonly IMessageService _messageService;

        private readonly ILogger<MessagesController> _logger;

        private readonly IModelMapper _modelMapper;

        public MessagesController([NotNull] IHubContext<MessageHub> messageHub,
            IConfiguration config,
            IMessageService messageService,
            ILogger<MessagesController> logger,
            IModelMapper modelMapper
            )
        {
            _messageHub = messageHub;
            _config = config;
            _messageService = messageService;
            _logger = logger;
            _modelMapper = modelMapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var messages = await _messageService.GetAll();
            return Ok(messages);
        }

        [HttpGet]
        [Route("aggregated")]
        public async Task<IActionResult> GetAllAggregatedMessages()
        {
            var messages = await _messageService.GetAllAggregatedMessages();
            return Ok(messages);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MessageViewModel message)
        {
            if (ModelState.IsValid)
            {
                message.DateTime = DateTime.Now;
                await _messageHub.Clients.All.SendAsync("sendToAll", message);
                var addedMessage = await _messageService.Add(message);
                return Ok(addedMessage);
            }

            _logger.LogInformation("There was an error during creating new message.");

            return BadRequest();
        }
    }
}
