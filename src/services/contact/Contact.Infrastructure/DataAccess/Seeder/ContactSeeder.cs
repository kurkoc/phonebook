using Contact.Domain.AggregateRoot;
using Contact.Domain.Entities;
using Contact.Infrastructure.DataAccess.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ns = Contact.Domain.AggregateRoot;
namespace Contact.Infrastructure.DataAccess.Seeder
{
    public static class ContactSeeder
    {
        public static void Seed(this IApplicationBuilder app)
        {
            ns.Person[] personlist = new[]
             {
                    ns.Person.Create(Guid.Parse("008c3bc1-ab17-4c30-912e-7c24d2dfc440"), "Jane", "Doe", "Doe Corp."),
                    ns.Person.Create(Guid.Parse("28baa018-1094-4af1-beba-a709eb1527ef"), "Joe", "Doe", "Doe Corp."),
                    ns.Person.Create(Guid.Parse("cd8109d6-9b31-4f12-a4b5-c0ea499d95f6"), "Max", "Doe", "Doe Corp."),
                    ns.Person.Create(Guid.Parse("8f4b2305-b24f-48b3-85bf-026f04d7e61b"), "Kevin", "Doe", "Doe Corp."),
                    ns.Person.Create(Guid.Parse("776ffb5d-7b34-43ea-aef7-6355fbe95edf"), "Larry", "Doe", "Doe Corp.")
            };

            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<ContactContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }


            if (context.Set<ns.Person>().Count() == 0)
            {
                context.Set<ns.Person>().AddRange(personlist);
            }

            if (context.Set<PersonContact>().Count() == 0)
            {
                context.Set<PersonContact>().AddRange(
                    PersonContact.Create(Guid.Parse("cf63f0e0-e08a-440a-a6a9-93e3f3682359"), personlist[0], Domain.Enums.ContactTypes.Phone, "5067282291"),
                    PersonContact.Create(Guid.Parse("0c18f2f6-efee-441a-bddb-82957bf2fe44"), personlist[0], Domain.Enums.ContactTypes.Email, "jane@doe.com"),
                    PersonContact.Create(Guid.Parse("a465768d-642a-4097-80eb-406ea6bc067f"), personlist[0], Domain.Enums.ContactTypes.Location, "Ankara"),
                    PersonContact.Create(Guid.Parse("9d99193e-4f38-4a7a-855f-080a74676a5a"), personlist[1], Domain.Enums.ContactTypes.Location, "İstanbul"),
                    PersonContact.Create(Guid.Parse("96b221e6-c898-4373-a2d2-cb3115b7820f"), personlist[2], Domain.Enums.ContactTypes.Location, "İzmir")
                    );
            }

            context.SaveChanges();

        }
    }
}
