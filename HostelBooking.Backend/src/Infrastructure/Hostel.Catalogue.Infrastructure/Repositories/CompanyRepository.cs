using Hostel.Catalogue.Application.Common.Repositories;
using Hostel.Catalogue.Domain.Entities;
using Hostel.Catalogue.Infrastructure.Dal;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Hostel.Catalogue.Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository, IDisposable
    {
        private CatalogueContext _context;
        private readonly ILogger _logger;
        private bool _disposed;

        public CompanyRepository(CatalogueContext context,
                                 ILogger logger)
        {
            (_context, _logger) = (context, logger);
        }

        public async Task<IEnumerable<Company>> GetAllCompanies()
        {
            try
            {
                return await _context.Company.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Exception: {ex.Message} when try to invoke get all companies method");
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
                _logger.Error(ex, $"Exception: {ex.Message} when try to invoke get company by id method");
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
                _logger.Error(ex, $"Exception: {ex.Message} when try to invoke add company method");
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
                _logger.Error(ex, $"Exception: {ex.Message} when try to invoke update company method");
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
                _logger.Error(ex, $"Exception: {ex.Message} when try to invoke delete company method");
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
