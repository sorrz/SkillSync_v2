using Entity.Models;

namespace Infrastructure.Repositories
{
    public interface ICompanyRepository
    {
        public string GetOk();
        Task<IEnumerable<CompanyModel>> GetAllCompanies();
        Task<CompanyModel> GetCompanyById(int id);
        Task<CompanyModel> AddCompany(CompanyModel company);
        Task UpdateCompany(CompanyModel company);
        Task DeleteCompany(int id);
    }
}