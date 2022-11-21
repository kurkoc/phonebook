using AutoMapper;
using BuildingBlocks.Domain;
using Contact.Domain.Enums;
using Contact.Domain.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ns = Contact.Domain.AggregateRoot;

namespace Contact.Application.Person
{
    public class PersonService : IPersonService
    {
        private readonly IMapper _mapper;
        private readonly IValidator<PersonSaveDto> _personSaveValidator;
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _uow;

        public PersonService(IPersonRepository personRepository, IValidator<PersonSaveDto> personSaveValidator, IUnitOfWork uow, IMapper mapper)
        {
            _personRepository = personRepository;
            _personSaveValidator = personSaveValidator;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<PersonListDto>> GetAllPerson(CancellationToken cancellationToken)
        {
            var persons = await _personRepository.GetAll(cancellationToken);
            var mappedList = _mapper.Map<List<ns.Person>, List<PersonListDto>>(persons);
            return mappedList;
        }
        public async Task<PersonDetailDto> GetPersonById(Guid id, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetPerson(id, cancellationToken);
            if (person == null)
                throw new BusinessRuleException("Kişi bulunamadı");

            var mappedPerson = _mapper.Map<ns.Person, PersonDetailDto>(person);
            return mappedPerson;
        }
        public async Task<PersonSaveDto> AddPerson(PersonSaveDto personSaveDto, CancellationToken cancellationToken)
        {
            _personSaveValidator.ValidateAndThrow(personSaveDto);

            ns.Person person = ns.Person.Create(Guid.NewGuid(), personSaveDto.FirstName, personSaveDto.LastName, personSaveDto.Company);
            _personRepository.Add(person);
            await _uow.SaveChangesAsync(cancellationToken);
            return personSaveDto;
        }
        public async Task<bool> RemovePerson(Guid id, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetById(id, cancellationToken);
            if (person == null)
                throw new BusinessRuleException("İlgili kişi bulunamadı");

            _personRepository.Delete(person);
            await _uow.SaveChangesAsync(cancellationToken);
            return true; //TODO : burası tam olmadı
        }
        public async Task AddContactInfoToPerson(Guid id, PersonContactSaveDto personContactSaveDto, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetPerson(id, cancellationToken);
            if (person == null)
                throw new BusinessRuleException("İlgili kişi bulunamadı");

            person.AddContactInfo(Guid.NewGuid(), (ContactTypes)personContactSaveDto.ContactType, personContactSaveDto.Value);
            await _uow.SaveChangesAsync(cancellationToken);
        }
        public async Task RemoveContactInfoPerson(Guid id, Guid personContactInfoId, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetPerson(id, cancellationToken);
            if (person == null)
                throw new BusinessRuleException("İlgili kişi bulunamadı");

            person.RemoveContactInfo(personContactInfoId);
            await _uow.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<ReportItemDto>> GetReportData(CancellationToken cancellationToken)
        {
            //List<ReportItemDto> reportItems = new List<ReportItemDto>()
            //{
            //    new ReportItemDto { LocationName = "Ankara", PersonCount = 4, PhoneCount = 5},
            //    new ReportItemDto { LocationName = "İstanbul", PersonCount = 2, PhoneCount = 2},
            //    new ReportItemDto { LocationName = "İzmir", PersonCount = 3, PhoneCount = 3},
            //};

            var reportItems = _personRepository.Query()
                .Include(fz => fz.Contacts).Where(fz => fz.Contacts.Any(fza => fza.TypeId == ContactTypes.Location))
                .SelectMany(fz => fz.Contacts, (p, pc) => new
                {
                    PersonId = p.Id,
                    PersonContact = pc.TypeId,
                    Location = p.Contacts.Where(fz => fz.TypeId == ContactTypes.Location).FirstOrDefault().Value
                }).ToList().GroupBy(fz => fz.Location).ToLookup(fz => fz.Key, fz => fz.ToList())
                .Select(fz => new ReportItemDto
                {
                    LocationName = fz.Key,
                    PersonCount = fz.SelectMany(fza => fza.GroupBy(fzaa => fzaa.PersonId)).Count(),
                    PhoneCount = fz.Sum(fza => fza.Count(fzaa => fzaa.PersonContact == ContactTypes.Phone))
                }).ToList();

            await Task.CompletedTask;
            return reportItems;
        }

    }
}
