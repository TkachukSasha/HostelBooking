using Hostel.Security.Application.Commands.Login;
using Hostel.Security.Application.Commands.Refresh;
using Hostel.Security.Application.Commands.Register;
using Hostel.Security.Application.Common.Security.TokenGenerators;
using Hostel.Security.Application.Common.Security.TokenValidators;
using Hostel.Shared.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Hostel.Security.Application.Extensions
{
    public static class Extension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<JwtTokenGenerator>();
            services.AddSingleton<AccessTokenGenerator>();
            services.AddSingleton<RefreshTokenGenerator>();
            services.AddSingleton<RefreshTokenValidator>();

            services.AddScoped<ICommand, SignIn>();
            services.AddScoped<ICommand, SignUp>();
            services.AddScoped<ICommand, Refresh>();
            services.AddScoped<ICommandHandler<SignIn>, SignInHandler>();
            services.AddScoped<ICommandHandler<SignUp>, SignUpHandler>();
            services.AddScoped<ICommandHandler<Refresh>, RefreshHandler>();

            // var applicationAssembly = typeof(ICommandHandler<>).Assembly;

            //services.Scan(s => s.FromAssemblies(applicationAssembly)
            //    .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            //    .AsImplementedInterfaces()
            //    .WithScopedLifetime());

            return services;
        }
    }
}
