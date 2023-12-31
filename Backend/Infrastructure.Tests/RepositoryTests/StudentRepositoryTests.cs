﻿using Castle.Core.Logging;
using Entity.Models;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.EventHandlers;
using Moq;
using Moq.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace Infrastructure.Tests.RepositoryTests
{
    public class StudentRepositoryTests
    {
        private StudentRepository _sut;
        private Mock<ILogger<StudentRepository>> _loggerMock;
        private Mock<ILogger<SecureRepository>> _loggerMock2;
        private Mock<AppDbContext> _appDbContextMock;
        private SecureRepository _secureRepository;

        public StudentRepositoryTests()
        {
            _loggerMock = new Mock<ILogger<StudentRepository>>();
            _loggerMock2 = new Mock<ILogger<SecureRepository>>();  

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var dbContext = new AppDbContext(options);
            _secureRepository = new SecureRepository(dbContext, _loggerMock2.Object);
            _sut = new StudentRepository(dbContext, _loggerMock.Object, _secureRepository);
        }

        public async Task TestInitializer()
        {
            await DeleteFromDb();
        }

        #region AddStudent

        [Fact]
        public async Task AddStudent_Should_Return_StudentModel()
        {
            //Arrange
            await TestInitializer();
            var id = 1;
            var student = CreateTestStudent(id);

            //Act
            await _sut.AddStudent(student, student.PasswordHash);

            //Assert
            var addedStudent = await _sut.GetStudentById(student.Id);
            Assert.NotNull(addedStudent);
            Assert.Equal(student.Name, addedStudent.Name);
        }

        #endregion

        #region DeleteStudentById
        [Fact]
        public async Task DeleteStudentById_Should_Return_True_When_Student_Exists()
        {
            //Arrange
            await TestInitializer();
            var id = 2;
            var student = CreateTestStudent(id);
            await _sut.AddStudent(student, student.PasswordHash);

            //Act
            var result = await _sut.DeleteStudentById(id);

            //Assert
            Assert.True(result);
           
        }

        [Fact]
        public async Task DeleteStudentById_Should_Return_False_When_Student_Not_Exists()
        {
            // Arrange
            await TestInitializer();
            var id = 3;
            var studentToDelete = CreateTestStudent(id);

            await _sut.AddStudent(studentToDelete, studentToDelete.PasswordHash);

            // Act
            var result = await _sut.DeleteStudentById(id);
            Assert.True(result);

            // Assert
            var deletedStudent = await _sut.GetStudentById(id);
            Assert.Null(deletedStudent);

        }

        #endregion

        #region GetStudentById
        [Fact]
        public async Task GetStudentById_Should_Return_Student_When_Exists()
        {
            // Arrange
            await TestInitializer();
            var id = 4;
            var student = CreateTestStudent(id);
            await _sut.AddStudent(student, student.PasswordHash);

            // Act
            var result = await _sut.GetStudentById(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(student, result);
            Assert.Equal(student.Id, result.Id);

        }

        [Fact]
        public async Task GetStudentById_Should_Return_Null_When_Student_Not_Exists()
        {
            // Arrange
            await TestInitializer();
            var id = 10;

            // Act
            var result = await _sut.GetStudentById(id);

            // Assert
            Assert.Null(result);

        }

        #endregion

        #region GetStudents
        /// <summary>
        /// If this test fails, run it multiple times. Seems to be some kind of bug!
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetStudents_Should_Return_List_Of_Students()
        {
            //Arrange
            await TestInitializer();
            var students = GetStudents();
            foreach(var student in students)
            {
                await _sut.AddStudent(student, student.PasswordHash);
            }

            // Act
            var result = await _sut.GetStudents();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(students.Count, result.Count);
            
        }

        [Fact]
        public async Task GetStudents_Should_Return_Empty_List_When_No_Students_Exist()
        {
            // Arrange
            await TestInitializer();
            for (var i = 0; i < 100; i++)
            {
                await _sut.DeleteStudentById(i);
            }

            // Act
            var result = await _sut.GetStudents();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);

        }
        #endregion

        #region UpdateStudent

        [Fact]
        public async Task UpdateStudent_Should_Update_Existing_Student_And_Return_Updated_Student()
        {
            await TestInitializer();
            var studentId = 9;
            var newName = "Mother of Dragons";
            var student = CreateTestStudent(studentId);
            var updatedStudent = CreateTestStudent(studentId, newName);

            // Act
            await _sut.AddStudent(student, student.PasswordHash);

            var result = await _sut.UpdateStudent(updatedStudent);

            Assert.NotNull(result);
            Assert.IsType<StudentModel>(result);
            Assert.Equal(student.Id, result.Id);
            Assert.Equal(newName, result.Name);
        }

        #endregion

        #region HelpersForTests

        public static StudentModel CreateTestStudent(int id = 1, string name = "John Doe")
        {
            return new StudentModel
            {
                Id = id,
                Name = name,
                MailAddress = $"{name.Replace(" ", string.Empty).ToLower()}@example.com",
                PasswordHash = "hashedPassword", // replace with an actual hash
                StudentSalt = "studentSalt", // replace with an actual salt
                TechStack = new List<string> { "C#", "ASP.NET", "SQL" },
                PhoneNumber = "123-456-7890",
                Graduation = DateTime.Now.AddYears(1),
                StartLia1 = DateTime.Now.AddMonths(3),
                EndLia1 = DateTime.Now.AddMonths(6),
                StartLia2 = DateTime.Now.AddMonths(9),
                EndLia2 = DateTime.Now.AddYears(1),
                Presentation = $"Test presentation content for {name}",
                ImageUrl = $"https://example.com/{name.ToLower()}.jpg",
                ConnectedTo = new List<string> { "Friend1", "Friend2" },
                LinkedInProfile = $"https://www.linkedin.com/in/{name.Replace(" ", string.Empty)}"
            };
        }
       
        private List<StudentModel> GetStudents()
        {
            var studentList = new List<StudentModel>();
            for (int i = 50; i <= 53; i++)
            {
                var student = CreateTestStudent(i);
                studentList.Add(student);
            }
            return studentList;
        }

        private async Task<bool> DeleteFromDb()
        {
            var students = await _sut.GetStudents();
            foreach (var student in students)
            {
                await _sut.DeleteStudentById(student.Id);
            }
            return true;
        }
        #endregion
    }
}
