using BuildingBlocks.Domain;
using Contact.Domain.Entities;
using Contact.Domain.Enums;

namespace Contact.Domain.AggregateRoot
{
    public class Person : IAggregateRoot
    {
        #region fields
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Company { get; private set; }

        private List<PersonContact> _contacts;
        public IReadOnlyCollection<PersonContact> Contacts { get { return _contacts.AsReadOnly(); } }
        #endregion

        #region constructors
        private Person()
        { }

        private Person(Guid id, string firstName, string lastName, string company)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Company = company;

            _contacts = new();
        }
        #endregion

        #region creations
        public static Person Create(Guid id, string firstName, string lastName, string company) => new Person(id,firstName, lastName, company);
        #endregion

        #region behaviours
        public void AddContactInfo(Guid id,ContactTypes contactType, string value)
        {
            //some business rules if it's necessary. for ex: people dont have five contact info
            PersonContact personContact = PersonContact.Create(id, this, contactType, value);
            _contacts.Add(personContact);
        }
        public void RemoveContactInfo(Guid personContactInfoId)
        {
            var contactInfo = _contacts.FirstOrDefault(q => q.Id == personContactInfoId);
            if (contactInfo == null)
                throw new BusinessRuleException("İlgili iletişim bilgisi bulunamadı");

            _contacts.Remove(contactInfo);
        }
        #endregion
    }
}
