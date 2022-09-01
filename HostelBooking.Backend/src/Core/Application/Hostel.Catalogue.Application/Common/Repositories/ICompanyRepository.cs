using Hostel.Catalogue.Domain.Entities;

namespace Hostel.Catalogue.Application.Common.Repositories
{
    public interface ICompanyRepository
    {
        Task<Company> GetCompanyById(int id);
        Task<IEnumerable<Company>> GetAllCompanies();
        Task<int> AddCompany(Company company);
        Task<int> UpdateCompany(Company company);
        Task<int> DeleteCompany(int companyId);
    }
}
