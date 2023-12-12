using Entity.Models;

namespace Infrastructure.Repositories
{
    public interface ISecureRepository
    {
        Task<bool> VerifyPasswordAsync(int id, string inputHash);
        Task<bool> CreateStudentHashAsync(StudentModel student, string inputHash);
        Task<StudentModel> LoginStudent(string email, string passwordHash);
        Task<bool> IsUserAuthenticated(int id);
    }
}
