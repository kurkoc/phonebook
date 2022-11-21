using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Report.API.Application;
using Report.API.Controllers;
using Report.API.RabbitMq.Events;
using Report.API.RabbitMq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Tests.Report
{
    public class ReportControllerTests
    {
        private readonly Mock<IRabbitMqProducer<RequestReportEvent>> _producer;
        private readonly Mock<IReportService> _reportService;
        private readonly ReportController _controller;
        private readonly Fixture _fixture;
        public ReportControllerTests()
        {
            _producer = new Mock<IRabbitMqProducer<RequestReportEvent>>();
            _reportService = new Mock<IReportService>();
            _controller = new ReportController(_reportService.Object, _producer.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Controller_GetAllReports_ShouldBeOkStatusCode()
        {
            var reports = _fixture.CreateMany<ReportListDto>().ToList();
            _reportService.Setup(q => q.GetAllReports(default))
                .ReturnsAsync(reports);

            var controllerResult = await _controller.GetAllReports(default);
            var objectResult = controllerResult as ObjectResult;
            var result = objectResult?.Value as List<ReportListDto>;

            Assert.Equal(200, objectResult?.StatusCode);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Controller_GetReportById_ShouldBeOkStatusCode()
        {
            var report = _fixture.Create<ReportListDto>();
            _reportService.Setup(q => q.GetReportById(It.IsAny<Guid>(), default))
                .ReturnsAsync(report);

            var controllerResult = await _controller.GetReportById(Guid.NewGuid(), default);
            var objectResult = controllerResult as ObjectResult;
            var result = objectResult?.Value as ReportListDto;

            Assert.Equal(200, objectResult?.StatusCode);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Controller_RequestReport_ShouldBeOkStatusCode()
        {
            RequestReportEvent @event = new RequestReportEvent();
            _producer.Setup(q => q.Publish(@event));

            var controllerResult = await _controller.RequestReport(default);
            var objectResult = controllerResult as OkResult;
            Assert.Equal(200, objectResult?.StatusCode);
        }
    }
}
