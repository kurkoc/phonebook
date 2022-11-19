using ns = Contact.Domain.AggregateRoot;
namespace Contact.Tests.Person
{
    public class FixturePerson
    {
        public static ns.Person GetPerson()
        {
            ns.Person person = ns.Person.Create(Guid.NewGuid(), "jane", "doe", "waynecorp");
            person.AddContactInfo(Guid.NewGuid(), Domain.Enums.ContactTypes.Phone, "123456");
            person.AddContactInfo(Guid.NewGuid(), Domain.Enums.ContactTypes.Email, "jane@does.com");
            person.AddContactInfo(Guid.NewGuid(), Domain.Enums.ContactTypes.Location, "new york");
            return person;
        }

        public static List<ns.Person> GetAllPersons()
        {
            List<ns.Person> persons = new();
            for (int i = 0; i < 5; i++)
            {
                persons.Add(GetPerson());
            }
            return persons;
        }

    }
}
