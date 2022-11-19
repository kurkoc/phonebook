using AutoMapper;
using BuildingBlock.Application.Helpers;
using ns = Report.API.Domain;

namespace Report.API.Application
{
    public class ReportProfile : Profile
    {
        public ReportProfile()
        {
            CreateMap<ns.Report, ReportListDto>()
                .ForMember(dest => dest.Status, src => src.MapFrom(c => c.Status))
                .ForMember(dest => dest.StatusName, src => src.MapFrom(c => c.Status.ToName()));
        }
    }
}
