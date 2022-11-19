using Contact.Application.Person;
using Contact.Infrastructure;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


string? connectionString = builder.Configuration.GetConnectionString("ContactConnectionString");
if (connectionString == null) throw new ArgumentNullException("");
builder.Services.AddDataAccess(connectionString);

builder.Services.AddScoped<IPersonService, PersonService>();

builder.Services.AddValidatorsFromAssemblyContaining<PersonSaveDtoValidator>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
