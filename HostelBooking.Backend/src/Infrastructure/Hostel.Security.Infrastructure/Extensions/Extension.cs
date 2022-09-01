using Hostel.Security.Application.Common.Repositories;
using Hostel.Security.Application.Common.Security.Options;
using Hostel.Security.Domain.Repositories;
using Hostel.Security.Infrastructure.Repositories;
using Hostel.Shared.Exceptions.Middleware;
using Hostel.Shared.Types;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Hostel.Security.Infrastructure.Extensions
{
    public static class Extension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<GlobalExceptionMiddleware>();

            JwtOptions jwtOptions = new JwtOptions();
            configuration.Bind("JwtOptions", jwtOptions);

            services.AddSingleton(jwtOptions);

            services.AddScoped<IClock, Clock>();
            services.AddScoped<IPasswordManager, PasswordManager>();
            services.AddScoped<ITokenStorage, HttpContextTokenStorage>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.AccessTokenSecret)),
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAuthorization();

            return services;
        }
    }
}
