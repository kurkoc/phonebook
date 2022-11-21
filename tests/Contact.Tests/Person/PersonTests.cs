using AutoFixture;
using ns = Contact.Domain.AggregateRoot;
namespace Contact.Tests.Person
{
    public class PersonTests
    {
        [Fact(DisplayName = "Create Person")]
        public void Person_Create_ShouldNotBeNull()
        {
            ns.Person person = ns.Person.Create(Guid.NewGuid(), "jane", "doe", "waynecorp");
            Assert.NotNull(person);    
        }

        [Fact(DisplayName = "Add ContactInfo To Person")]
        public void Person_AddContact_ContactsCountMustIncreased()
        {
            ns.Person person = FixturePerson.GetPerson();
            int oldCount = person.Contacts.Count;
            person.AddContactInfo(Guid.NewGuid(), Domain.Enums.ContactTypes.Phone, "654321");
            Assert.Equal(oldCount + 1,person.Contacts.Count);
        }

        [Fact(DisplayName = "Remove ContactInfo from Person")]
        public void Person_RemoveContact_ContactsCountStaySame()
        {
            Guid id = Guid.NewGuid();
            ns.Person person = FixturePerson.GetPerson();
            int oldCount = person.Contacts.Count;
            person.AddContactInfo(id, Domain.Enums.ContactTypes.Email, "jane@gmail.com");
            person.RemoveContactInfo(id);
            Assert.Equal(oldCount, person.Contacts.Count);
        }
    }
}
