using Entity.Dtos;
using AutoMapper;
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

  
        [HttpGet("{Id}", Name = "GetCompanyById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CompanyDto>> GetCompanyById(int Id)
        {
            try
            {
                
                var company = await _companyRepository.GetCompanyById(Id);
                if (company == null)
                {
                    _logger.LogInformation($"Company with ID {Id} not found.");
                    return NotFound($"Company with ID {Id} not found.");
                }

                var companyDto = _mapper.Map<CompanyDto>(company);
                return Ok(companyDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while retrieving company with ID {Id}: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet(Name = "GetAllCompanies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CompanyDto>>> GetAllCompanies()
        {
            try
            {
                var companies = await _companyRepository.GetAllCompanies();
                if (companies == null || companies.Count == 0)
                {
                    _logger.LogInformation($"No companies where found.");
                    return NotFound($"No companies where found.");
                }

                var mappedCompanies = _mapper.Map<List<CompanyDto>>(companies);
                return Ok(mappedCompanies);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving companies: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost(Name = "RegisterCompany")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CompanyModel>> AddCompany(CompanyModel company)
        {
            if (company == null)
            {
                _logger.LogError("Company is null.");
                return BadRequest("Company is null.");
            }

            var createdCompany = await _companyRepository.AddCompany(company);

            return CreatedAtAction("GetCompanyById", new { id = createdCompany.Id }, createdCompany);
        }

        [HttpPut(Name = "EditCompany")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<CompanyDto>> EditCompany(int companyId, CompanyDto newCompanyDto)
        {
            try
            {
                if (newCompanyDto == null)
                {
                    _logger.LogInformation($"Faulty Dto from Frontend, no company information found");
                    return NoContent();
                }

                var newCompanyModel = _mapper.Map<CompanyModel>(newCompanyDto);
                var updatedCompanyModel = await _companyRepository.UpdateCompany(newCompanyModel);

                _logger.LogInformation($"Company with ID {companyId} updated successfully.");

                return Ok(_mapper.Map<CompanyDto>(updatedCompanyModel));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating company with ID {companyId}: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete(Name = "DeleteCompany")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteCompanyById(int companyId)
        {
            var result = await _companyRepository.DeleteCompanyById(companyId);
            if (result == false)
            {
                _logger.LogInformation($"No Company with ID {companyId} found!");
                return NotFound($"No Company with ID {companyId} found!");
            }

            _logger.LogInformation($"Company with ID {companyId} removed successfully!");
            return Ok($"Company with ID {companyId} removed successfully!");
        }


    }

}
