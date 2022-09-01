using AutoMapper;
using Hostel.Catalogue.Application.Common.Repositories;
using Hostel.Catalogue.Domain.Entities;
using Hostel.Shared.Types;

namespace Hostel.Catalogue.Application.Queries.Companies.GetCompanyById
{
    public class GetCompany : IQuery<Company>
    {
        public int CompanyId { get; set; }
    }

    public class GetCompanyQueryHandler : IQueryHandler<GetCompany, Company>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public GetCompanyQueryHandler(ICompanyRepository companyRepository,
                                        IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<Company> HandleAsync(GetCompany query)
        {
            var existingCompany = await _companyRepository.GetCompanyById(query.CompanyId);

            return existingCompany;
        }
    }
}
