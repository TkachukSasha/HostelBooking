using Hostel.Shared.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Hostel.Security.Application.Extensions
{
    public static class Extension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var applicationAssembly = typeof(ICommandHandler<>).Assembly;

            services.Scan(s => s.FromAssemblies(applicationAssembly)
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }
    }
}
