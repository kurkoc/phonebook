using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Threading;
using System.Text;
using Report.API.Application;
using System.Threading.Channels;
using Report.API.RabbitMq.Configuration;

namespace Report.API.BackgroundServices
{
    public class ReportGeneratorBackgroundService : BackgroundService
    {
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        private readonly IServiceProvider _serviceProvider;
        private readonly RabbitMqConfiguration _rabbitMqConfiguration;

        public ReportGeneratorBackgroundService(IServiceProvider serviceProvider, ConnectionFactory connectionFactory,RabbitMqConfiguration rabbitMqConfiguration)
        {
            _serviceProvider = serviceProvider;
            _rabbitMqConfiguration = rabbitMqConfiguration;
            _connectionFactory = connectionFactory;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(
                exchange: _rabbitMqConfiguration.ExchangeName,
                type: "direct",
                durable: true,
                autoDelete: false);

            _channel.QueueDeclare(
                queue: _rabbitMqConfiguration.QueueName,
                durable: false,
                exclusive: false,
                autoDelete: false);

            _channel.QueueBind(
                queue: _rabbitMqConfiguration.QueueName,
                exchange: _rabbitMqConfiguration.ExchangeName,
                routingKey: _rabbitMqConfiguration.RoutingKey);

            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _connection.Close();
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            using (var scope = _serviceProvider.CreateScope())
            {
                var _reportService = scope.ServiceProvider.GetRequiredService<IReportService>();
                var consumer = new AsyncEventingBasicConsumer(_channel);
                consumer.Received += Consumer_Received;
                _channel.BasicConsume(queue: _rabbitMqConfiguration.QueueName, autoAck: false, consumer: consumer);
            }

            await Task.CompletedTask;
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            var message = Encoding.UTF8.GetString(@event.Body.ToArray());
            try
            {
                Console.WriteLine(message);
                _channel.BasicAck(@event.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                _channel.BasicNack(@event.DeliveryTag, false, false);
            }
            await Task.CompletedTask;
        }
    }
}
