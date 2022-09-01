using AutoMapper;
using Hostel.Catalogue.Application.Common.Repositories;
using Hostel.Catalogue.Domain.Entities;
using Hostel.Shared.Types;

namespace Hostel.Catalogue.Application.Queries.Companies.GetListCompanies
{
    public class GetCompanies : IQuery<IEnumerable<Company>>
    {
    }

    public class GetCompamiesQueryHandler : IQueryHandler<GetCompanies, IEnumerable<Company>>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public GetCompamiesQueryHandler(ICompanyRepository companyRepository,
                                        IMapper mapper)
        {
            _companyRepository = companyRepository; 
            _mapper = mapper;
        }

        public async Task<IEnumerable<Company>> HandleAsync(GetCompanies query)
        {
            return await _companyRepository.GetAllCompanies();
        }
    }
}
