using Contact.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contact.Infrastructure.DataAccess.Configurations
{
    public class PersonContactConfiguration : IEntityTypeConfiguration<PersonContact>
    {
        public void Configure(EntityTypeBuilder<PersonContact> builder)
        {
            builder.ToTable("PersonContact");

            builder.HasKey(x => x.Id);

            builder.Property(q=>q.Id).ValueGeneratedNever();

            builder.Property(q => q.TypeId)
                .IsRequired();

            builder.Property(q=>q.Value)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(q => q.Person)
                .WithMany(q => q.Contacts)
                .HasForeignKey(q => q.PersonId);


        }
    }
}
