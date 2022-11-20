﻿using BuildingBlocks.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using Report.API.Application;
using Report.API.Infrastructure.Excel;
using Report.API.RabbitMq;
using Report.API.RabbitMq.Events;
using System.Text;

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
        public async Task<IActionResult> GetAllReports(Guid id,CancellationToken cancellationToken)
        {
            var report = await _reportService.GetReportById(id,cancellationToken);
            return Ok(report);
        }

        [HttpPost]
        public async Task<IActionResult> RequestReport(CancellationToken cancellationToken)
        {
            RequestReportEvent @event = new RequestReportEvent();
            _producer.Publish(@event);
            return Ok();
        }

        [HttpGet("excel")]
        public IActionResult SaveExcel()
        {
            List<ReportItemDto> reportItems = new List<ReportItemDto>()
            {
                new ReportItemDto { LocationName = "Ankara", PersonCount = 4, PhoneCount = 5},
                new ReportItemDto { LocationName = "İstanbul", PersonCount = 2, PhoneCount = 2},
                new ReportItemDto { LocationName = "İzmir", PersonCount = 3, PhoneCount = 3},
            };
            ExcelHelper.GenerateExcel(reportItems);
            return Ok();
        }
    }
}
