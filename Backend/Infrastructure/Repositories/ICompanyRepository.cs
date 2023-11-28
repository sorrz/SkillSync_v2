using Entity.Models;

namespace Infrastructure.Repositories
{
    public interface ICompanyRepository
    {
        public string GetOk();
        IEnumerable<CompanyModel>GetAllCompanies();
        Task<CompanyModel> GetCompanyById(int id);
        Task AddCompany(CompanyModel company);
        Task UpdateCompany(CompanyModel company);
        void DeleteCompany(int id);
    }
}