using AutoMapper;
using Hostel.Catalogue.Application.Common.Repositories;
using Hostel.Catalogue.Application.Dto.Company;
using Hostel.Catalogue.Domain.Entities;
using Hostel.Shared.Types;

namespace Hostel.Catalogue.Application.Commands.Companies.Delete
{
    public class DeleteCompany : ICommand<CompanyReturnDto>
    {
    }

    public class DeleteCompanyCommandHandler : ICommandHandler<DeleteCompany, CompanyReturnDto>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteCompanyCommandHandler(ICompanyRepository companyRepository,
                                    IUnitOfWork unitOfWork,
                                    IMapper mapper)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<CompanyReturnDto> HandleAsync(DeleteCompany command, CancellationToken cancellationToken)
        {
            var company = _mapper.Map<Company>(command);

            await _companyRepository.DeleteCompany(company.CompanyId);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var returnCompany = _mapper.Map<CompanyReturnDto>(company);

            return returnCompany;
        }
    }
}
