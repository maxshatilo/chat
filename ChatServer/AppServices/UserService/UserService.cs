namespace ChatServer.AppServices.UserService
{
    using System;
    using System.Threading.Tasks;
    using Domain.ViewModels.AccountViewModels;
    using Infrastructure.Mapping.Interfaces;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Repository.Interfaces;
    using Repository.Models;

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly IModelMapper _modelMapper;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserService(IUserRepository userRepository, IModelMapper modelMapper, IServiceProvider serviceProvider)
        {
            _userRepository = userRepository;
            _modelMapper = modelMapper;
            _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            _signInManager = serviceProvider.GetRequiredService<SignInManager<ApplicationUser>>();
        }

        public async Task<UserViewModel> GetByUserName(string userName)
        {
            var identityUser = await _userManager.FindByNameAsync(userName);
            if (identityUser != null)
            {
                return new UserViewModel() { Id = identityUser.Id, UserName = identityUser.UserName };
            }

            return null;
        }

        public async Task<IdentityResult> CreateAsync(string userName, string password)
        {
            var user = new ApplicationUser { UserName = userName };
            var result = await _userManager.CreateAsync(user, password);

            return result;
        }

        public async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool rememberMe, bool lockoutOnFailure)
        {
            var result = await _signInManager.PasswordSignInAsync(userName, password, rememberMe, lockoutOnFailure);

            return result;
        }

        public async Task SignInAsync(string userName, bool isPersistent)
        {
            var identityUser = await _userManager.FindByNameAsync(userName);
            await _signInManager.SignInAsync(identityUser, isPersistent: isPersistent);
        }
    }
}
