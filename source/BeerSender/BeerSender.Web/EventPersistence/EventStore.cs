using BeerSender.Domain;

namespace BeerSender.Web.EventPersistence;

public class EventStore(EventContext dbContext) : IEventStore
{
    public List<StoredEvent> GetEvents(Guid aggregateId)
    {
        return dbContext.Events
            .Where(e => e.AggregateId == aggregateId)
            .OrderBy(e => e.SequenceNumber)
            .Select(e => new StoredEvent(
                e.AggregateId, 
                e.SequenceNumber, 
                e.Timestamp, 
                e.PayLoad))
            .ToList();
    }


    public void AppendEvent(StoredEvent @event)
    {
        dbContext.Events.Add(new PersistedEvent{
            AggregateId = @event.AggregateId,
            SequenceNumber = @event.SequenceNumber,
            Timestamp = @event.Timestamp,
            PayLoad = @event.Payload
        });
    }

    public void SaveChanges()
    {
        dbContext.SaveChanges();
    }
}