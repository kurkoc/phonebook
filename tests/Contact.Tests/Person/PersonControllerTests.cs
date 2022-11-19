using AutoFixture;
using Contact.API.Controllers;
using Contact.Application.Person;
using Microsoft.AspNetCore.Mvc;
using Moq;

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

            var result = await _controller.GetAllPersons(default);
            var obj = result as ObjectResult;
            Assert.Equal(200, obj?.StatusCode);
        }
    }
}
