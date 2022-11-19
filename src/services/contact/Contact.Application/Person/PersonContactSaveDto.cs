using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Application.Person
{
    public record PersonContactSaveDto
    {
        public Guid PersonId { get; set; }
        public int ContactType { get; set; }
        public string Value { get; set; }
    }
}
