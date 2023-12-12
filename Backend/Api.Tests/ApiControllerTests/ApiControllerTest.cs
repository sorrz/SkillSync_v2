using Api.Controllers;
using AutoMapper;
using Entity.Dtos;
using Entity.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;


namespace Api.Tests.ApiControllerTests
{
    public class ApiControllerTest
    {
        private ApiController _sut;
        private Mock<ILogger<ApiController>> _logger;
        private Mock<ISecureRepository> _secureRepository;
        private Mock<IStudentRepository> _studentRepository;
        private Mock<IMapper> _mapperMock;


        public ApiControllerTest()
        {
            _logger = new Mock<ILogger<ApiController>>();
            _secureRepository = new Mock<ISecureRepository>();
            _studentRepository = new Mock<IStudentRepository>();
            _mapperMock = new Mock<IMapper>();

            _sut = new ApiController(_logger.Object, _secureRepository.Object, _studentRepository.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Login_InvalidCredentials_ReturnsBadRequest()
        {
            // Arrange
            var email = "test@example.com";
            var passwordHash = "hashedPassword";

            _secureRepository.Setup(repo => repo.LoginStudent(email, passwordHash))
                           .ReturnsAsync((StudentModel)null);

            // Act
            var result = await _sut.Login(email, passwordHash);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsOk()
        {
            // Arrange
            var email = "test@example.com";
            var passwordHash = "hashedPassword";
            var id = 1;
            var loggedInStudent = GetStudent(id);
            var StudentDto = GetStudentDto(id);

            _secureRepository.Setup(repo => repo.LoginStudent(email, passwordHash))
                           .ReturnsAsync(loggedInStudent);

            _mapperMock.Setup(mapper => mapper.Map<StudentDto>(loggedInStudent))
                       .Returns(StudentDto);

            // Act
            var result = await _sut.Login(email, passwordHash);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = (OkObjectResult)result.Result;
            Assert.IsType<StudentDto>(okResult.Value);
        }

        private StudentDto GetStudentDto(int id)
        {
            return new StudentDto
            {
                Id = 1,
                Name = "John Doe",
                MailAddress = "john.doe@example.com",
                PasswordHash = "hashed_password",
                TechStack = new List<string> { "C#", "ASP.NET", "SQL" },
                PhoneNumber = "123-456-7890",
                Graduation = new DateTime(2023, 12, 31),
                Lia1Start = new DateTime(2023, 1, 1),
                Lia1End = new DateTime(2023, 6, 30),
                Lia2Start = new DateTime(2023, 7, 1),
                Lia2End = new DateTime(2023, 12, 31),
                Presentation = "Tech Presentation",
                ImageUrl = "https://example.com/image.jpg",
                ConnectedTo = new List<string> { "Friend1", "Friend2" },
                LinkedInProfile = "https://www.linkedin.com/in/johndoe"
            };
        }

        private StudentModel GetStudent(int id)
        {
            return new StudentModel
            {
                Id = id,
                Name = "John Doe",
                MailAddress = "john.doe@example.com",
                PasswordHash = "hashedPassword123", // Replace with actual hashed password
                TechStack = new List<string> { "C#", "Java", "Python" },
                PhoneNumber = "123-456-7890",
                Graduation = new DateTime(2023, 5, 31),
                StartLia1 = new DateTime(2023, 1, 15),
                EndLia1 = new DateTime(2023, 4, 15),
                StartLia2 = new DateTime(2023, 6, 1),
                EndLia2 = new DateTime(2023, 9, 1),
                Presentation = "This is my presentation.",
                ImageUrl = "https://example.com/john_doe.jpg"
            };
        }

    }
}
