using BeerSender.Domain;

namespace BeerSender.Web.EventPersistence;

public class EventStore(EventContext dbContext) : IEventStore
{
    public List<StoredEvent> GetEvents(Guid aggregateId)
    {
        return dbContext.Events
            .Where(e => e.AggregateId == aggregateId)
            .OrderBy(e => e.SequenceNumber)
            .ToList();
    }

    public void AppendEvent(StoredEvent @event)
    {
        dbContext.Events.Add(@event);
    }

    public void SaveChanges()
    {
        dbContext.SaveChanges();
    }
}