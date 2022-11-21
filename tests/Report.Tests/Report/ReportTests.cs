using ns = Report.API.Domain;
namespace Report.Tests.Report
{
    public class ReportTests
    {
        [Fact(DisplayName = "Create Report")]
        public void Report_Create_ShouldNotBeNull()
        {
            ns.Report report= ns.Report.Create(Guid.NewGuid(), DateTime.UtcNow);
            Assert.NotNull(report);
        }

        [Fact(DisplayName = "Set Path and Change Status")]
        public void Report_SetFilePath_ShouldStatusToBeCompleted()
        {
            ns.Report report = ns.Report.Create(Guid.NewGuid(), DateTime.UtcNow);
            report.SetFilePath("path");

            Assert.NotNull(report.Path);
            Assert.Equal(ns.ReportStatus.Completed, report.Status);
        }
    }
}
