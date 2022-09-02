using Hostel.Catalogue.Application.Commands.Companies.Create;
using Hostel.Catalogue.Application.Commands.Companies.Delete;
using Hostel.Catalogue.Application.Commands.Companies.Update;
using Hostel.Catalogue.Application.Commands.Rooms.Create;
using Hostel.Catalogue.Application.Commands.Rooms.Delete;
using Hostel.Catalogue.Application.Commands.Rooms.Update;
using Hostel.Catalogue.Application.Common.Cache;
using Hostel.Catalogue.Application.Common.Mapper;
using Hostel.Catalogue.Application.Dto.Company;
using Hostel.Catalogue.Application.Dto.Room;
using Hostel.Catalogue.Application.Queries.Companies.GetCompanyById;
using Hostel.Catalogue.Application.Queries.Companies.GetListCompanies;
using Hostel.Catalogue.Application.Queries.Rooms.GetListRooms;
using Hostel.Catalogue.Application.Queries.Rooms.GetRoomById;
using Hostel.Catalogue.Domain.Entities;
using Hostel.Shared.Types;
using Hostel.Shared.Types.Cache;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Hostel.Catalogue.Application.Extensions
{
    public static class Extension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CatalogueProfile));

            services.AddScoped<IQuery<IEnumerable<Company>>, GetCompanies>();
            services.AddScoped<IQueryHandler<GetCompanies, IEnumerable<Company>>, GetCompaniesQueryHandler>();

            services.AddScoped<IQuery<Company>, GetCompany>();
            services.AddScoped<IQueryHandler<GetCompany, Company>, GetCompanyQueryHandler>();

            services.AddScoped<ICommand<CompanyReturnDto>, CreateCompany>();
            services.AddScoped<ICommand<CompanyReturnDto>, UpdateCompany>();
            services.AddScoped<ICommand<CompanyReturnDto>, DeleteCompany>();
            services.AddScoped<ICommandHandler<CreateCompany, CompanyReturnDto>, CreateCompanyCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateCompany, CompanyReturnDto>, UpdateCompanyCommandHandler>();
            services.AddScoped<ICommandHandler<DeleteCompany, CompanyReturnDto>, DeleteCompanyCommandHandler>();

            services.AddScoped<IQuery<Room>, GetRoom>();
            services.AddScoped<IQueryHandler<GetRoom, Room>, GetRoomQueryHandler>();

            services.AddScoped<IQuery<IEnumerable<Room>>, GetRooms>();
            services.AddScoped<IQueryHandler<GetRooms, IEnumerable<Room>>, GetRoomsQueryHandler>();

            services.AddScoped<ICommand<RoomReturnDto>, CreateRoom>();
            services.AddScoped<ICommand<RoomReturnDto>, UpdateRoom>();
            services.AddScoped<ICommand<RoomReturnDto>, DeleteRoom>();
            services.AddScoped<ICommandHandler<CreateRoom, RoomReturnDto>, CreateRoomCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateRoom, RoomReturnDto>, UpdateRoomCommandHandler>();
            services.AddScoped<ICommandHandler<DeleteRoom, RoomReturnDto>, DeleteRoomCommandHandler>();

            return services;
        }

        public static IServiceCollection AddRedis(this IServiceCollection services,
              IConfiguration configuration)
        {
            var redisCacheSettings = new RedisCacheSettings();
            configuration.GetSection(nameof(RedisCacheSettings)).Bind(redisCacheSettings);
            services.AddSingleton(redisCacheSettings);

            //if (!redisCacheSettings.Enabled)
            //{
            //    return;
            //}

            services.AddStackExchangeRedisCache(options => options.Configuration = redisCacheSettings.ConnectionString);
            services.AddSingleton<ICacheRepository, CacheRepository>();

            return services;
        }
    }
}
