using Entity.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // TODO : chek up with the other AddCompany 
        public async Task AddCompany(CompanyModel company)
        {
            _context.Companies.Add(company);
            _context.SaveChanges();

            _logger.LogInformation($"Company added: {company.Id} - {company.CompanyName}");
        }

        public async Task DeleteCompany(int id)
        {
            try
            {
                var companyToDelete = _context.Companies.Find(id);

                if (companyToDelete != null)
                {
                    _context.Companies.Remove(companyToDelete);
                    _context.SaveChanges();
                }
                else
                {
                    _logger.LogWarning($"Company with ID {id} not found during delete.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting company with ID {id}: {ex.Message}");
                throw;
            }
        }

        // TODO : check with the other GetAllCompanies
        public IEnumerable<CompanyModel> GetAllCompanies()
        {
            return _context.Companies.ToList();
        }

        public async Task<CompanyModel> GetCompanyById(int id)
        {
            return await _context.Companies.FindAsync(id);
            //return _context.Companies.FirstOrDefault(c => c.Id == id);
        }

        public string GetOk()
        {
            return "OK";
        }

        public async Task UpdateCompany(CompanyModel company)
        {
            var existingCompany = await _context.Companies.FindAsync(company.Id);

            if (existingCompany != null)
            {
                existingCompany.CompanyName = company.CompanyName;
                _context.SaveChanges();
                _logger.LogInformation($"Company updated: {company.Id} - {company.CompanyName}");
            }
            else
            {
                _logger.LogError($"Company with ID {company.Id} not found.");
                throw new InvalidOperationException($"Company with ID {company.Id} not found.");
            }
        }

        Task<CompanyModel> ICompanyRepository.AddCompany(CompanyModel company)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<CompanyModel>> ICompanyRepository.GetAllCompanies()
        {
            throw new NotImplementedException();
        }
    }
}
