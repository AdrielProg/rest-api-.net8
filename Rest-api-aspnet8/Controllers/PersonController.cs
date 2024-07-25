using Microsoft.AspNetCore.Mvc;
using Rest_api_aspnet8.Model;
using Rest_api_aspnet8.Services;
using Rest_api_aspnet8.Model.Exceptions;
using Rest_api_aspnet8.Model.Exceptions.Person;

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
            var persons = _personService.FindAll();
            return Ok(persons);
        }

        [HttpGet("{id}")]
        public IActionResult Get(long? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var person = _personService.GetById(id);
            return Ok(person);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Person person)
        {
            if (!ModelState.IsValid)
            {
                return HandleInvalidModelState();
            }

            var createdPerson = _personService.Create(person);
            return Ok(createdPerson);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Person person)
        {
            if (!ModelState.IsValid || id != person.Id)
            {
                return HandleInvalidModelState();
            }

            var updatedPerson = _personService.Update(person);
            return Ok(updatedPerson);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _personService.Delete(id);
            return NoContent();
        }

        private IActionResult HandleInvalidModelState()
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                         .Select(e => e.ErrorMessage);
            return BadRequest(new { errors });
        }
    }
}

