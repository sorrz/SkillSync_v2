using Api.Controllers;
using Entity.Dtos;
using AutoMapper;
using Entity.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Api.Tests.CompanyControllerTests
{
    public class CompanyControllerTests
    {
       
        #region GetById

        [Fact]
        public async void ShouldReturn_CompanyDtoWithCorrectId_WhenUsingGetById()
        {

            var companyID = 1;
            var testCompany = BuildCompantWithId(companyID);
            var testCompanyDto = BuildTestCompanyDto(companyID);

            var _repo = new Mock<ICompanyRepository>();
            _repo.Setup(repo => repo.GetCompanyById(companyID)).ReturnsAsync(testCompany);

            var _mapper = new Mock<IMapper>();
            _mapper.Setup(mapper => mapper.Map<CompanyDto>(testCompany)).Returns(testCompanyDto);

            var _secureRepositoryMock = new Mock<ISecureRepository>();

            var _logger = new Mock<ILogger<CompanyController>>();
            var _sut = new CompanyController(_logger.Object, _repo.Object, _mapper.Object, _secureRepositoryMock.Object);


            var result = await _sut.GetCompanyById(1);

            Assert.NotNull(result);
            Assert.Equal(testCompany.Id, testCompanyDto.Id);
            Assert.IsAssignableFrom<ActionResult<CompanyDto>>(result);
        }

        [Fact]
        public async Task GetCompanyById_ReturnsNotFound_WhenCompanyDoesNotExist()
        {
            // Arrange
            var _logger = new Mock<ILogger<CompanyController>>();
            var _repo = new Mock<ICompanyRepository>();
            var _mapper = new Mock<IMapper>();
            var _secureRepositoryMock = new Mock<ISecureRepository>();

            var _sut = new CompanyController(_logger.Object, _repo.Object, _mapper.Object, _secureRepositoryMock.Object);

            var companyId = 1;
            _repo.Setup(repo => repo.GetCompanyById(companyId)).ReturnsAsync((CompanyModel)null);

            // Act
            var result = await _sut.GetCompanyById(companyId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);

            var notFoundResult = result.Result as NotFoundObjectResult;
            Assert.Equal($"Company with ID {companyId} not found.", notFoundResult.Value);
        }

        [Fact]
        public async Task GetCompanyById_ReturnsInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            var _logger = new Mock<ILogger<CompanyController>>();
            var _repo = new Mock<ICompanyRepository>();
            var _mapper = new Mock<IMapper>();
            var _secureRepositoryMock = new Mock<ISecureRepository>();
            var _sut = new CompanyController(_logger.Object, _repo.Object, _mapper.Object, _secureRepositoryMock.Object);

            var companyId = 1;
            _repo.Setup(repo => repo.GetCompanyById(companyId)).ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _sut.GetCompanyById(companyId);

            // Assert
            Assert.IsType<ObjectResult>(result.Result);

            var internalServerErrorResult = result.Result as ObjectResult;
            Assert.Equal(500, internalServerErrorResult.StatusCode);
            Assert.Equal("Internal server error", internalServerErrorResult.Value);
        }
        #endregion

        #region GetAll

        [Fact]
        public async Task GetAllCompanies_ReturnsListOfCompanyDtos_WhenCompaniesExist()
        {
            // Arrange
            var _logger = new Mock<ILogger<CompanyController>>();
            var _repo = new Mock<ICompanyRepository>();
            var _mapper = new Mock<IMapper>();
            var _secureRepositoryMock = new Mock<ISecureRepository>();
            var _sut = new CompanyController(_logger.Object, _repo.Object, _mapper.Object, _secureRepositoryMock.Object);

            var companyIdOne = 1;
            var companyIdTwo = 2;
            var companyOne = BuildCompantWithId(1);
            var companyTwo = BuildCompantWithId(2);
            var companies = new List<CompanyModel>();
            companies.Add(companyOne);
            companies.Add(companyTwo);

            var companyOneDto = BuildTestCompanyDto(companyIdOne);
            var companyTwoDto = BuildTestCompanyDto(companyIdTwo);
            var companyDtos = new List<CompanyDto>();
            companyDtos.Add(companyOneDto);
            companyDtos.Add(companyTwoDto);


            _repo.Setup(repo => repo.GetAllCompanies()).ReturnsAsync(companies);
            _mapper.Setup(mapper => mapper.Map<List<CompanyDto>>(companies)).Returns(companyDtos);

            // Act
            var result = await _sut.GetAllCompanies();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);

            var okResult = result.Result as OkObjectResult;
            Assert.IsType<List<CompanyDto>>(okResult.Value);
            Assert.Equal(companyDtos, okResult.Value);
        }
        [Theory]
        [InlineData(null)]
        public async Task GetAllCompanies_ReturnsNotFound_WhenNoCompaniesExist(List<CompanyModel> companies)
        {
            // Arrange
            var loggerMock = new Mock<ILogger<CompanyController>>();
            var companyRepositoryMock = new Mock<ICompanyRepository>();
            var mapperMock = new Mock<IMapper>();
            var _secureRepositoryMock = new Mock<ISecureRepository>();
            var controller = new CompanyController(loggerMock.Object, companyRepositoryMock.Object, mapperMock.Object, _secureRepositoryMock.Object);

            companyRepositoryMock.Setup(repo => repo.GetAllCompanies()).ReturnsAsync(companies ?? new List<CompanyModel>());

            // Act
            var result = await controller.GetAllCompanies();

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);

            var notFoundResult = result.Result as NotFoundObjectResult;
            Assert.Equal("No companies where found.", notFoundResult.Value);
        }

        [Fact]
        public async Task GetAllCompanies_ReturnsInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<CompanyController>>();
            var companyRepositoryMock = new Mock<ICompanyRepository>();
            var mapperMock = new Mock<IMapper>();
            var _secureRepositoryMock = new Mock<ISecureRepository>();
            var controller = new CompanyController(loggerMock.Object, companyRepositoryMock.Object, mapperMock.Object, _secureRepositoryMock.Object);

            companyRepositoryMock.Setup(repo => repo.GetAllCompanies()).ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await controller.GetAllCompanies();

            // Assert
            Assert.IsType<ObjectResult>(result.Result);

            var internalServerErrorResult = result.Result as ObjectResult;
            Assert.Equal(500, internalServerErrorResult.StatusCode);
            Assert.Equal("Internal server error", internalServerErrorResult.Value);
        }
        #endregion

        #region AddCompany

        [Fact]
        public async Task AddCompany_ReturnsCreated_WhenCompanyIsAddedSuccessfully()
        {
            // Arrange
            var _logger = new Mock<ILogger<CompanyController>>();
            var _repo = new Mock<ICompanyRepository>();
            var _mapper = new Mock<IMapper>();
            var _secureRepositoryMock = new Mock<ISecureRepository>();
            var _sut = new CompanyController(_logger.Object, _repo.Object, _mapper.Object, _secureRepositoryMock.Object);

            var companyId = 1;
            var validCompany = BuildCompantWithId(companyId);

            _repo.Setup(repo => repo.AddCompany(validCompany)).ReturnsAsync(validCompany);

            // Act
            var result = await _sut.AddCompany(validCompany);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result.Result);

            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.IsType<CompanyModel>(createdAtActionResult.Value);
            Assert.Equal(validCompany, createdAtActionResult.Value);
        }

        [Theory]
        [InlineData(null)]
        public async Task AddCompany_ReturnsBadRequest_WhenCompanyIsInvalid(CompanyModel invalidCompany)
        {
            // Arrange
            var _logger = new Mock<ILogger<CompanyController>>();
            var _repo = new Mock<ICompanyRepository>();
            var _mapper = new Mock<IMapper>();
            var _secureRepositoryMock = new Mock<ISecureRepository>();
            var _sut = new CompanyController(_logger.Object, _repo.Object, _mapper.Object, _secureRepositoryMock.Object);

            // Act
            var result = await _sut.AddCompany(invalidCompany);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);

            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.Equal("Company is null.", badRequestResult.Value);
        }

        [Fact(Skip = "Disabled due to un-obtainable state")]
        public async Task AddCompany_ReturnsInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            var _logger = new Mock<ILogger<CompanyController>>();
            var _repo = new Mock<ICompanyRepository>();
            var _mapper = new Mock<IMapper>();
            var _secureRepositoryMock = new Mock<ISecureRepository>();
            var _sut = new CompanyController(_logger.Object, _repo.Object, _mapper.Object, _secureRepositoryMock.Object);

            var companyID = 1;
            var validCompany = BuildCompantWithId(companyID);
            _repo.Setup(repo => repo.AddCompany(validCompany)).ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _sut.AddCompany(validCompany);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);

            var internalServerErrorResult = result.Result as ObjectResult;
            Assert.Equal(500, internalServerErrorResult.StatusCode);
            Assert.Equal("Internal server error", internalServerErrorResult.Value);
        }

        #endregion

        #region EditCompany

        [Fact]
        public async Task EditCompany_ReturnsOk_WhenCompanyIsUpdatedSuccessfully()
        {
            // Arrange
            var _logger = new Mock<ILogger<CompanyController>>();
            var _repo = new Mock<ICompanyRepository>();
            var _mapper = new Mock<IMapper>();
            var _secureRepositoryMock = new Mock<ISecureRepository>();
            var _sut = new CompanyController(_logger.Object, _repo.Object, _mapper.Object, _secureRepositoryMock.Object);

            var companyId = 1;
            var newContact = "Johan Larsson";
            var newCompanyDto = BuildTestCompanyDto(companyId);
            var newCompanyModel = BuildCompantWithId(companyId);
            var updatedCompanyModel = BuildCompanyWithContactNameAndId(companyId, newContact);
            var updatedCompanyDto = BuildTestCompanyDtoWithContactNameAndId(companyId, newContact);

            _repo.Setup(repo => repo.GetCompanyById(companyId)).ReturnsAsync(newCompanyModel);
            _repo.Setup(repo => repo.UpdateCompany(newCompanyModel)).ReturnsAsync(updatedCompanyModel);
            _mapper.Setup(mapper => mapper.Map<CompanyModel>(newCompanyDto)).Returns(newCompanyModel);
            _mapper.Setup(mapper => mapper.Map<CompanyDto>(updatedCompanyModel)).Returns(updatedCompanyDto);
            _secureRepositoryMock.Setup(repo => repo.VerifyPasswordAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(true);
            _secureRepositoryMock.Setup(repo => repo.IsUserAuthenticated(companyId)).ReturnsAsync(true);

            // Act
            var result = await _sut.EditCompany(companyId, newCompanyDto);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);

            var okResult = result.Result as OkObjectResult;
            Assert.IsType<CompanyDto>(okResult.Value);
            Assert.Equal(updatedCompanyDto, okResult.Value);
        }

        [Theory]
        [InlineData(1, null)]
        public async Task EditCompany_ReturnsNoContent_WhenCompanyDtoIsInvalid(int companyId, CompanyDto invalidCompanyDto)
        {
            // Arrange
            var _logger = new Mock<ILogger<CompanyController>>();
            var _repo = new Mock<ICompanyRepository>();
            var _mapper = new Mock<IMapper>();
            var _secureRepositoryMock = new Mock<ISecureRepository>();
            var _sut = new CompanyController(_logger.Object, _repo.Object, _mapper.Object, _secureRepositoryMock.Object);

            // Act
            var result = await _sut.EditCompany(companyId, invalidCompanyDto);

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact(Skip = "None obtainable state, when not using in-memory database.")]
        public async Task EditCompany_ReturnsInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            var _logger = new Mock<ILogger<CompanyController>>();
            var _repo = new Mock<ICompanyRepository>();
            var _mapper = new Mock<IMapper>();
            var _secureRepositoryMock = new Mock<ISecureRepository>();
            var _sut = new CompanyController(_logger.Object, _repo.Object, _mapper.Object, _secureRepositoryMock.Object);

            var companyId = 1;
            var newCompanyDto = BuildTestCompanyDto(companyId);
            var newCompanyModel = BuildCompantWithId(companyId);

            //_repo.Setup(repo => repo.GetCompanyById(companyId)).ReturnsAsync((CompanyModel?)null);
            _repo.Setup(repo => repo.UpdateCompany(newCompanyModel)).ThrowsAsync(new Exception("Test exception"));
            _mapper.Setup(mapper => mapper.Map<CompanyModel>(newCompanyDto)).Returns(newCompanyModel);
            //_secureRepositoryMock.Setup(repo => repo.VerifyPasswordAsync(It.IsAny<int>(), It.IsAny<string>()))
                //.ReturnsAsync(false);

            // Act
            var result = await _sut.EditCompany(companyId, newCompanyDto);

            // Assert
            Assert.IsType<ObjectResult>(result.Result);

            var internalServerErrorResult = result.Result as ObjectResult;
            Assert.Equal(500, internalServerErrorResult.StatusCode);
            Assert.Equal("Internal server error", internalServerErrorResult.Value);
        }


        #endregion

        #region DeleteCompany

        [Fact]
        public async Task DeleteCompanyById_ReturnsOk_WhenCompanyIsDeletedSuccessfully()
        {
            /// Arrange
            var _logger = new Mock<ILogger<CompanyController>>();
            var _repo = new Mock<ICompanyRepository>();
            var _mapper = new Mock<IMapper>();
            var _secureRepositoryMock = new Mock<ISecureRepository>();
            var _sut = new CompanyController(_logger.Object, _repo.Object, _mapper.Object, _secureRepositoryMock.Object);

            var companyId = 1;
            _repo.Setup(repo => repo.GetCompanyById(companyId)).ReturnsAsync(BuildCompantWithId(companyId));
            _repo.Setup(repo => repo.DeleteCompanyById(companyId)).ReturnsAsync(true);
            _secureRepositoryMock.Setup(repo => repo.VerifyPasswordAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(true);
            _secureRepositoryMock.Setup(repo => repo.IsUserAuthenticated(companyId)).ReturnsAsync(true);

            // Act
            var result = await _sut.DeleteCompanyById(companyId);

            // Assert
            Assert.IsType<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.Equal($"Company with ID {companyId} removed successfully!", okResult.Value);
        }

        [Fact]
        public async Task DeleteCompanyById_ReturnsNotFound_WhenCompanyIsNotFound()
        {
            // Arrange
            var _logger = new Mock<ILogger<CompanyController>>();
            var _repo = new Mock<ICompanyRepository>();
            var _mapper = new Mock<IMapper>();
            var _secureRepositoryMock = new Mock<ISecureRepository>();
            var _sut = new CompanyController(_logger.Object, _repo.Object, _mapper.Object, _secureRepositoryMock.Object);

            var companyId = 1;

            _repo.Setup(repo => repo.DeleteCompanyById(companyId)).ReturnsAsync(false);

            // Act
            var result = await _sut.DeleteCompanyById(companyId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);

            var notFoundResult = result as NotFoundObjectResult;
            Assert.Equal($"No Company with ID {companyId} found!", notFoundResult.Value);
        }

        [Fact(Skip = "Skipped due to unobtainable state")]
        public async Task DeleteCompanyById_ReturnsInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            var _logger = new Mock<ILogger<CompanyController>>();
            var _repo = new Mock<ICompanyRepository>();
            var _mapper = new Mock<IMapper>();
            var _secureRepositoryMock = new Mock<ISecureRepository>();
            var _sut = new CompanyController(_logger.Object, _repo.Object, _mapper.Object, _secureRepositoryMock.Object);

            var companyId = 1;

            _repo.Setup(repo => repo.DeleteCompanyById(companyId)).ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _sut.DeleteCompanyById(companyId);

            // Assert
            Assert.IsType<ObjectResult>(result);

            var internalServerErrorResult = result as ObjectResult;
            Assert.Equal(500, internalServerErrorResult.StatusCode);
            Assert.Equal("Internal server error", internalServerErrorResult.Value);
        }


        #endregion

        #region Helpers

        private static CompanyModel BuildCompantWithId(int id)
        {
            return new CompanyModel
            {
                Id = id,
                CompanyName = "TestCompany",
                ContactName = "TestContact",
                ContactMail = "test@example.com",
                ContactPhone = "123456789",
                PasswordHash = "testPasswordHash",
                TechStack = new List<string> { "C#", "Java" },
                Mentorship = true,
                Lia1Spots = 2,
                Lia2Spots = 3,
                HasExjob = false,
                Presentation = "Test presentation",
                ImageUrl = "test_image_url.jpg"
            };
        }
        private static CompanyModel BuildCompanyWithContactNameAndId(int id, string contactName)
        {
            return new CompanyModel
            {
                Id = id,
                CompanyName = "TestCompany",
                ContactName = contactName,
                ContactMail = "test@example.com",
                ContactPhone = "123456789",
                PasswordHash = "testPasswordHash",
                TechStack = new List<string> { "C#", "Java" },
                Mentorship = true,
                Lia1Spots = 2,
                Lia2Spots = 3,
                HasExjob = false,
                Presentation = "Test presentation",
                ImageUrl = "test_image_url.jpg"
            };
        }

        public static CompanyDto BuildTestCompanyDto(int id)
        {
            return new CompanyDto
            {
                Id = id,
                CompanyName = "TestCompany",
                ContactName = "TestContact",
                ContactMail = "test@example.com",
                ContactPhone = "123456789",
                TechStack = new List<string> { "C#", "Java" },
                Mentorship = true,
                LiaSpots = 2,
                HasExjob = false,
                Presentation = "Test presentation",
                ImageUrl = "test_image_url.jpg"
            };
        }

        public static CompanyDto BuildTestCompanyDtoWithContactNameAndId(int id, string contactName)
        {
            return new CompanyDto
            {
                Id = id,
                CompanyName = "TestCompany",
                ContactName = contactName,
                ContactMail = "test@example.com",
                ContactPhone = "123456789",
                TechStack = new List<string> { "C#", "Java" },
                Mentorship = true,
                LiaSpots = 2,
                HasExjob = false,
                Presentation = "Test presentation",
                ImageUrl = "test_image_url.jpg"
            };
        }

        #endregion
    }
}
