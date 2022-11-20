namespace Report.API.RabbitMq.Events
{
    public class RequestReportEvent
    {
        public DateTime RequestDate { get; set; }
        public RequestReportEvent()
        {
            RequestDate = DateTime.UtcNow;
        }
    }
}
