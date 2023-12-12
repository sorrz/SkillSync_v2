using Entity.Models;
using Infrastructure.Context;
using Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories
{
    public class SecureRepository : ISecureRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<SecureRepository> _logger;
        private readonly DbSet<CompanyModel> _companySet;
        private readonly DbSet<StudentModel> _studentSet;
        private readonly SecurityHandler _secure;

        public SecureRepository(AppDbContext appDbContext, ILogger<SecureRepository> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
            _companySet = _appDbContext.Set<CompanyModel>();
            _studentSet = _appDbContext.Set<StudentModel>();
            _secure = new SecurityHandler();
        }

        public async Task<bool> CreateStudentHashAsync(StudentModel student, string inputHash)
        {
            var hasBeenSalted = await SetStudentSalt(student);
            if (!hasBeenSalted) return false;

            var hasBeenHashed = await SetStudentHash(student, inputHash);
            if (!hasBeenHashed) return false;

            return true;
        }
        public async Task<bool> VerifyPasswordAsync(int userId, string inputHash)
        {
            if(inputHash  == null) return false;
            var salt = await GetStudentSalt(userId);
            var storedHash = await GetStudentHash(userId);
            var result = _secure.Verify(inputHash, storedHash, salt);
           
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<bool> IsUserAuthenticated(int id)
        {
            var student = await _appDbContext.Students.FindAsync(id);
            var company = await _appDbContext.Companies.FindAsync(id);

            if (student != null || company != null) return true;
            return false;

        }

        private async Task<bool> SetStudentHash(StudentModel student, string inputHash)
        {
            var hashedStudent = await _secure.SetStudentHash(student, inputHash);
            if (hashedStudent.PasswordHash == null) return false;

            _appDbContext.Entry(student).CurrentValues.SetValues(hashedStudent);
            await _appDbContext.SaveChangesAsync();
            return true;
        }


        private async Task<string> GetStudentHash(int userId)
        {
            var student = await _appDbContext.Students.FindAsync(userId);
            if (student == null) return null;

            return student.PasswordHash;
        }

        private async Task<string> GetStudentSalt(int userId)
        {
            var student = await _appDbContext.Students.FindAsync(userId);
            return student.StudentSalt;
        }

        private async Task<bool> SetStudentSalt(StudentModel student)
        {
            //var currentStudentModel = await _appDbContext.Students.FindAsync(student.Id);
            //if (currentStudentModel == null) return false;
            
            var saltedStudent = await _secure.SetStudentSalt(student);
            _appDbContext.Entry(student).CurrentValues.SetValues(saltedStudent);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<StudentModel?> LoginStudent(string email, string passwordHash)
        {
            var student = _appDbContext.Students
                    .Where(s => s.MailAddress == email.ToString())
                    .FirstOrDefault();
            if (student == null) return null;

            var storedHash = student.PasswordHash;
            var salt = student.StudentSalt;
            var result = _secure.Verify(passwordHash, storedHash, salt);

            if (result) return student;
            else return null;
        }
    }
}
