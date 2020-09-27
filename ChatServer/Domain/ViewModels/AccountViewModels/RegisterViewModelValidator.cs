namespace ChatServer.Domain.ViewModels.AccountViewModels
{
    using FluentValidation;

    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(x => x.UserName).NotNull();
            RuleFor(x => x.Password).NotNull();
            RuleFor(x => x.ConfirmPassword).NotNull().Equal(x => x.Password);
        }
    }
}
