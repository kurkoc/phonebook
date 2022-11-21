namespace Report.API.Application
{
    public class ReportListDto
    {
        public Guid Id { get; set; }
        public DateTime RequestDate { get; set; }
        public string Path { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
    }
}
