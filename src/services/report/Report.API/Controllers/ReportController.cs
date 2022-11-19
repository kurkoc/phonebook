using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Report.API.Application;

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

        [HttpGet]
        [Route("test")]
        public IActionResult Test()
        {
            return Ok("it works! hello from reports service");
        }

        [HttpPost]
        public async Task<IActionResult> RequestReport(CancellationToken cancellationToken)
        {
            await _reportService.RequestReport(cancellationToken);
            return Ok();
        }
    }
}
