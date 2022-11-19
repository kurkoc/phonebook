using BuildingBlocks.Domain;
using BuildingBlocks.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Report.API.Application;
using Report.API.Infrastructure.DataAccess.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

string? connectionString = builder.Configuration.GetConnectionString("ReportConnectionString");
if (connectionString == null) throw new ArgumentNullException("");
builder.Services.AddDbContext<IDataContext, ReportContext>(options =>
{
    options.UseNpgsql(connectionString);
});
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

builder.Services.AddScoped<IReportService,ReportService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
