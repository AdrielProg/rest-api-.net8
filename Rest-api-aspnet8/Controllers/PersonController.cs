using Microsoft.AspNetCore.Mvc;
using Rest_api_aspnet8.Model;
using Rest_api_aspnet8.Services;
using Rest_api_aspnet8.Model.Exceptions;

namespace Rest_api_aspnet8.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonService _personService;

        public PersonController(ILogger<PersonController> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var persons = _personService.FindAll();
                return Ok(persons);
            }
            catch (DataAccessException e)
            {
                _logger.LogError(e, "An error occurred while retrieving all persons.");
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(long? id)
        {
            if (id == null) { 
                return BadRequest();
            }
            try
            {
                var person = _personService.GetById(id);
                return Ok(person);
            }
            catch (BusinessException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] Person person)
        {
            if (person == null)
            {
                return BadRequest();
            }
            try
            {
                var createdPerson = _personService.Create(person);
                return Ok(person);
            }
            catch (DataAccessException e)
            {
                return StatusCode(500, e.Message);
            }
                
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Person person)
        {
            if (person == null || id != person.Id)
            {
                return BadRequest();
            }

            try
            {
                var updatedPerson = _personService.Update(person);
                return Ok(updatedPerson);
            }
            catch (BusinessException e)
            {
                return NotFound(e.Message);
            }
            catch (DataAccessException e)
            {
                _logger.LogError(e, "An error occurred while creating a person.");
                return StatusCode(500, e.Message);
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                _personService.Delete(id);
                return NoContent();
            }
            catch (BusinessException)
            {
                return NotFound();
            }
        }
    }
}
