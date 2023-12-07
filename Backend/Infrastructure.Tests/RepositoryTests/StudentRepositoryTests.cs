using Entity.Models;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.ComponentModel.Design;

namespace Infrastructure.Tests.RepositoryTests
{
    public class StudentRepositoryTests
    {
        private StudentRepository _sut;
        private Mock<ILogger<StudentRepository>> _loggerMock;
        private Mock<AppDbContext> _appDbContextMock;
        //private DbContextOptions<AppDbContext> _dbContextOptions;

        public StudentRepositoryTests()
        {
            _loggerMock = new Mock<ILogger<StudentRepository>>();
            //var options = new DbContextOptionsBuilder<AppDbContext>()
            //    .UseInMemoryDatabase(databaseName: "TestDatabase")
            //    .Options;

            //var dbContext = new AppDbContext(options);
            _appDbContextMock = new Mock<AppDbContext>();
            _sut = new StudentRepository(_appDbContextMock.Object, _loggerMock.Object);
        }

        #region AddStudent

        [Fact]
        public async Task AddStudent_Should_Return_StudentModel()
        {
            //Arrange
            var id = 1;
            var student = GetStudent(id);
            _appDbContextMock.Setup(context => context.Students.AddAsync(It.IsAny<StudentModel>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync((StudentModel returnedStudent, CancellationToken token) =>
                    {
                        returnedStudent.Id = id;
                        return default;
                    });

            //Act
            var result = await _sut.AddStudent(student);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(student.Name, result.Name);

            _appDbContextMock.Verify(context => context.Students
                .AddAsync(It.IsAny<StudentModel>(), It.IsAny<CancellationToken>()), Times.Once);

            _appDbContextMock.Verify(context => context.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

            _loggerMock.Verify(logger => logger.Log(LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Once);
        }

        #endregion

        #region DeleteStudentById
        [Fact]
        public async Task DeleteStudentById_Should_Return_True_When_Student_Exists()
        {
            //Arrange
            var id = 1;
            var student = GetStudent(id);

            _appDbContextMock.Setup(context => context.Students.FindAsync(id))
                .ReturnsAsync(student);

            //Act
            var result = await _sut.DeleteStudentById(id);

            //Assert
            Assert.True(result);

            _appDbContextMock.Verify(x => x.Students.FindAsync(id), Times.Once);

            _appDbContextMock.Verify(x => x.Students.Remove(student), Times.Once);

            _appDbContextMock.Verify(x => x.SaveChanges(), Times.Once);

            _loggerMock.Verify(
                logger => logger.Log(
                    It.IsAny<LogLevel>(),
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Never);
        }
        [Fact]
        public async Task DeleteStudentById_Should_Return_False_When_Student_Not_Exists()
        {
            // Arrange
            var id = 1;

            _appDbContextMock.Setup(x => x.Students.FindAsync(id))
                .ReturnsAsync((StudentModel)null);

            // Act
            var result = await _sut.DeleteStudentById(id);

            // Assert
            Assert.False(result);

            _appDbContextMock.Verify(x => x.Students.FindAsync(id), Times.Once);

            _appDbContextMock.Verify(x => x.Students.Remove(It.IsAny<StudentModel>()), Times.Never);

            _appDbContextMock.Verify(x => x.SaveChanges(), Times.Never);

            _loggerMock.Verify(
                x => x.Log(
                    It.IsAny<LogLevel>(),
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Never);
        }
        #endregion

        #region GetStudentById
        [Fact]
        public async Task GetStudentById_Should_Return_Student_When_Exists()
        {
            // Arrange
            var id = 1;
            var student = GetStudent(id);

            _appDbContextMock.Setup(x => x.Students.FindAsync(id))
                .ReturnsAsync(student);

            // Act
            var result = await _sut.GetStudentById(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(student, result);
            Assert.Equal(student.Id, result.Id);

            _appDbContextMock.Verify(x => x.Students.FindAsync(id), Times.Once);
        }
        [Fact]
        public async Task GetStudentById_Should_Return_Null_When_Student_Not_Exists()
        {
            // Arrange
            var id = 1;

            _appDbContextMock.Setup(x => x.Students.FindAsync(id))
                .ReturnsAsync((StudentModel)null);

            // Act
            var result = await _sut.GetStudentById(id);

            // Assert
            Assert.Null(result);

            _appDbContextMock.Verify(x => x.Students.FindAsync(id), Times.Once);
        }
        #endregion

        #region GetStudents
        //Todo: Fix test GetStudents for StudentRepsository
        //[Fact]
        // public async Task GetStudents_Should_Return_List_Of_Students()
        // {
        //     //Arrange
        //     var students = GetStudents();
        //     _appDbContextMock.Setup(context => context.Students.ToListAsync())
        //     .ReturnsAsync(students);

        //     // Act
        //     var result = await _sut.GetStudents();

        //     // Assert
        //     Assert.NotNull(result);
        //     Assert.Equal(students.Count, result.Count);

        //     _appDbContextMock.Verify(context => context.Students.ToListAsync(), Times.Once);
        // }
        // [Fact]
        // public async Task GetStudents_Should_Return_Empty_List_When_No_Students_Exist()
        // {
        //     // Arrange
        //     _appDbContextMock.Setup(context => context.Students.ToListAsync())
        //         .ReturnsAsync(new List<StudentModel>());

        //     // Act
        //     var result = await _sut.GetStudents();

        //     // Assert
        //     Assert.NotNull(result);
        //     Assert.Empty(result);

        //     // Kontrollera om ToListAsync-metoden kallades på AppDbContext
        //     _appDbContextMock.Verify(context => context.Students.ToListAsync(), Times.Once);
        // }
        #endregion

        #region UpdateStudent

        [Fact]
        public async Task UpdateStudent_Should_Update_Existing_Student_And_Return_Updated_Student()
        {
            // Arrange
            var id = 1;
            var updatedStudent = GetStudent(id);
            var student = GetStudent(id);

            _appDbContextMock.Setup(x => x.Students.FindAsync(id))
                .ReturnsAsync(student);

            //_appDbContextMock.Setup(context => context.Entry(student).CurrentValues.SetValues(updatedStudent))
            //    .Verifiable();

            //_appDbContextMock.Setup(context => context.SaveChangesAsync(It.IsAny<CancellationToken>()))
            //    .ReturnsAsync(1);

            // Act
            await _sut.UpdateStudent(student);

            // Assert
            _appDbContextMock.Verify(context => context.Students.FindAsync(id), Times.Once);
        }

        #endregion

        #region HelpersForTests

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
        private StudentModel GetStudentWithIdAndName(int id, string name)
        {
            return new StudentModel
            {
                Id = id,
                Name = name,
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
        private List<StudentModel> GetStudents()
        {
            var studentList = new List<StudentModel>();
            for (int i = 1; i < 2; i++)
            {
                var student = GetStudent(i);
                studentList.Add(student);
            }
            return studentList;
        }
        #endregion
    }
}
