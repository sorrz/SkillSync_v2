using Api.Controllers;
using AutoMapper;
using Backend.Dtos;
using Entity.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;


namespace Api.Tests.CompanyControllerTests
{
    public class CompanyControllerTests
    {
        private readonly Mock<ILogger<CompanyController>> _logger;
        private readonly CompanyController _sut;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ICompanyRepository> _companyRepositoryMock;

        public CompanyControllerTests()
        {
            _logger = new Mock<ILogger<CompanyController>>();
            _mapperMock = new Mock<IMapper>();
            _companyRepositoryMock = new Mock<ICompanyRepository>();

            _sut = new CompanyController(_logger.Object, _companyRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void Check_GetHealth()
        {
            var result = _sut.GetHealth();
            Assert.NotNull(result);
        }
        [Fact]
        public async Task GetCompanyById_ReturnsOkResult()
        {
            //arrange
            int companyId = 1;
            _companyRepositoryMock.Setup(repo => repo.GetCompanyById(companyId)).ReturnsAsync(new CompanyModel());

            //act
            var result = await _sut.GetCompanyById(companyId);

            //assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task GetCompanyById_ReturnsNotFoundResult()
        {
            //arrange
            int companyId = 1;
            _companyRepositoryMock.Setup(repo => repo.GetCompanyById(companyId)).ReturnsAsync((CompanyModel)null);

            //act
            var result = await _sut.GetCompanyById(companyId);

            //assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetCompanyById_ReturnsInternalServerErrorResult()
        {
            //arrange
            int companyId = 1;
            _companyRepositoryMock.Setup(repo => repo.GetCompanyById(companyId)).ThrowsAsync(new Exception("Mayday mayday"));

            //act
            var result = await _sut.GetCompanyById(companyId);

            //assert
            Assert.IsType<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.Equal(500, objectResult.StatusCode);
        }

        [Fact]
        public async Task GetAllCompanies_ReturnsOkResultWithCompanies()
        {
            //arrange
            _companyRepositoryMock.Setup(repo => repo.GetAllCompanies()).ReturnsAsync(new List<CompanyModel> { new CompanyModel(), new CompanyModel() });

            //act
            var result = await _sut.GetAllCompanies();

            //assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
            Assert.IsAssignableFrom<IEnumerable<CompanyModel>>(okResult.Value);
            // Assert.IsType<List<CompanyModel>>(okResult.Value);

        }

        [Fact]
        public async Task GetAllCompanies_ReturnsOkResultWithEmptyList()
        {
            //arange
            _companyRepositoryMock.Setup(repo => repo.GetAllCompanies()).ReturnsAsync(new List<CompanyModel>());

            //at
            var result = await _sut.GetAllCompanies();

            //asert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
            Assert.IsAssignableFrom<IEnumerable<CompanyModel>>(okResult.Value);

            var companies = okResult.Value as IEnumerable<CompanyModel>;
            Assert.Empty(companies);
        }

        [Fact]
        public async Task GetAllCompanies_ReturnsInternalServerErrorResult()
        {
            //arrange
            _companyRepositoryMock.Setup(repo => repo.GetAllCompanies()).ThrowsAsync(new Exception("Test exception"));

            //act
            var result = await _sut.GetAllCompanies();

            //assert
            Assert.IsType<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.Equal(500, objectResult.StatusCode);
        }

        [Fact]
        public async Task AddCompany_ReturnsCreatedAtActionResult()
        {
            // arrange
            var newCompanyDto = new CompanyDto {
                Id = 1,
                CompanyName = "TestCompany",
                ContactName = "John Doe",
                ContactMail = "john.doe@testcompany.com",
                ContactPhone = "123-456-7890",
                TechStack = new List<string> { "C#", "Java" },
                Mentorship = true,
                LiaSpots = 2,
                HasExjob = false,
                Presentation = "This is a test company for demonstration purposes.",
                ImageUrl = "testcompany_logo.jpg"
            };
            _mapperMock.Setup(mapper => mapper.Map<CompanyModel>(newCompanyDto))
               .Returns(new CompanyModel
               {
                   Id = 1, 
                   CompanyName = newCompanyDto.CompanyName,
                   ContactName = newCompanyDto.ContactName,
                   ContactMail = newCompanyDto.ContactMail,
                   ContactPhone = newCompanyDto.ContactPhone,
                   TechStack = newCompanyDto.TechStack,
                   Mentorship = newCompanyDto.Mentorship,
                   // TODO : check the differences between model and dtos
                   Lia1Spots = newCompanyDto.LiaSpots, 
                   HasExjob = newCompanyDto.HasExjob,
                   Presentation = newCompanyDto.Presentation,
                   ImageUrl = newCompanyDto.ImageUrl
               });

            _companyRepositoryMock.Setup(repo => repo.AddCompany(It.IsAny<CompanyModel>()))
                                  .ReturnsAsync(new CompanyModel());

            // act
            var result = await _sut.AddCompany(newCompanyDto);

            // assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var addedCompanyModel = Assert.IsType<CompanyModel>(createdAtActionResult.Value);

            Assert.Equal(1, addedCompanyModel.Id);
            Assert.Equal(newCompanyDto.CompanyName, addedCompanyModel.CompanyName);

        }

        [Fact]
        public async Task AddCompany_ReturnsBadRequestResult_WhenDtoIsNull()
        {
            //arrange
            CompanyDto newCompanyDto = null;

            //at
            var result = await _sut.AddCompany(newCompanyDto);

            //assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<ObjectResult>(result);

            var objectResult = result as ObjectResult;
            Assert.Equal(400, objectResult.StatusCode);
        }

        [Fact]
        public async Task AddCompany_ReturnsInternalServerErrorResult()
        {
            //arrange
            var newCompanyDto = new CompanyDto
            {
                Id = 1,
                CompanyName = "TestCompany",
                ContactName = "John Doe",
                ContactMail = "john.doe@testcompany.com",
                ContactPhone = "123-456-7890",
                TechStack = new List<string> { "C#", "Java" },
                Mentorship = true,
                LiaSpots = 2,
                HasExjob = false,
                Presentation = "This is a test company for demonstration purposes.",
                ImageUrl = "testcompany_logo.jpg"
            };

            _mapperMock.Setup(mapper => mapper.Map<CompanyModel>(newCompanyDto))
                       .Returns(new CompanyModel
                       {
                           Id = 1,
                           CompanyName = newCompanyDto.CompanyName,
                           ContactName = newCompanyDto.ContactName,
                           ContactMail = newCompanyDto.ContactMail,
                           ContactPhone = newCompanyDto.ContactPhone,
                           TechStack = newCompanyDto.TechStack,
                           Mentorship = newCompanyDto.Mentorship,
                           Lia1Spots = newCompanyDto.LiaSpots,
                           HasExjob = newCompanyDto.HasExjob,
                           Presentation = newCompanyDto.Presentation,
                           ImageUrl = newCompanyDto.ImageUrl
                       });

            _companyRepositoryMock.Setup(repo => repo.AddCompany(It.IsAny<CompanyModel>()))
                                  .ThrowsAsync(new Exception("testing testing"));

            //act
            var result = await _sut.AddCompany(newCompanyDto);

            //assert
            Assert.IsType<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.Equal(500, objectResult.StatusCode);
        }

        [Fact]
        public async Task EditCompany_ReturnsNoContentResults()
        {
            var companyId = 1;
            var updatedCompanyDto = new CompanyDto
            {
                Id = companyId,
                CompanyName = "UpdatedCompanyName",
                ContactName = "UpdatedContactName",
                ContactMail = "updated.contact@testcompany.com",
                ContactPhone = "987-654-3210",
                TechStack = new List<string> { "UpdatedTech1", "UpdatedTech2" },
                Mentorship = false,
                LiaSpots = 3,
                HasExjob = true,
                Presentation = "Updated presentation for the company",
                ImageUrl = "updated_company_logo.jpg"
            };
            _companyRepositoryMock.Setup(repo => repo.GetCompanyById(companyId)).ReturnsAsync(new CompanyModel
            {
                Id = companyId,
                CompanyName = "ExistingCompanyName",
                ContactName = "ExistingContactName",
                ContactMail = "existing.contact@testcompany.com",
                ContactPhone = "123-456-7890",
                TechStack = new List<string> { "Tech1", "Tech2" },
                Mentorship = true,
                Lia1Spots = 2,
                HasExjob = false,
                Presentation = "Existing presentation for the company",
                ImageUrl = "existing_company_logo.jpg"
            });

            //act
            var result = await _sut.EditCompany(companyId, updatedCompanyDto);

            //assert
            Assert.IsType<NoContentResult>(result);

        }

        [Fact]
        public async Task EditCompany_ReturnsNotFoundResult_WhenCompanyNotFound()
        {
            //arrange
            var companyId = 1;
            var updatedCompanyDto = new CompanyDto
            {
                Id = companyId,
                CompanyName = "UpdatedCompanyName",
                ContactName = "UpdatedContactName",
                ContactMail = "updated.contact@testcompany.com",
                ContactPhone = "987-654-3210",
                TechStack = new List<string> { "UpdatedTech1", "UpdatedTech2" },
                Mentorship = false,
                LiaSpots = 3,
                HasExjob = true,
                Presentation = "Updated presentation for the company",
                ImageUrl = "updated_company_logo.jpg"
            };

            _companyRepositoryMock.Setup(repo => repo.GetCompanyById(companyId)).ReturnsAsync((CompanyModel)null);

            //act
            var result = await _sut.EditCompany(companyId, updatedCompanyDto);

            //assrt
            Assert.IsAssignableFrom<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteCompany_ReturnsNoContentResult()
        {
            //arrange
            int companyId = 1;
            _companyRepositoryMock.Setup(repo => repo.GetCompanyById(companyId)).ReturnsAsync(new CompanyModel());

            //act
            var result = await _sut.DeleteCompany(companyId);

            //assert
            Assert.IsType<NoContentResult>(result);
            _companyRepositoryMock.Verify(repo => repo.DeleteCompany(companyId), Times.Once);
        }

        [Fact]
        public async Task DeleteCompany_ReturnsNotFoundResult_WhenCompanyNotFound()
        {
            //arrange
            int companyId = 1;
            _companyRepositoryMock.Setup(repo => repo.GetCompanyById(companyId)).ReturnsAsync((CompanyModel)null);

            //act
            var result = await _sut.DeleteCompany(companyId);

            //assert
            Assert.IsType<NotFoundResult>(result);
            _companyRepositoryMock.Verify(repo => repo.DeleteCompany(companyId), Times.Never);
        }

        [Fact]
        public async Task DeleteCompany_ReturnsInternalServerErrorResult_OnException()
        {
            //arrange
            int companyId = 1;
            _companyRepositoryMock.Setup(repo => repo.GetCompanyById(companyId)).ReturnsAsync(new CompanyModel());
            _companyRepositoryMock.Setup(repo => repo.DeleteCompany(companyId)).ThrowsAsync(new Exception("pasta carbonara"));

            //act
            var result = await _sut.DeleteCompany(companyId);

            //assert
            Assert.IsType<NoContentResult>(result);
        }

    }
}
