using Api.Controllers;
using AutoMapper;
using Entity.Dtos;
using Entity.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Api.Tests.StudentControllerTests
{
    public class StudentControllerTest
    {
        private StudentController _sut;
        private Mock<IStudentRepository> _studentRepositoryMock;
        private Mock<ILogger<StudentController>> _loggerMock;
        private Mock<IMapper> _mapperMock;

        public StudentControllerTest()
        {
            _studentRepositoryMock = new Mock<IStudentRepository>();
            _loggerMock = new Mock<ILogger<StudentController>>();
            _mapperMock = new Mock<IMapper>();
            _sut = new StudentController(
                _studentRepositoryMock.Object,
                _loggerMock.Object,
                _mapperMock.Object);
        }

        #region GetStudentById

        [Fact]
        public async Task GetStudent_Should_Return_OkResult_Whit_Correct_Result()
        {
            //Arrange
            int id = 1;
            var expectedStudent = GetStudent(id);
            var expectedStudentDto = GetStudentDto(id);

            _studentRepositoryMock.Setup(repo => repo.GetStudentById(id))
                .ReturnsAsync(expectedStudent);
            _mapperMock.Setup(mapper => mapper.Map<StudentDto>(expectedStudent))
                .Returns(expectedStudentDto);

            //Act
            var result = await _sut.GetStudentById(id);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(okResult);
            Assert.NotNull(expectedStudent);
            var model = Assert.IsType<StudentDto>(okResult.Value);
            Assert.Equal(expectedStudent.Id, model.Id);
        }

        [Fact]
        public async Task GetStudent_Should_Return_NotFound_When_Input_Invalid_Id()
        {
            //Arrange
            int id = 2;
            var expectedStudent = GetStudent(id);
            var expectedStudentDto = GetStudentDto(id);

            _studentRepositoryMock.Setup(repo => repo.GetStudentById(id))
           .ReturnsAsync((StudentModel?)null);
            _mapperMock.Setup(mapper => mapper.Map<StudentDto>(expectedStudent))
                .Returns((StudentDto?)null);

            //Act
            var result = await _sut.GetStudentById(id);

            //Assert
            var returnedResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.NotNull(returnedResult);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task GetStudent_Should_Return_InternalServerError_When_Exception_Is_Thrown()
        {
            //Arrange
            int id = 1;
            _studentRepositoryMock.Setup(repo => repo.GetStudentById(id))
                .ThrowsAsync(new Exception());

            //Act
            var result = await _sut.GetStudentById(id);

            //Assert
            Assert.IsType<ObjectResult>(result.Result);
            var internalServerErrorResult = result.Result as ObjectResult;
            Assert.Equal(500, internalServerErrorResult.StatusCode);
            Assert.Equal("Internal Server Error", internalServerErrorResult.Value);
        }

        #endregion

        #region GetStudents

        [Fact]
        public async Task GetStudents_Should_Return_OkResult_With_Correct_Values()
        {
            //Arrange
            var studentList = GetStudents();
            var studentDtoList = GetStudentsDto();

            _studentRepositoryMock.Setup(repo => repo.GetStudents())
                .ReturnsAsync(studentList);
            _mapperMock.Setup(mapper => mapper.Map<List<StudentDto>>(studentList))
                .Returns(studentDtoList);

            //Act
            var result = await _sut.GetStudents();

            ////Assert
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<List<StudentDto>>(okResult.Value);
            Assert.Equal(studentDtoList, model);
        }

        [Fact]
        public async Task GetStudents_Should_Return_NotFound_When_No_List_Exist()
        {
            //Arrange
            var studentList = new List<StudentModel>();

            _studentRepositoryMock.Setup(repo => repo.GetStudents())
                .ReturnsAsync(studentList);

            //Act
            var result = await _sut.GetStudents();

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetStudents_Should_Return_InternalServerError_When_Exception_Is_Thrown()
        {
            //Arrange
            _studentRepositoryMock.Setup(repo => repo.GetStudents())
                .ThrowsAsync(new Exception());

            //Act
            var result = await _sut.GetStudents();

            //Assert
            var internalServerErrorResult = result.Result as ObjectResult;
            Assert.Equal(500, internalServerErrorResult.StatusCode);
            Assert.Equal("Internal Server Error", internalServerErrorResult.Value);
        }

        #endregion

        #region DeleteStudent

        [Fact]
        public async Task DeleteStudentById_Should_Return_OkResult_When_Todo_Was_Deleted()
        {
            //Arrange
            var id = 1;
            _studentRepositoryMock.Setup(repo => repo.DeleteStudentById(id))
            .ReturnsAsync(true);
            //Act
            var result = await _sut.DeleteStudentById(id);

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task DeleteStudentById_Should_Return_NotFound_When_Todo_Not_Found()
        {
            //Arrange
            var id = 1;
            _studentRepositoryMock.Setup(repo => repo.DeleteStudentById(id))
            .ReturnsAsync(false);
            //Act
            var result = await _sut.DeleteStudentById(id);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        #endregion

        #region EditStudent

        [Fact]
        public async Task EditStudent_Should_Return_BadRequest_When_Student_Could_Not_Be_Updated()
        {
            //Arrange
            var id = 1;
            var student = GetStudent(id);
            var studentDto = GetStudentDto(id);

            _studentRepositoryMock.Setup(repo => repo.UpdateStudent(It.IsAny<StudentModel>()))
           .ReturnsAsync(student);
            _mapperMock.Setup(mapper => mapper.Map<StudentModel>(studentDto))
                .Returns(student);

            //Act
            var result = await _sut.EditStudent(id, studentDto);

            ////Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task EditStudent_Should_Return_OkResult_When_Student_Has_Been_Updated()
        {
            //Arrange
            var id = 1;
            var student = GetStudent(id);
            var studentDto = GetStudentDto(id);

            _studentRepositoryMock.Setup(repo => repo.UpdateStudent(It.IsAny<StudentModel>()))
                .ReturnsAsync(student);
            _mapperMock.Setup(mapper => mapper.Map<StudentModel>(studentDto))
                .Returns(student);

            //Act
            var result = await _sut.EditStudent(id, studentDto);

            ////Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
        [Fact]
        public async Task EditStudent_Should_Return_InternalServerError_When_Exception_Is_Thrown()
        {
            //Arrange
            var id = 1;
            var student = GetStudent(id);
            var studentDto = GetStudentDto(id);
            _studentRepositoryMock.Setup(repo => repo.UpdateStudent(student))
                .ThrowsAsync(new Exception());
            _mapperMock.Setup(mapper => mapper.Map<StudentModel>(studentDto))
                .Returns(student);
            //Act
            var result = await _sut.EditStudent(id, studentDto);

            //Assert
            var internalServerErrorResult = result.Result as ObjectResult;
            Assert.Equal(500, internalServerErrorResult.StatusCode);
            Assert.Equal("Internal Server Error", internalServerErrorResult.Value);
        }

        #endregion

        #region AddStudent

        [Fact]
        public async Task AddStudent_Should_Return_CreatedAtAction_When_Successed_Adding_Student()
        {
            //Arrange
            var id = 1;
            var student = GetStudent(id);

            _studentRepositoryMock.Setup(repo => repo.AddStudent(student))
                .ReturnsAsync(student);

            //Act
            var result = await _sut.AddStudent(student);

            //Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var model = Assert.IsType<StudentModel>(createdResult.Value);
            Assert.Equal(student.Id, model.Id);
        }
        [Theory]
        [InlineData(null)]
        public async Task AddStudent_Should_Return_BadRequest_When_Student_Is_Null(StudentModel invalidStudent)
        {
            //Act
            var result = await _sut.AddStudent(invalidStudent);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Student is null.", badRequestResult.Value);
        }
        #endregion

        #region HelpersForTests
        private List<StudentModel> GetStudents()
        {
            var studentList = new List<StudentModel>();
            for (int i = 1; i <= 2; i++)
            {
                var student = GetStudent(i);
                studentList.Add(student);
            }
            return studentList;
        }
        private List<StudentDto> GetStudentsDto()
        {
            var studentDtoList = new List<StudentDto>();
            for (int i = 1; i < 2; i++)
            {
                var studentDto = GetStudentDto(i);
                studentDtoList.Add(studentDto);
            }
            return studentDtoList;
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
        #endregion
    }
}
