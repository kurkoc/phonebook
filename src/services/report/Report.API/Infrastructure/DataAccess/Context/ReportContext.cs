using Microsoft.EntityFrameworkCore;

namespace Report.API.Infrastructure.DataAccess.Context
{
    public class ReportContext : DbContext, IDataContext
    {
        public ReportContext(DbContextOptions<ReportContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
