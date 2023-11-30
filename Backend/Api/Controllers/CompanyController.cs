using AutoMapper;
using Backend.Dtos;
using Entity.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly ICompanyRepository _companyRepository;
        private IMapper _mapper;

        public CompanyController(ILogger<CompanyController> logger, ICompanyRepository companyRepository, IMapper mapper)
        {
            _logger = logger;
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetHealthCompany")]
        public string GetHealth()
        {
            return $"Company ok @ {DateTime.Now.ToLocalTime()}";
        }

        [HttpGet(Name ="GetCompanyById")]
        public async Task<IActionResult> GetCompanyById(int companyId)
        {
            try
            {
                if (!ModelState.IsValid || companyId <= 0)
                {
                    _logger.LogInformation($"Invalid companyId provided: {companyId}");
                    return BadRequest("Invalid companyId provided.");
                } 

                var company = await _companyRepository.GetCompanyById(companyId);

                if (company == null)
                {
                    _logger.LogInformation($"Company with ID {companyId} not found.");
                    return NotFound($"Company with ID {companyId} not found.");
                }

                var companyDto = _mapper.Map<CompanyDto>(company);
                return Ok(companyDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while retrieving company with ID {companyId}: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet(Name ="GetAll")]
        public async Task<IActionResult> GetAllCompanies()
        {
            try
            {
                var companies = await _companyRepository.GetAllCompanies();
                var mappedCompanies = _mapper.Map<IEnumerable<CompanyModel>>(companies);

                return Ok(mappedCompanies);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving companies: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost(Name = "RegisterCompany")]
        public async Task<IActionResult> AddCompany(CompanyDto companyDto)
        {
            if (companyDto == null)
            {
                _logger.LogError("Company DTO is null.");
                return BadRequest("Company DTO is null.");
            }

            var newCompanyModel = _mapper.Map<CompanyModel>(companyDto);
            await _companyRepository.AddCompany(newCompanyModel);

            return CreatedAtAction("GetCompanyById", new { id = newCompanyModel.Id }, newCompanyModel);
        }

        [HttpPut(Name =" EditCOmpany")]
        public async Task<IActionResult> EditCompany(int companyId, CompanyDto updatedCompanyDto)
        {
            try
            {
                var existingCompany = await _companyRepository.GetCompanyById(companyId);

                if (existingCompany == null)
                {
                    _logger.LogInformation($"Company with ID {companyId} not found.");
                    return NotFound($"Company with ID {companyId} not found.");
                }

                _mapper.Map(updatedCompanyDto, existingCompany);

                
                await _companyRepository.UpdateCompany(existingCompany);

                _logger.LogInformation($"Company with ID {companyId} updated successfully.");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating company with ID {companyId}: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete(Name ="DeleteCompany")]
        public async Task<IActionResult> DeleteCompany(int companyId)
        {
            try
            {
                var existingCompany = await _companyRepository.GetCompanyById(companyId);
                if (existingCompany == null)
                {
                    _logger.LogInformation($"Company with ID {companyId} not found.");
                    return NotFound();
                }

                _companyRepository.DeleteCompany(companyId);

                _logger.LogInformation($"Company with ID {companyId} deleted successfully.");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting company with ID {companyId}: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


    }

}
