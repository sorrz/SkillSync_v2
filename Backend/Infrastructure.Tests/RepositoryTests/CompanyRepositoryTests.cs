﻿using Entity.Models;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tests.RepositoryTests
{
    public class CompanyRepositoryTests
    {
        private CompanyRepository _sut;
        private Mock<AppDbContext> _mockDbContext;
        private Mock<DbSet<CompanyModel>> _mockCompanies;
        private Mock<ILogger<CompanyRepository>> _mockLogger;
        public CompanyRepositoryTests()
        {
            _mockDbContext = new Mock<AppDbContext>();
            _mockCompanies = new Mock<DbSet<CompanyModel>>();
            _mockLogger = new Mock<ILogger<CompanyRepository>>();

            _mockDbContext.Setup(c => c.Companies).Returns(_mockCompanies.Object);

            _sut = new CompanyRepository(_mockDbContext.Object, _mockLogger.Object);
        }
        [Fact]
        public void Should_MatchTypeOf_Repository()
        {
            //arrange
            var sut = _sut.GetType();

            //assert
            Assert.Equal(typeof(CompanyRepository), sut);
        }

        [Fact]
        public void ShouldReturn_OK_FromGetOK()
        {
            //arrange
            var expected = "OK";

            //act
            var result = _sut.GetOk();

            //assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Should_ReturnEmptyList_WhenNoCompaniesExist()
        {
            // arrange
            var companiesData = new List<CompanyModel>();
            var mockCompanies = new Mock<DbSet<CompanyModel>>();

            // Set up IQueryable 
            mockCompanies.As<IQueryable<CompanyModel>>().Setup(m => m.Provider).Returns(companiesData.AsQueryable().Provider);
            mockCompanies.As<IQueryable<CompanyModel>>().Setup(m => m.Expression).Returns(companiesData.AsQueryable().Expression);
            mockCompanies.As<IQueryable<CompanyModel>>().Setup(m => m.ElementType).Returns(companiesData.AsQueryable().ElementType);
            mockCompanies.As<IQueryable<CompanyModel>>().Setup(m => m.GetEnumerator()).Returns(() => companiesData.AsQueryable().GetEnumerator());

            _mockDbContext.Setup(c => c.Companies).Returns(mockCompanies.Object);

            //act
            var companies = _sut.GetAllCompanies();

            //assert
            Assert.Empty(companies);
        }

        [Fact]
        public async Task Should_AddCompany_WhenCompanyIsCalled()
        {
            //arrange
            var newCompany = new CompanyModel
            {
                Id = 1,
                CompanyName = "Test",
                ContactName = "Sven Svenson",
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

                        _mockCompanies.Setup(c => c.FindAsync(newCompany.Id))
            .ReturnsAsync(newCompany); // Set up the FindAsync to return the newCompany when called with companyId

                        _mockDbContext.Setup(c => c.Companies)
                            .Returns(_mockCompanies.Object);

            //act
            await _sut.AddCompany(newCompany);

            // assert
            var addedCompany = await _sut.GetCompanyById(newCompany.Id);
            Assert.NotNull(addedCompany);
            Assert.Equal(newCompany.CompanyName, addedCompany.CompanyName);
        }

        [Fact]
        public async Task Should_UpdateCompany_WhenUpdateCompanyIsCalled()
        {
            //arrange
            var companyId = 1;
            var newCompany = new CompanyModel
            {
                Id = 1,
                CompanyName = "Test",
                ContactName = "Sven Svenson",
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

            var updatedCompany = new CompanyModel
            {
                Id = 1,
                CompanyName = "Test",
                ContactName = "Arne Svenson",
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

            _mockCompanies.Setup(c => c.FindAsync(newCompany.Id))
        .ReturnsAsync(newCompany); // Set up the FindAsync to return the newCompany when called with companyId

            _mockDbContext.Setup(c => c.Companies)
                .Returns(_mockCompanies.Object);


            //act
            await _sut.AddCompany(newCompany);
            await _sut.UpdateCompany(updatedCompany);

            //assert
            var retrievedCompany = await _sut.GetCompanyById(companyId);
            Assert.NotNull(retrievedCompany);
            Assert.Equal(updatedCompany.CompanyName, retrievedCompany.CompanyName);
        }

        [Fact]
        public void Shoudl_DeleteCompany_WhenDeleteCompanyIsCalled()
        {
            //arrange
            var companyIdToDelete = 1;
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var dbContext = new AppDbContext(options))
            {
                var companyToDelete = new CompanyModel
                {
                    Id = 1,
                    CompanyName = "Test",
                    ContactName = "Sven Svenson",
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
                dbContext.Companies.Add(companyToDelete);
                dbContext.SaveChanges();

                var mockLogger = new Mock<ILogger<CompanyRepository>>();
                var repository = new CompanyRepository(dbContext, mockLogger.Object);

                //act
                repository.DeleteCompany(companyIdToDelete);

                //assert
                // mockLogger.Verify(
                // x => x.LogError(It.IsAny<string>()),
                // Times.Never);

                var deletedCompany = dbContext.Companies.FirstOrDefault(c => c.Id == companyIdToDelete);
                Assert.Null(deletedCompany);
            }
        }


    }
}
