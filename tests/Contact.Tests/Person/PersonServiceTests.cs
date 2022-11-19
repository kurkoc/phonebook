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
        public PersonServiceTests()
        {
            _personRepositoryMock = new Mock<IPersonRepository>();
            _validatorMock = new Mock<IValidator<PersonSaveDto>>();
            _uowMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();

            _sut = new PersonService(_personRepositoryMock.Object, _validatorMock.Object, _uowMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task PersonService_GetPersonList_ShouldCountBeGreaterThanZero()
        {
            var persons = FixturePerson.GetAllPersons();

            _personRepositoryMock.Setup(q => q.GetAll(default))
            .   ReturnsAsync(()=> persons);

            PersonController contactController = new PersonController(_sut);
            var result = await contactController.GetAllPersons(default);

        }

    }
}
