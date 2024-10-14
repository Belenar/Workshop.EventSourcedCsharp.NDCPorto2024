using BeerSender.Domain;

namespace BeerSender.Tests;

public class TestStore : IEventStore
{
    public List<object> previousEvents = new();
    public List<object> newEvents = new();

    public List<StoredEvent> GetEvents(Guid aggregateId)
    {
        return previousEvents.Select((e,i) => 
                new StoredEvent(aggregateId, i, DateTime.UtcNow, e))
            .ToList();
    }

    public void AppendEvent(StoredEvent @event)
    {
        newEvents.Add(@event.Payload);
    }

    public void SaveChanges()
    {
        throw new NotImplementedException();
    }
}