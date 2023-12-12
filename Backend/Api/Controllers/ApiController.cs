using AutoMapper;
using Entity.Dtos;
using Entity.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly ILogger<ApiController> _logger;
        private readonly IStudentRepository _studentRepository;
        private readonly ISecureRepository _secureRepository;
        private readonly IMapper _mapper;

        public ApiController(ILogger<ApiController> logger, ISecureRepository secureRepository, IStudentRepository studentRepository, IMapper mapper)
        {
            _logger = logger;
            _secureRepository = secureRepository;
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        [HttpPost(Name = "Login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StudentDto>> Login(string email, string passwordHash)
        {
            if (passwordHash == null || email == null)
            {
                _logger.LogError("Email or Password is null.");
                return BadRequest("Mail or Password was null");
            }

            var loggedInStudent = await _secureRepository.LoginStudent(email, passwordHash);
            if(loggedInStudent == null)
            {
                _logger.LogError($"Failed to Login {email}");
                return BadRequest($"Failed to Login {email}");
            }
            return Ok(_mapper.Map<StudentDto>(loggedInStudent));
        }

    }
}
