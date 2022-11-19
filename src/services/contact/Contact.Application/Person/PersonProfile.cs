using AutoMapper;
using BuildingBlock.Application.Helpers;
using Contact.Domain.Entities;
using ns = Contact.Domain.AggregateRoot;
namespace Contact.Application.Person
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<ns.Person, PersonListDto>();

            CreateMap<PersonContact, PersonContactDto>()
                .ForMember(q => q.ContactType, src => src.MapFrom(q => q.TypeId))
                .ForMember(q => q.ContactTypeName, src => src.MapFrom(q => q.TypeId.ToName()));

            CreateMap<ns.Person, PersonDetailDto>();
        }
    }
}
