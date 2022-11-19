using System.ComponentModel;

namespace Contact.Domain.Enums
{
    public enum ContactTypes
    {
        [Description("Telefon")]
        Phone,
        [Description("Email")]
        Email,
        [Description("Lokasyon")]
        Location
    }
}
