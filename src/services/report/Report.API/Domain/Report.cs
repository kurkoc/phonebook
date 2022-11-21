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

        private Report(Guid id)
        {
            Id = id;
            RequestDate = DateTime.UtcNow;
            Status = ReportStatus.Processing;
        }
        #endregion

        #region creations
        public static Report Create(Guid id) => new Report(id);
        #endregion

        #region behaviours
        public void SetStatusCompleted()
        {
            Status = ReportStatus.Completed;
        }
        #endregion
    }
}
