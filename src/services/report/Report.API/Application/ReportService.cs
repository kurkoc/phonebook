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

        public async Task RequestReport(Guid id,DateTime requestDate, CancellationToken cancellationToken)
        {
            ns.Report report = ns.Report.Create(id,requestDate);
            _repository.Add(report);
            await _uow.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<ReportListDto>> GetAllReports(CancellationToken cancellationToken)
        {
            var reports = await _repository.GetAll(cancellationToken);
            var mappedReports = _mapper.Map<List<ns.Report>, List<ReportListDto>>(reports);
            return mappedReports;
        }

        public async Task<ReportListDto> GetReportById(Guid id, CancellationToken cancellationToken)
        {
            var report = await _repository.GetById(id,cancellationToken);
            if (report == null)
                throw new BusinessRuleException("Rapor bulunamadı");

            var mappedReport = _mapper.Map<ns.Report, ReportListDto>(report);
            return mappedReport;
        }

        public async Task SetReportPath(Guid id, string path, CancellationToken cancellationToken)
        {
            ns.Report? report = await _repository.GetById(id, cancellationToken);
            report.SetFilePath(path);

            await _uow.SaveChangesAsync(cancellationToken);
        }
    }
}
