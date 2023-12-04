using Entity.Dtos;
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

        public SecureRepositoryTests()
        {
            _logger = new Mock<ILogger<SecureRepository>>();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var dbContext = new AppDbContext(options);
            _sut = new SecureRepository(dbContext, _logger.Object);

        }



      /*  [Fact(Skip = " not done")]
        public async void Should_CreateSafePwdWithSalt_WhenCalled()
        {

            var frontendHash = "4234a9cea21fa911110cf36e96cd887049543ca31e7c95e04028290bde1db1e0";
            var userId = 1;

            var testStudentDto = new StudentDto
            {
                Id = 1,
                Name = "John Doe",
                MailAddress = "john.doe@example.com",
                TechStack = new List<string> { "C#", "ASP.NET", "JavaScript" },
                PhoneNumber = "123-456-7890",
                Liaison1 = new LiaPeriodDto
                {
                    Start = DateTime.Parse("2023-01-01"),
                    End = DateTime.Parse("2023-06-30")
                },
                Liaison2 = new LiaPeriodDto
                {
                    Start = DateTime.Parse("2023-07-01"),
                    End = DateTime.Parse("2023-12-31")
                },
                Presentation = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                ImageUrl = "https://example.com/john_doe_image.jpg"
            };

            var testStudent = new StudentModel
            {
                Id = userId,
                Name = "John Doe",
                MailAddress = "john.doe@example.com",
                PasswordHash = "", 
                StudentSalt = "",
                TechStack = new List<string> { "C#", "ASP.NET", "JavaScript" },
                PhoneNumber = "123-456-7890",
                Graduation = DateTime.Parse("2023-12-31"),
                StartLia1 = DateTime.Parse("2023-01-01"),
                EndLia1 = DateTime.Parse("2023-06-30"),
                StartLia2 = DateTime.Parse("2023-07-01"),
                EndLia2 = DateTime.Parse("2023-12-31"),
                Presentation = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                ImageUrl = "https://example.com/john_doe_image.jpg"
            };

            var result = await _sut.CreateStudentHashAsync(testStudentDto, frontendHash);

            Assert.True(result);

        }*/


    }
}
