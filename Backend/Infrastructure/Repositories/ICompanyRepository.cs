using Entity.Models;

namespace Infrastructure.Repositories
{
    public interface ICompanyRepository
    {
        public string GetOk();
        IEnumerable<CompanyModel>GetAllCompanies();
        CompanyModel GetCompanyById(int id);
        void AddCompany(CompanyModel company);
        void UpdateCompany(CompanyModel company);
        void DeleteCompany(int id);
    }
}