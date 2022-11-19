using BuildingBlocks.Domain;
using ns = Report.API.Domain;

namespace Report.API.Application
{
    public class ReportService : IReportService
    {
        private readonly IRepository<ns.Report> _repository;
        private readonly IUnitOfWork _uow;

        public ReportService(IRepository<ns.Report> repository, IUnitOfWork uow)
        {
            _repository = repository;
            _uow = uow;
        }

        public async Task RequestReport(CancellationToken cancellationToken)
        {
            ns.Report report = ns.Report.Create(Guid.NewGuid());
            _repository.Add(report);
            await _uow.SaveChangesAsync(cancellationToken);
        }
    }
}
