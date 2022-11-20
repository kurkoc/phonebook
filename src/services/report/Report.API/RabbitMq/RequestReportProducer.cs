using RabbitMQ.Client;
using Report.API.RabbitMq.Configuration;
using Report.API.RabbitMq.Events;

namespace Report.API.RabbitMq
{
    public class RequestReportProducer : RabbitMqProducer<RequestReportEvent>
    {
        private readonly RabbitMqConfiguration _rabbitMqConfiguration;
        public RequestReportProducer(ConnectionFactory connectionFactory, RabbitMqConfiguration rabbitMqConfiguration) : base(connectionFactory)
        {
            _rabbitMqConfiguration = rabbitMqConfiguration;
        }

        protected override string ExchangeName => _rabbitMqConfiguration.ExchangeName;
        protected override string RoutingKeyName => _rabbitMqConfiguration.RoutingKey;


    }
}
