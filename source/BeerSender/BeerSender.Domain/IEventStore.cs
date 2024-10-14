namespace BeerSender.Domain;

public interface IEventStore
{
    List<StoredEvent> GetEvents(Guid aggregateId);
    void AppendEvent(StoredEvent @event);
    void SaveChanges();
}