namespace BuildingBlocks.Domain
{
    public class BaseDomainEvent : IDomainEvent
    {
        public Guid Id { get; }

        public DateTime CreatedDate { get; }

        public BaseDomainEvent()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.UtcNow;
        }
    }
}
