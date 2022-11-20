using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Report.API.RabbitMq
{
    public abstract class RabbitMqProducer<T> : RabbitMqClient, IRabbitMqProducer<T>
    {
        protected abstract string ExchangeName { get; }
        protected abstract string RoutingKeyName { get; }

        protected RabbitMqProducer(ConnectionFactory connectionFactory) : base(connectionFactory) { }


        public virtual void Publish(T @event)
        {
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            var properties = Channel.CreateBasicProperties();
            properties.ContentType = "application/json";
            properties.DeliveryMode = 2;
            properties.Timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            Channel.BasicPublish(exchange: ExchangeName, routingKey: RoutingKeyName, body: body, basicProperties: properties);
        }
    }
}
