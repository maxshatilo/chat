namespace ChatServer.Infrastructure.IoC.Configurations
{
    using AppServices.MessageService;
    using AppServices.MessageService.Interfaces;
    using AppServices.UserService;
    using AppServices.UserService.Interfaces;
    using Domain.ViewModels;
    using Domain.ViewModels.AccountViewModels;
    using FluentValidation;
    using Helpers.Identity;
    using Helpers.Identity.Interfaces;
    using Mapping;
    using Mapping.Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using Repository.Interfaces;
    using Repository.Repositories;

    public class ChatConfiguration
    {
        public IServiceCollection Load(IServiceCollection services)
        {
            // Services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IJWTTokenProvider, JWTTokenProvider>();

            // Repositories
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();

            // Infrastructure
            services.AddScoped<IModelMapper, ModelMapper>();

            // Validators
            services.AddTransient<IValidator<LoginViewModel>, LoginViewModelValidator>();
            services.AddTransient<IValidator<RegisterViewModel>, RegisterViewModelValidator>();
            services.AddTransient<IValidator<MessageViewModel>, MessageViewModelValidator>();
           
            return services;
        }
    }
}
