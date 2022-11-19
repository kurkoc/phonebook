using AutoMapper;
using BuildingBlocks.Domain;
using ns = Report.API.Domain;

namespace Report.API.Application
{
    public class ReportService : IReportService
    {
        private readonly IRepository<ns.Report> _repository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public ReportService(IRepository<ns.Report> repository, IUnitOfWork uow, IMapper mapper)
        {
            _repository = repository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task RequestReport(CancellationToken cancellationToken)
        {
            ns.Report report = ns.Report.Create(Guid.NewGuid());
            _repository.Add(report);
            await _uow.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<ReportListDto>> GetAllReports(CancellationToken cancellationToken)
        {
            var reports = await _repository.GetAll(cancellationToken);
            var mappedReports = _mapper.Map<List<ns.Report>, List<ReportListDto>>(reports);
            return mappedReports;
        }
    }
}
