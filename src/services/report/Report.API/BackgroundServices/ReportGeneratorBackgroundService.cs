using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Threading;
using System.Text;
using Report.API.Application;
using System.Threading.Channels;
using Report.API.RabbitMq.Configuration;
using BuildingBlocks.Domain;
using System.Text.Json;
using Report.API.Infrastructure.Excel;
using BuildingBlocks.Infrastructure.Serialization;
using Report.API.RabbitMq.Events;

namespace Report.API.BackgroundServices
{
    public class ReportGeneratorBackgroundService : BackgroundService
    {
        private readonly IWebHostEnvironment _env;
        private readonly ISerializer _serializer;
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        private readonly IServiceProvider _serviceProvider;
        private readonly RabbitMqConfiguration _rabbitMqConfiguration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _client;
        private readonly ILogger<ReportGeneratorBackgroundService> _logger;
        private IReportService _reportService;

        public ReportGeneratorBackgroundService(IServiceProvider serviceProvider,
                                                ConnectionFactory connectionFactory,
                                                RabbitMqConfiguration rabbitMqConfiguration,
                                                IHttpClientFactory httpClientFactory,
                                                IWebHostEnvironment env,
                                                ISerializer serializer,
                                                ILogger<ReportGeneratorBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _rabbitMqConfiguration = rabbitMqConfiguration;
            _connectionFactory = connectionFactory;
            _httpClientFactory = httpClientFactory;
            _client = _httpClientFactory.CreateClient("ContactApi");
            _env = env;
            _serializer = serializer;
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("background service started...");

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
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += Consumer_Received;
            _channel.BasicConsume(queue: _rabbitMqConfiguration.QueueName, autoAck: false, consumer: consumer);


            await Task.CompletedTask;
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            using var scope = _serviceProvider.CreateScope();
            _reportService = scope.ServiceProvider.GetRequiredService<IReportService>();

            var message = Encoding.UTF8.GetString(@event.Body.ToArray());
            RequestReportEvent requestReportEvent = _serializer.Deserialize<RequestReportEvent>(message);
            await _reportService.RequestReport(requestReportEvent.Id, requestReportEvent.RequestDate, default);
            try
            {
                string reportJson = await _client.GetStringAsync("/api/persons/GetReportData");
                List<ReportItemDto> reportData = _serializer.Deserialize<List<ReportItemDto>>(reportJson);
                var fileArray = ExcelHelper.GenerateExcel(reportData);
                string filePath = Path.Combine(_env.ContentRootPath, "Reports", Guid.NewGuid() + ".xlsx");
                File.WriteAllBytes(filePath, fileArray);
                await _reportService.SetReportPath(requestReportEvent.Id, filePath, default);
                _channel.BasicAck(@event.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                _channel.BasicNack(@event.DeliveryTag, false, false);
                _logger.LogError(ex.Message);
            }
        }
    }
}
