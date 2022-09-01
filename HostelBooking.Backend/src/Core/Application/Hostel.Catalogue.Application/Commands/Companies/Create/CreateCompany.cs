using AutoMapper;
using Hostel.Catalogue.Application.Common.Repositories;
using Hostel.Catalogue.Domain.Entities;
using Hostel.Shared.Types;

namespace Hostel.Catalogue.Application.Commands.Companies.Create
{
    public class CreateCompany : ICommand
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public bool? IsDeleted { get; set; }
    }

    public class CreateCompanyCommandHandler : ICommandHandler<CreateCompany, int>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCompanyCommandHandler(ICompanyRepository companyRepository,
                                    IUnitOfWork unitOfWork,
                                    IMapper mapper)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> HandleAsync(CreateCompany command, CancellationToken cancellationToken)
        {
            var company = _mapper.Map<Company>(command);

            await _companyRepository.AddCompany(company);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return company.CompanyId;
        }
    }
}
