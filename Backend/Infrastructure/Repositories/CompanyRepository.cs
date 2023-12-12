using Entity.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<CompanyModel> _companies;
        private readonly ILogger<CompanyRepository> _logger;
        public CompanyRepository(AppDbContext context, ILogger<CompanyRepository> logger)
        {
            _context = context;
            _companies = context.Set<CompanyModel>();
            _logger = logger;
        }

        public async Task<CompanyModel> AddCompany(CompanyModel company)
        {
            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Company added: {company.Id} - {company.CompanyName}");
            return company;
        }

        public async Task<bool> DeleteCompanyById(int id)
        {
            try
            {
                var companyToDelete = await _context.Companies.FindAsync(id);
                if (companyToDelete == null) return false;

                _context.Companies.Remove(companyToDelete);
                _context.SaveChanges();
                return true;        
               
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting company with ID {id}: {ex.Message}");
                throw;
            }
        }

        public async Task<List<CompanyModel?>> GetAllCompanies() => await _context.Companies.ToListAsync();

        public async Task<CompanyModel?> GetCompanyById(int companyId) => await _context.Companies.FindAsync(companyId);

        public async Task<CompanyModel> UpdateCompany(CompanyModel company)
        {
            try
            {
                var existingCompany = await _context.Companies.FindAsync(company.Id);

                if (existingCompany == null)
                {
                    _logger.LogError($"Company with ID {company.Id} not found.");
                    throw new InvalidOperationException($"Company with ID {company.Id} not found.");
                }

                _context.Entry(existingCompany).CurrentValues.SetValues(company);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while updating company with ID {company.Id}: {ex.Message}");
                throw;
            }

            return await GetCompanyById(company.Id);
        }


    }
}
