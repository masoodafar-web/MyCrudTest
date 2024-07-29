namespace CrudTestMicroservice.Domain.Common
{
    public abstract class AggregateRoot : BaseEntity
    {
        protected AggregateRoot() : this((long.Parse(DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds.ToString().Replace(".",""))))
        {

        }

        protected AggregateRoot(long id)
        {
            Id = id;
        }

        private readonly List<BaseEvent> _domainEvents = new List<BaseEvent>();
        public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(BaseEvent eventItem)
        {
            _domainEvents.Add(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
