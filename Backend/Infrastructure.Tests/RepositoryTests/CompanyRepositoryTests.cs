using Entity.Models;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;


namespace Infrastructure.Tests.RepositoryTests
{
    public class CompanyRepositoryTests
    {

        private readonly Mock<ILogger<CompanyRepository>> _logger;
        private readonly CompanyRepository _sut;

        public CompanyRepositoryTests()
        {
            _logger = new Mock<ILogger<CompanyRepository>>();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var dbContext = new AppDbContext(options);
            _sut = new CompanyRepository(dbContext, _logger.Object);
        }


        #region AddCompany

        [Fact]
        public async Task Should_AddCompany_AndReturnModel()
        {
            // Arrange
            var companyId = 1;
            var newCompany = GetCompanyWithId(companyId);


            // Act
            await _sut.AddCompany(newCompany);

            // Assert
            var addedCompany = await _sut.GetCompanyById(newCompany.Id);
            Assert.NotNull(addedCompany);
            Assert.Equal(newCompany.CompanyName, addedCompany.CompanyName);
        }

        #endregion

        #region DeleteCompany

        [Fact]
        public async Task Should_DeleteCompany_WhenDeleteCompanyByIdIsCalled()
        {
            // Arrange
            var companyIdToDelete = 4;
            var companyToDelete = GetCompanyWithId(companyIdToDelete);

            await _sut.AddCompany(companyToDelete);

            // Act
            var result = await _sut.DeleteCompanyById(companyIdToDelete);

            // Assert
            Assert.True(result);

            var deletedCompany = await _sut.GetCompanyById(companyIdToDelete);
            Assert.Null(deletedCompany);
        }

        [Fact]
        public async Task Should_ReturnFalse_WhenDeleteNonexistentCompanyByIdIsCalled()
        {
            // Arrange
            var nonexistentCompanyId = 999;

            // Act
            var result = await _sut.DeleteCompanyById(nonexistentCompanyId);

            // Assert
            Assert.False(result);
        }

        #endregion

        #region GetByCompanyId

        [Fact]
        public async Task Should_ReturnCompany_WhenGetCompanyByIdIsCalled()
        {
            // Arrange
            var companyId = 2;
            var company = GetCompanyWithId(companyId);

            await _sut.AddCompany(company);

            // Act
            var result = await _sut.GetCompanyById(companyId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(companyId, result.Id);
        }

        [Fact]
        public async Task Should_ReturnNull_WhenGetNonexistentCompanyByIdIsCalled()
        {
            // Arrange
            var nonexistentCompanyId = 999;

            // Act
            var result = await _sut.GetCompanyById(nonexistentCompanyId);

            // Assert
            Assert.Null(result);
        }

        #endregion

        #region UpdateCompany

        [Fact]
        public async Task Should_UpdateCompany_WhenUpdateCompanyIsCalled()
        {
            // Arrange
            var companyId = 3;
            var nameToVerify = "Ture Svenson";

            var originalCompany = GetCompanyWithId(companyId);
            var updatedCompany = GetCompanyWithIdAndName(companyId, nameToVerify);

            await _sut.AddCompany(originalCompany);

            // Act
            var result = await _sut.UpdateCompany(updatedCompany);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(companyId, result.Id);
            Assert.Equal(nameToVerify, result.ContactName);

            var updatedCompanyInDatabase = await _sut.GetCompanyById(companyId);
            Assert.NotNull(updatedCompanyInDatabase);
            Assert.Equal(nameToVerify, updatedCompanyInDatabase.ContactName);
        }

        [Fact]
        public async Task Should_ThrowException_WhenUpdateNonexistentCompanyIsCalled()
        {
            // Arrange
            var nonexistentCompanyId = 999;
            var nonexistentCompany = new CompanyModel
            {
                Id = nonexistentCompanyId,
                CompanyName = "NonexistentCompany",
                // ... (other properties)
            };

            // Act and Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _sut.UpdateCompany(nonexistentCompany));
        }

        #endregion

        #region Helpers

        private static CompanyModel GetCompanyWithId(int id)
        {
            return new CompanyModel
            {
                Id = id,
                CompanyName = "Test",
                ContactName = "Ture Svenson",
                ContactPhone = " 0730858655",
                ContactMail = "sven@iver.com",
                PasswordHash = "svensonthebest",
                TechStack = ["C#", "Java"],
                Mentorship = true,
                Lia1Spots = 1,
                Lia2Spots = 2,
                HasExjob = false,
                Presentation = "one two three",
                ImageUrl = "test"
            };
        }

        private static CompanyModel GetCompanyWithIdAndName(int id, string name)
        {
            return new CompanyModel
            {
                Id = id,
                CompanyName = "Test",
                ContactName = name,
                ContactPhone = " 0730858655",
                ContactMail = "sven@iver.com",
                PasswordHash = "svensonthebest",
                TechStack = ["C#", "Java"],
                Mentorship = true,
                Lia1Spots = 1,
                Lia2Spots = 2,
                HasExjob = false,
                Presentation = "one two three",
                ImageUrl = "test"
            };
        }

        #endregion

    }
}
