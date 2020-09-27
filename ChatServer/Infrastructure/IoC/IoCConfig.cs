namespace ChatServer.Infrastructure.IoC
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public static class IoCConfig
    {
        public static IServiceCollection ConfigureIoC<T>(this IServiceCollection services, Func<T, IServiceCollection, IServiceCollection> configAction)
        {
            var config = Activator.CreateInstance<T>();
            configAction(config, services);
            return services;
        }
    }
}
