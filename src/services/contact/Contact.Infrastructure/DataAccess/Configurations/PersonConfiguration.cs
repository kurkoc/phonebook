using Contact.Domain.AggregateRoot;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contact.Infrastructure.DataAccess.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Person");

            builder.HasKey(x => x.Id);

            builder.Property(q => q.Id).ValueGeneratedNever();

            builder.Property(q => q.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(q => q.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(q => q.Company)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
