using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using Report.API.Application;
using System.Text;

namespace Report.API.Controllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("it works! hello from reports service");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRequests(CancellationToken cancellationToken)
        {
            var reports = await _reportService.GetAllReports(cancellationToken);
            return Ok(reports);
        }

        [HttpPost]
        public async Task<IActionResult> RequestReport(CancellationToken cancellationToken)
        {
            await _reportService.RequestReport(cancellationToken);
            return Ok();
        }
    }
}
