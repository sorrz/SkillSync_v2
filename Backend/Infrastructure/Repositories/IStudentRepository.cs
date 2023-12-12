using Entity.Models;

namespace Infrastructure.Repositories
{
    public interface IStudentRepository
    {
        Task<StudentModel> AddStudent(StudentModel student, string inputHash);
        Task<bool> DeleteStudentById(int id);
        Task<StudentModel?> GetStudentById(int id);
        Task<List<StudentModel?>> GetStudents();
        Task<StudentModel> UpdateStudent(StudentModel student);
    }
}
