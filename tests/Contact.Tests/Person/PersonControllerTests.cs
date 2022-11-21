using AutoFixture;
using BuildingBlocks.Domain;
using Contact.API.Controllers;
using Contact.Application.Person;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ns = Contact.Domain.AggregateRoot;

namespace Contact.Tests.Person
{
    public class PersonControllerTests
    {
        private readonly Mock<IPersonService> _personService;
        private readonly PersonController _controller;
        private readonly Fixture _fixture;
        public PersonControllerTests()
        {
            _personService = new Mock<IPersonService>();
            _controller = new PersonController(_personService.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Controller_GetAllPerson_ShouldBeOkStatusCode()
        {
            var persons = _fixture.CreateMany<PersonListDto>().ToList();
            _personService.Setup(q => q.GetAllPerson(default))
                .ReturnsAsync(persons);

            var controllerResult = await _controller.GetAllPersons(default);
            var objectResult = controllerResult as ObjectResult;
            var result = objectResult?.Value as List<PersonListDto>;

            Assert.Equal(200, objectResult?.StatusCode);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Controller_GetPerson_ShouldBeOkStatusCode()
        {
            var person = _fixture.Create<PersonDetailDto>();
            _personService.Setup(q => q.GetPersonById(It.IsAny<Guid>(), default))
                .ReturnsAsync(person);

            var controllerResult = await _controller.GetPerson(Guid.NewGuid(), default);
            var objectResult = controllerResult as ObjectResult;
            var result = objectResult?.Value as PersonDetailDto;

            Assert.Equal(200, objectResult?.StatusCode);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Controller_AddPerson_ShouldBeOkStatusCode()
        {
            var personSaveDto = _fixture.Create<PersonSaveDto>();
            _personService.Setup(q => q.AddPerson(personSaveDto, default));

            var result = await _controller.AddPerson(personSaveDto,default);
            var obj = result as OkResult;
            Assert.Equal(200, obj?.StatusCode);
        }

        [Fact]
        public async Task Controller_AddContactToPerson_ShouldBeOkStatusCode()
        {
            var personContactSaveDto = _fixture.Create<PersonContactSaveDto>();
            _personService.Setup(q => q.AddContactInfoToPerson(It.IsAny<Guid>(), personContactSaveDto, default));
            var result = await _controller.AddContactToPerson(Guid.NewGuid(),personContactSaveDto, default);
            var obj = result as OkResult;
            Assert.Equal(200, obj?.StatusCode);
        }

        [Fact]
        public async Task Controller_RemoveContactToPerson_ShouldBeOkStatusCode()
        {
            _personService.Setup(q => q.RemoveContactInfoPerson(It.IsAny<Guid>(), It.IsAny<Guid>(), default));
            var result = await _controller.RemoveContactToPerson(Guid.NewGuid(),Guid.NewGuid(), default);
            var obj = result as OkResult;
            Assert.Equal(200, obj?.StatusCode);
        }

        [Fact]
        public async Task Controller_GetReportData_ShouldBeOkStatusCode()
        {
            var list  = _fixture.CreateMany<ReportItemDto>().ToList();
            _personService.Setup(q => q.GetReportData(default)).ReturnsAsync(list);
            var result = await _controller.GetReportData(default);
            var obj = result as OkObjectResult;
            Assert.Equal(200, obj?.StatusCode);
        }
    }
}
