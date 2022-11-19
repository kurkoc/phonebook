using Contact.Domain.Entities;
using ns = Contact.Domain.AggregateRoot;
namespace Contact.Tests.Person
{
    public class PersonContactTests
    {
        [Fact]
        public void PersonContact_Create_ShouldNotBeNull()
        {
            PersonContact personContact = PersonContact.Create(Guid.NewGuid(), FixturePerson.GetPerson(),Domain.Enums.ContactTypes.Email,"jane@doe.com");
            Assert.NotNull(personContact);
        }

        [Fact]
        public void PersonContact_CreateWithNullPerson_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => {
                PersonContact.Create(Guid.NewGuid(), null, Domain.Enums.ContactTypes.Email, "jane@doe.com");
            });

            Assert.Equal("Value cannot be null. (Parameter 'person')", exception.Message);
        }
    }
}
