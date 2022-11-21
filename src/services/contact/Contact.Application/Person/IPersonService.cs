using BuildingBlocks.Domain;
using System.Threading;

namespace Contact.Application.Person
{
    public interface IPersonService
    {
        Task<List<PersonListDto>> GetAllPerson(CancellationToken cancellationToken);
        Task<PersonDetailDto> GetPersonById(Guid id, CancellationToken cancellationToken);
        Task<PersonSaveDto> AddPerson(PersonSaveDto personSaveDto, CancellationToken cancellationToken);
        Task<bool> RemovePerson(Guid id, CancellationToken cancellationToken);
        Task AddContactInfoToPerson(Guid id, PersonContactSaveDto personContactSaveDto, CancellationToken cancellationToken);
        Task RemoveContactInfoPerson(Guid id, Guid personContactInfoId, CancellationToken cancellationToken);
        Task<List<ReportItemDto>> GetReportData(CancellationToken cancellationToken);
    }
}
