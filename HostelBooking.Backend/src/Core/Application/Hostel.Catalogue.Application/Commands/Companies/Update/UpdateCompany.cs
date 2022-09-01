using AutoMapper;
using Hostel.Catalogue.Application.Common.Repositories;
using Hostel.Catalogue.Domain.Entities;
using Hostel.Shared.Types;

namespace Hostel.Catalogue.Application.Commands.Companies.Update
{
    public class UpdateCompany : ICommand
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public bool? IsDeleted { get; set; }
    }

    public class UpdateCompanyCommandHandler : ICommandHandler<UpdateCompany, int>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCompanyCommandHandler(ICompanyRepository companyRepository,
                                    IUnitOfWork unitOfWork,
                                    IMapper mapper)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> HandleAsync(UpdateCompany command, CancellationToken cancellationToken)
        {
            var company = _mapper.Map<Company>(command);

            await _companyRepository.UpdateCompany(company);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return company.CompanyId;
        }
    }
}
