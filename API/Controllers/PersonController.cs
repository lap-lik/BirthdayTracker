using Core.DTOs.Input.Person;
using Core.DTOs.Output.Person;
using Core.Intefaces.Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("persons")]
    public class PersonController : AuthorizedController
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<PersonResponse>> CreatePerson([FromForm] CreatePersonRequest request)
        {
            var result = await _personService.AddAsync(AuthorizedUserId, request);
            return CreatedAtAction(nameof(GetPerson), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonResponse>> GetPerson(Guid id)
        {
            var result = await _personService.GetPersonAsync(AuthorizedUserId, id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonResponse>>> GetAllPersons()
        {
            var result = await _personService.GetAllPersonsAsync(AuthorizedUserId);
            return Ok(result);
        }

        [HttpGet("upcoming-birthdays")]
        public async Task<ActionResult<IEnumerable<PersonResponse>>> GetUpcomingBirthdays()
        {
            var result = await _personService.GetUpcomingBirthdaysAsync(AuthorizedUserId);
            return Ok(result);
        }

        [HttpPatch("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdatePerson(Guid id, [FromForm] UpdatePersonRequest request)
        {
            await _personService.UpdateAsync(AuthorizedUserId, id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            await _personService.DeleteAsync(AuthorizedUserId, id);
            return NoContent();
        }
    }
}