namespace Report.API.RabbitMq.Events
{
    public class RequestReportEvent
    {
        public Guid Id { get; set; }
        public DateTime RequestDate { get; set; }
        public RequestReportEvent()
        {
            Id = Guid.NewGuid();
            RequestDate = DateTime.UtcNow;
        }
    }
}
