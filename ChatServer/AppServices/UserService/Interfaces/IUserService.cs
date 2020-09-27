namespace ChatServer.AppServices.UserService.Interfaces
{
    using System.Threading.Tasks;
    using Domain.ViewModels.AccountViewModels;
    using Microsoft.AspNetCore.Identity;

    public interface IUserService
    {
        Task<UserViewModel> GetByUserName(string userName);

        Task<IdentityResult> CreateAsync(string userName, string password);

        Task<SignInResult> PasswordSignInAsync(string userName, string password, bool rememberMe, bool lockoutOnFailure);

        Task SignInAsync(string userName, bool isPersistent);
    }
}
