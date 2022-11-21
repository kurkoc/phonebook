using AutoFixture;
using AutoMapper;
using BuildingBlocks.Domain;
using Contact.API.Controllers;
using Contact.Application.Person;
using Contact.Domain.Repositories;
using FluentValidation;
using Moq;
using ns = Contact.Domain.AggregateRoot;
namespace Contact.Tests.Person
{
    public class PersonServiceTests
    {
        private readonly PersonService _sut;
        private readonly Mock<IPersonRepository> _personRepositoryMock;
        private readonly Mock<IValidator<PersonSaveDto>> _validatorMock;
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Fixture _fixture;
        public PersonServiceTests()
        {
            _personRepositoryMock = new Mock<IPersonRepository>();
            _validatorMock = new Mock<IValidator<PersonSaveDto>>();
            _uowMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _fixture = new Fixture();

            _sut = new PersonService(_personRepositoryMock.Object, _validatorMock.Object, _uowMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task PersonService_GetPersonList_ShouldCountBeGreaterThanZero()
        {
            var persons = _fixture.CreateMany<ns.Person>(7).ToList();
            var mappedPersons = _fixture.CreateMany<PersonListDto>(7).ToList();

            _personRepositoryMock.Setup(q => q.GetAll(default))
                .ReturnsAsync(persons);

            _mapperMock.Setup(q => q.Map<List<ns.Person>, List<PersonListDto>>(persons))
                .Returns(mappedPersons);

            var result = await _sut.GetAllPerson(default);

            Assert.NotNull(result);
            Assert.Equal(7, result.Count);

        }

        [Fact]
        public async Task PersonService_GetPersonById_ShouldNotBeNull()
        {
            var person = _fixture.Create<ns.Person>();
            var mappedPerson = _fixture.Create<PersonDetailDto>();

            _personRepositoryMock.Setup(q => q.GetPerson(It.IsAny<Guid>(),default))
                .ReturnsAsync(person);

            _mapperMock.Setup(q => q.Map<ns.Person, PersonDetailDto>(person))
                .Returns(mappedPerson);

            var result = await _sut.GetPersonById(Guid.NewGuid(),default);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task PersonService_AddPerson_ShouldNotBeNull()
        {
            var person = _fixture.Create<ns.Person>();
            var personSaveDto = _fixture.Create<PersonSaveDto>();

            _personRepositoryMock.Setup(q => q.Add(person));
            _uowMock.Setup(q => q.SaveChangesAsync(default)).ReturnsAsync(1);

            var result = await _sut.AddPerson(personSaveDto, default);

            Assert.NotNull(personSaveDto);
        }

        [Fact]
        public async Task PersonService_RemovePerson_ShouldBeTrue()
        {
            var person = _fixture.Create<ns.Person>();

            _personRepositoryMock.Setup(q => q.GetById(It.IsAny<Guid>(),default)).ReturnsAsync(person);
            _uowMock.Setup(q => q.SaveChangesAsync(default)).ReturnsAsync(1);

            var result = await _sut.RemovePerson(Guid.NewGuid(), default);

            Assert.True(result);
        }

    }
}
