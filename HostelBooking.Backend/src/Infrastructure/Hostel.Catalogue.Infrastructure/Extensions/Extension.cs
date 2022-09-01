using Hostel.Catalogue.Application.Common.Repositories;
using Hostel.Catalogue.Infrastructure.Repositories;
using Hostel.Shared.Exceptions.Middleware;
using Hostel.Shared.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Hostel.Catalogue.Infrastructure.Extensions
{
    public static class Extension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<GlobalExceptionMiddleware>();

            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
