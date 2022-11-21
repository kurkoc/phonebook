using BuildingBlocks.Domain;
using BuildingBlocks.Infrastructure.DataAccess;
using BuildingBlocks.Infrastructure.Serialization;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using Report.API.Application;
using Report.API.BackgroundServices;
using Report.API.Infrastructure.DataAccess.Context;
using Report.API.Infrastructure.DataAccess.Seeder;
using Report.API.RabbitMq;
using Report.API.RabbitMq.Configuration;
using Report.API.RabbitMq.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<ISerializer, Serializer>();


string? connectionString = builder.Configuration.GetConnectionString("ReportConnectionString");
if (connectionString == null) throw new ArgumentNullException("");
builder.Services.AddDbContext<IDataContext, ReportContext>(options =>
{
    options.UseNpgsql(connectionString);
});
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHostedService<ReportGeneratorBackgroundService>();

RabbitMqConfiguration rabbitMqConfiguration = new();
builder.Configuration.Bind("RabbitMq", rabbitMqConfiguration);
builder.Services.AddSingleton(rabbitMqConfiguration);
builder.Services.AddSingleton<ConnectionFactory>(provider =>
{
    return new ConnectionFactory() { HostName = rabbitMqConfiguration.Url,Port = rabbitMqConfiguration.Port, DispatchConsumersAsync = true };
});
builder.Services.AddSingleton<IRabbitMqProducer<RequestReportEvent>, RequestReportProducer>();

builder.Services.AddHttpClient("ContactApi", client =>
{
    string baseUrl = builder.Configuration["ContactApi:BaseUrl"];
    client.BaseAddress = new Uri(baseUrl);
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Seed();
app.Run();
