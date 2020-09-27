namespace ChatServer.Domain.ViewModels.AccountViewModels
{
    using FluentValidation;

    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(x => x.UserName).NotNull();
            RuleFor(x => x.Password).NotNull();
        }
    }
}
