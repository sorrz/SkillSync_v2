using Entity.Models;

namespace Infrastructure.Repositories
{
    public interface ICompanyRepository
    {
        Task<List<CompanyModel?>> GetAllCompanies();
        Task<CompanyModel?> GetCompanyById(int id);
        Task<CompanyModel> AddCompany(CompanyModel company);
        Task<CompanyModel> UpdateCompany(CompanyModel company);
        Task<bool> DeleteCompanyById(int id);
    }
}