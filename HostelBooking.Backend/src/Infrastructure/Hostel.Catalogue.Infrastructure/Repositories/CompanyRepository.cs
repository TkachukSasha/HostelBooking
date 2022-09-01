using Hostel.Catalogue.Domain.Entities;
using Hostel.Catalogue.Domain.Repositories;
using Hostel.Catalogue.Infrastructure.Dal;
using Microsoft.EntityFrameworkCore;

namespace Hostel.Catalogue.Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository, IDisposable
    {
        private CatalogueContext _context;
        private bool _disposed;

        public CompanyRepository(CatalogueContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Company>> GetAllCompanies()
        {
            try
            {
                return await _context.Company.ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Company> GetCompanyById(int id)
        {
            try
            {
                return await _context.Company.FirstOrDefaultAsync(x => x.CompanyId == id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> AddCompany(Company company)
        {
            try
            {
                await _context.Company.AddAsync(company);
                await _context.SaveChangesAsync();
                return company.CompanyId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> UpdateCompany(Company company)
        {
            try
            {
                 _context.Company.Update(company);
                await _context.SaveChangesAsync();
                return company.CompanyId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> DeleteCompany(int companyId)
        {
            try
            {
                var existCompany = await _context.Company.FirstOrDefaultAsync(x => x.CompanyId == companyId);

                _context.Company.Remove(existCompany);
                await _context.SaveChangesAsync();
                return existCompany.CompanyId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            _ = disposing;

            if (!_disposed)
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }

            _disposed = true;
        }
    }
}
