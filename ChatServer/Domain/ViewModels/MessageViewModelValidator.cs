namespace ChatServer.Domain.ViewModels
{
    using FluentValidation;

    public class MessageViewModelValidator : AbstractValidator<MessageViewModel>
    {
        public MessageViewModelValidator()
        {
            RuleFor(x => x.UserName).NotNull();
            RuleFor(x => x.DateTime).NotNull();
            RuleFor(x => x.MessageText).NotNull();
            RuleFor(x => x.MessageType).NotNull();
        }
    }
}
