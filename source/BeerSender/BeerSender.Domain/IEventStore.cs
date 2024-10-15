namespace BeerSender.Domain;

public interface IEventStore
{
    EventStream GetEventStream(Guid aggregateId);
    void AppendEvent(StoredEvent @event);
    void SaveChanges();
}