using Contact.Application.Person;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contact.API.Controllers
{
    [Route("api/persons")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }


        [HttpGet]
        [Route("test")]
        public IActionResult Test()
        {
            return Ok("it works! hello from contacts service");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPersons(CancellationToken cancellationToken = default)
        {
            var persons = await _personService.GetAllPerson(cancellationToken);
            return Ok(persons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson(Guid id, CancellationToken cancellationToken = default)
        {
            var person = await _personService.GetPersonById(id, cancellationToken);
            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> AddPerson([FromBody] PersonSaveDto personSaveDto, CancellationToken cancellationToken = default)
        {
            await _personService.AddPerson(personSaveDto, cancellationToken);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> RemovePerson(Guid id, CancellationToken cancellationToken = default)
        {
            await _personService.RemovePerson(id, cancellationToken);
            return Ok();
        }

        [HttpPost("{id}/contacts")]
        public async Task<IActionResult> AddContactToPerson(Guid id, [FromBody] PersonContactSaveDto personContactSaveDto, CancellationToken cancellationToken = default)
        {
            await _personService.AddContactInfoToPerson(id, personContactSaveDto, cancellationToken);
            return Ok();
        }

        [HttpDelete("{id}/contacts/{personContactId}")]
        public async Task<IActionResult> RemoveContactToPerson(Guid id, Guid personContactId, CancellationToken cancellationToken = default)
        {
            await _personService.RemoveContactInfoPerson(id, personContactId, cancellationToken);
            return Ok();
        }

        [HttpGet("GetReportData")]
        public async Task<IActionResult> GetReportData(CancellationToken cancellationToken = default)
        {
            var datas = await _personService.GetReportData(cancellationToken);
            return Ok(datas);
        }

    }
}
