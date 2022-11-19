namespace Contact.Application.Person
{
    public record PersonSaveDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
    }
}
