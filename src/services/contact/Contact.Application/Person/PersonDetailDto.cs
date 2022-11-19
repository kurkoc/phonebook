namespace Contact.Application.Person
{
    public record PersonDetailDto : PersonListDto
    {
        public List<PersonContactDto> Contacts { get; set; }
    }

    public record PersonContactDto
    {
        public int ContactType { get; set; }
        public string ContactTypeName { get; set; }
        public string Value { get; set; }
    }
}
