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

        public void AddCompany(CompanyModel company)
        {
            _context.Companies.Add(company);
            _context.SaveChanges();
            _logger.LogInformation($"Company added: {company.Id} - {company.CompanyName}");
        }

        public void DeleteCompany(int id)
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

        public IEnumerable<CompanyModel> GetAllCompanies()
        {
            return _context.Companies.ToList();
        }

        public CompanyModel GetCompanyById(int id)
        {
            return _context.Companies.SingleOrDefault(c => c.Id == id);
        }

        public string GetOk()
        {
            return "OK";
        }

        public void UpdateCompany(CompanyModel company)
        {
            var existingCompany = _context.Companies.Find(company.Id);

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

        public void RemoveCompany(int companyIdToRemove)
        {
           
            var companyToRemove = _context.Companies.Find(companyIdToRemove);

            if (companyToRemove != null)
            {
             
                _context.Companies.Remove(companyToRemove);
                _context.SaveChanges();
                _logger.LogInformation($"Company removed: {companyIdToRemove}");
            }
            else
            {
                _logger.LogError($"Company with ID {companyIdToRemove} not found.");
                throw new InvalidOperationException($"Company with ID {companyIdToRemove} not found.");
            }
        }
    }
}
