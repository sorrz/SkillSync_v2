using AutoMapper;
using Entity.Dtos;
using Entity.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ILogger<StudentController> _logger;
        private readonly IMapper _mapper;

        public StudentController(IStudentRepository studentRepository, ILogger<StudentController> logger, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("GetHealthStudent")]
        public string GetHealth()
        {
            return $"Student ok @ {DateTime.Now.ToLocalTime()}";
        }

        [HttpGet("{id}", Name = "GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentDto>> GetStudentById(int id)
        {
            try
            {
                var student = await _studentRepository.GetStudentById(id);
                if (student == null)
                {
                    _logger.LogWarning($"Student with id {id} was not found", id);
                    return NotFound();
                }
                var studentDto = _mapper.Map<StudentDto>(student);
                _logger.LogInformation($"Get student whit id {id} was succeded", id);
                return Ok(studentDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while trying to get student whit id {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet(Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<StudentDto>>> GetStudents()
        {
            try
            {
                var students = await _studentRepository.GetStudents();
                if (students == null || students.Count == 0)
                {
                    _logger.LogInformation($"No students where found.");
                    return NotFound();
                }
                return Ok(_mapper.Map<List<StudentDto>>(students));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while trying to get list of students");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("{id}", Name = "DeleteById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> DeleteStudentById(int id)
        {
            var result = await _studentRepository.DeleteStudentById(id);
            if (result == false)
            {
                _logger.LogWarning($"Student with id {id} was not found", id);
                return NotFound($"Student with id {id} was not found");
            }
            _logger.LogInformation($"Student whit id {id} was deleted", id);
            return Ok($"Student whit id {id} was deleted");
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StudentDto>> EditStudent(int id, StudentDto studentDto)
        {
            try
            {
                if (studentDto == null)
                {
                    _logger.LogWarning($"Student could not be updated!");
                    return BadRequest();
                }

                // 
                var student = await _studentRepository.GetStudentById(studentDto.Id);
                //
                var updatedStudent = await _studentRepository.UpdateStudent(student);
                _logger.LogInformation("Student was updated");
                return Ok(_mapper.Map<StudentDto>(updatedStudent));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while trying to update student whit id {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpPost(Name = "RegisterStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StudentModel>> AddStudent(StudentModel student)
        {
            if (student == null)
            {
                _logger.LogError("Student is null.");
                return BadRequest("Student is null.");
            }

            var createdStudent = await _studentRepository.AddStudent(student, student.PasswordHash);

            return CreatedAtAction("GetStudentById", new { id = createdStudent.Id }, createdStudent);
        }
    }
}
