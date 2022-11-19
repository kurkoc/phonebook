using Contact.Domain.AggregateRoot;
using Contact.Domain.Enums;

namespace Contact.Domain.Entities
{
    public class PersonContact
    {
        #region fields
        public Guid Id { get; set; }
        public ContactTypes TypeId { get; set; }
        public string Value { get; set; }
        public Guid PersonId { get; set; }
        public Person Person { get; set; } 
        #endregion

        #region constructors
        private PersonContact() { }
        private PersonContact(Guid id, Person person, ContactTypes typeId, string value)
        {
            if (person == null)
                throw new ArgumentNullException(nameof(person));

            Id = id;
            PersonId = person.Id;
            Person = person;
            TypeId = typeId;
            Value = value;
        } 
        #endregion

        #region creations
        public static PersonContact Create(Guid id,Person person, ContactTypes typeId, string value) => new PersonContact(id,person, typeId, value);
        #endregion
    }
}
