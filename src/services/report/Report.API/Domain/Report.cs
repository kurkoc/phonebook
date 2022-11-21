using BuildingBlocks.Domain;

namespace Report.API.Domain
{
    public class Report : IAggregateRoot
    {
        #region fields
        public Guid Id { get; private set; }
        public DateTime RequestDate { get; private set; }
        public ReportStatus Status { get; set; }
        public string Path { get; set; }
        #endregion

        #region constructors
        private Report()
        { }

        private Report(Guid id, DateTime requestDate)
        {
            Id = id;
            RequestDate = requestDate;
            Status = ReportStatus.Processing;
        }
        #endregion

        #region creations
        public static Report Create(Guid id, DateTime requestDate) => new Report(id,requestDate);
        #endregion

        #region behaviours
        public void SetFilePath(string path)
        {
            Path = path;
            Status = ReportStatus.Completed;
        }
        #endregion
    }
}
