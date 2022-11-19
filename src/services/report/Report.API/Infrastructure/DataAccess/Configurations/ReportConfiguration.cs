using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ns = Report.API.Domain;
namespace Report.API.Infrastructure.DataAccess.Configurations
{
    public class ReportConfiguration : IEntityTypeConfiguration<ns.Report>
    {
        public void Configure(EntityTypeBuilder<ns.Report> builder)
        {
            builder.ToTable("Report");

            builder.HasKey(x => x.Id);

            builder.Property(q => q.Id).ValueGeneratedNever();

            builder.Property(q => q.RequestDate)
                .IsRequired();

            builder.Property(q => q.Status)
                .IsRequired();
        }
    }
}
