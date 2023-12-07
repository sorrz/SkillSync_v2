using Entity.Models;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace Infrastructure.Tests.RepositoryTests
{
    public class SecureRepositoryTests
    {
        private readonly Mock<ILogger<SecureRepository>> _logger;
        private readonly SecureRepository _sut;
        private readonly StudentRepository _studentRepository;

        private readonly AppDbContext dbContext;

        public SecureRepositoryTests()
        {
            _logger = new Mock<ILogger<SecureRepository>>();
            var studentLogger = new Mock<ILogger<StudentRepository>>();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            dbContext = new AppDbContext(options);
            _sut = new SecureRepository(dbContext, _logger.Object);
            _studentRepository = new StudentRepository(dbContext, studentLogger.Object);


        }



        [Fact]
        public async void Should_CreateSafeStudentPwdWithSalt_WhenCalled()
        {

            var frontendHash = "4234a9cea21fa911110cf36e96cd887049543ca31e7c95e04028290bde1db1e0";
            var userId = 11;

            var testStudent = BuildTestStudent(userId);
            var savedStudent = await AddStudentToDbAsync(testStudent);

            var result = await _sut.CreateStudentHashAsync(savedStudent, frontendHash);

            Assert.True(result);
            
        }

        [Fact]
        public async Task VerifyPasswordAsync_ValidCredentials_ReturnsTrue()
        {
            // Arrange
            var userId = 12;
            var inputHash = "4234a9cea21fa911110cf36e96cd887049543ca31e7c95e04028290bde1db1e0";


            var testStudent = BuildTestStudent(userId);
            var saveStudent = await SaltHashAndAddToDbAsync(testStudent, inputHash);
            

            // Act
            var result = await _sut.VerifyPasswordAsync(userId, inputHash);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task VerifyPasswordAsync_NullInputHash_ReturnsFalse()
        {
            // Arrange
            var userId = 12;
            string inputHash = null;

            // Act
            var result = await _sut.VerifyPasswordAsync(userId, inputHash);

            // Assert
            Assert.False(result);
        }


        private async Task<StudentModel> AddStudentToDbAsync(StudentModel student)
        {
            await dbContext.Students.AddAsync(student);
            await dbContext.SaveChangesAsync();
            return student;
        }

        private async Task<StudentModel> SaltHashAndAddToDbAsync(StudentModel student, string inputHash)
        {
            var saltedStudentModel = await SetSalt(student);
            var saltedHashedStudent = await SetHash(saltedStudentModel, inputHash);         
            await dbContext.Students.AddAsync(saltedHashedStudent);
            await dbContext.SaveChangesAsync();
            return saltedHashedStudent;
        }

        private StudentModel BuildTestStudent(int id)
        {
            return new StudentModel
            {
                Id = id,
                Name = "John Doe",
                MailAddress = "john.doe@example.com",
                PasswordHash = "",
                StudentSalt = "",
                TechStack = new List<string> { "C#", "ASP.NET", "SQL" },
                PhoneNumber = "123-456-7890",
                Graduation = DateTime.Now.AddYears(1),
                StartLia1 = DateTime.Now.AddMonths(3),
                EndLia1 = DateTime.Now.AddMonths(6),
                StartLia2 = DateTime.Now.AddMonths(9),
                EndLia2 = DateTime.Now.AddYears(1),
                Presentation = "Test presentation content",
                ImageUrl = "https://example.com/image.jpg",
                ConnectedTo = new List<string> { "Friend1", "Friend2" },
                LinkedInProfile = "https://www.linkedin.com/in/johndoe"
            };
        }
       
        private async Task<StudentModel> SetSalt(StudentModel student)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            student.StudentSalt = salt;

            return student;
        }
        private async Task<StudentModel> SetHash(StudentModel student, string inputHash)
        {
            var salt = student.StudentSalt.ToString();
            string finalHashedPassword = BCrypt.Net.BCrypt.HashPassword(inputHash + salt, salt);
            student.PasswordHash = finalHashedPassword;
            return student;
        }

    }
}
