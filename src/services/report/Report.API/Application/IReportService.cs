namespace Report.API.Application
{
    public interface IReportService
    {
        Task RequestReport(Guid id, DateTime requestDate, CancellationToken cancellationToken);
        Task<List<ReportListDto>> GetAllReports(CancellationToken cancellationToken);
        Task<ReportListDto> GetReportById(Guid id, CancellationToken cancellationToken);
        Task SetReportPath(Guid id, string path, CancellationToken cancellationToken);
    }
}
