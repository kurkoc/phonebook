using BuildingBlocks.Domain;
using Contact.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Contact.Infrastructure.DataAccess.Context;
using Contact.Domain.Repositories;
using BuildingBlocks.Infrastructure.DataAccess;

namespace Contact.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDataAccess(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<IDataContext,ContactContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IUnitOfWork,UnitOfWork>();
            
            //TODO : assembly scan and decorate
            services.AddScoped<IPersonRepository, PersonRepository>();
        }
    }
}
