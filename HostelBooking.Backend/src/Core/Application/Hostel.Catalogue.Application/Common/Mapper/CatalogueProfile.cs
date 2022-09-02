using AutoMapper;
using Hostel.Catalogue.Application.Commands.Companies.Create;
using Hostel.Catalogue.Application.Commands.Companies.Delete;
using Hostel.Catalogue.Application.Commands.Companies.Update;
using Hostel.Catalogue.Application.Commands.Rooms.Create;
using Hostel.Catalogue.Application.Commands.Rooms.Delete;
using Hostel.Catalogue.Application.Commands.Rooms.Update;
using Hostel.Catalogue.Application.Dto.Company;
using Hostel.Catalogue.Application.Dto.Room;
using Hostel.Catalogue.Domain.Entities;

namespace Hostel.Catalogue.Application.Common.Mapper
{
    public class CatalogueProfile : Profile
    {
        public CatalogueProfile()
        {
            CreateMap<Company, CreateCompany>().ReverseMap();
            CreateMap<Company, UpdateCompany>().ReverseMap();
            CreateMap<Company, DeleteCompany>().ReverseMap();
            CreateMap<CompanyReturnDto, Company>().ReverseMap();
            CreateMap<Room, CreateRoom>().ReverseMap();
            CreateMap<Room, UpdateRoom>().ReverseMap();
            CreateMap<Room, DeleteRoom>().ReverseMap();
            CreateMap<RoomReturnDto, Room>().ReverseMap();
        }
    }
}
