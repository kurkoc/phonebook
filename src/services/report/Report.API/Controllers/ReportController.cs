using Microsoft.AspNetCore.Mvc;
using Report.API.Application;
using Report.API.RabbitMq;
using Report.API.RabbitMq.Events;

namespace Report.API.Controllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IRabbitMqProducer<RequestReportEvent> _producer;

        public ReportController(IReportService reportService, IRabbitMqProducer<RequestReportEvent> producer)
        {
            _reportService = reportService;
            _producer = producer;
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("it works! hello from reports service");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReports(CancellationToken cancellationToken)
        {
            var reports = await _reportService.GetAllReports(cancellationToken);
            return Ok(reports);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReportById(Guid id, CancellationToken cancellationToken)
        {
            var report = await _reportService.GetReportById(id, cancellationToken);
            return Ok(report);
        }

        [HttpPost]
        public async Task<IActionResult> RequestReport(CancellationToken cancellationToken)
        {
            RequestReportEvent @event = new RequestReportEvent();
            _producer.Publish(@event);
            await Task.CompletedTask;
            return Ok();
        }
    }
}
