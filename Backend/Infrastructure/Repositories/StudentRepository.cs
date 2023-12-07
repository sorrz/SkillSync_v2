using Entity.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<StudentModel> _students;
        private readonly ILogger<StudentRepository> _logger;


        public StudentRepository(AppDbContext context, ILogger<StudentRepository> logger)
        {
            _context = context;
            _logger = logger;
            _students = context.Set<StudentModel>();
        }

        public async Task<StudentModel> AddStudent(StudentModel student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Student whit id {student.Id} was created");
            return student;
        }

        public async Task<bool> DeleteStudentById(int id)
        {
            try
            {
                var student = await _context.Students.FindAsync(id);
                if (student == null) return false;

                _context.Students.Remove(student);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting company with ID {id}: {ex.Message}");
                throw;
            }
        }

        public async Task<StudentModel?> GetStudentById(int id) => await _context.Students.FindAsync(id);

        public async Task<List<StudentModel?>> GetStudents() => await _context.Students.ToListAsync();

        public async Task<StudentModel?> UpdateStudent(StudentModel student)
        {
            try
            {
                var existingStudent = await _context.Students.FindAsync(student.Id);
                if (existingStudent == null)
                {
                    _logger.LogError($"Student with id {student.Id} was not found.");
                    throw new InvalidOperationException($"Student with id {student.Id} was not found");
                }
                _context.Entry(existingStudent).CurrentValues.SetValues(student);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) 
        {
                _logger.LogError(ex, $"Error while trying to update student whit id {student.Id}", student.Id);
                throw;
            }
            return await GetStudentById(student.Id);
        }
    }
}
