namespace Report.API.RabbitMq.Configuration
{
    public class RabbitMqConfiguration
    {
        public string Url { get; set; }
        public int Port { get; set; }
        public string ExchangeName { get; set; }
        public string RoutingKey { get; set; }
        public string QueueName { get; set; }
    }
}
