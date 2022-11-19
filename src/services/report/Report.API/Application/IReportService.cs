namespace Report.API.Application
{
    public interface IReportService
    {
        Task RequestReport(CancellationToken cancellationToken);
        Task<List<ReportListDto>> GetAllReports(CancellationToken cancellationToken);
        Task<ReportListDto> GetReportById(Guid id, CancellationToken cancellationToken);
    }
}
