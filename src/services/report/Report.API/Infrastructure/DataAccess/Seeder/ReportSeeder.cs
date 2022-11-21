using Microsoft.EntityFrameworkCore;
using Report.API.Infrastructure.DataAccess.Context;
using ns = Report.API.Domain;
namespace Report.API.Infrastructure.DataAccess.Seeder
{
    public static class ReportSeeder
    {
        public static void Seed(this IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<ReportContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (context.Set<ns.Report>().Count() == 0)
            {
                context.Set<ns.Report>().AddRange(
                        ns.Report.Create(Guid.Parse("f8b754ef-63bf-48c2-ab87-8c61acbfd6dc"),DateTime.UtcNow),
                        ns.Report.Create(Guid.Parse("84740a8b-1e09-483a-a8c9-a2995bcff444"),DateTime.UtcNow),
                        ns.Report.Create(Guid.Parse("c0452d3a-f7e6-4d15-8593-06d27953db6d"),DateTime.UtcNow),
                        ns.Report.Create(Guid.Parse("88e46aea-3eba-4efa-8169-15a553cf3781"),DateTime.UtcNow)
                    );
                context.SaveChanges();
            }
        }
    }
}
