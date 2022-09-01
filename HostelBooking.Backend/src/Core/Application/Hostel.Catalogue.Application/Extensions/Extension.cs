using Hostel.Catalogue.Application.Commands.Companies.Create;
using Hostel.Catalogue.Application.Commands.Companies.Delete;
using Hostel.Catalogue.Application.Commands.Companies.Update;
using Hostel.Catalogue.Application.Commands.Rooms.Create;
using Hostel.Catalogue.Application.Commands.Rooms.Delete;
using Hostel.Catalogue.Application.Commands.Rooms.Update;
using Hostel.Catalogue.Application.Common.Mapper;
using Hostel.Catalogue.Application.Queries.Companies.GetCompanyById;
using Hostel.Catalogue.Application.Queries.Companies.GetListCompanies;
using Hostel.Catalogue.Application.Queries.Rooms.GetListRooms;
using Hostel.Catalogue.Application.Queries.Rooms.GetRoomById;
using Hostel.Catalogue.Domain.Entities;
using Hostel.Shared.Types;
using Microsoft.Extensions.DependencyInjection;
namespace Hostel.Catalogue.Application.Extensions
{
    public static class Extension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CatalogueProfile));

            services.AddScoped<ICommand, CreateCompany>();
            services.AddScoped<ICommand, UpdateCompany>();
            services.AddScoped<ICommand, DeleteCompany>();
            services.AddScoped<ICommandHandler<CreateCompany, int>, CreateCompanyCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateCompany, int>, UpdateCompanyCommandHandler>();
            services.AddScoped<ICommandHandler<DeleteCompany, int>, DeleteCompanyCommandHandler>();

            services.AddScoped<IQuery<IEnumerable<Company>>, GetCompanies>();
            services.AddScoped<IQueryHandler<GetCompanies, IEnumerable<Company>>, GetCompamiesQueryHandler>();

            services.AddScoped<IQuery<Company>, GetCompany>();
            services.AddScoped<IQueryHandler<GetCompany, Company>, GetCompanyQueryHandler>();

            services.AddScoped<ICommand, CreateRoom>();
            services.AddScoped<ICommand, UpdateRoom>();
            services.AddScoped<ICommand, DeleteRoom>();
            services.AddScoped<ICommandHandler<CreateRoom, int>, CreateRoomCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateRoom, int>, UpdateRoomCommandHandler>();
            services.AddScoped<ICommandHandler<DeleteRoom, int>, DeleteRoomCommandHandler>();

            services.AddScoped<IQuery<Room>, GetRoom>();
            services.AddScoped<IQueryHandler<GetRoom, Room>, GetRoomQueryHandler>();

            services.AddScoped<IQuery<IEnumerable<Room>>, GetRooms>();
            services.AddScoped<IQueryHandler<GetRooms, IEnumerable<Room>>, GetRoomsQueryHandler>();

            return services;
        }
    }
}
