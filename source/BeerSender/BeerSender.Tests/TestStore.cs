using BeerSender.Domain;

namespace BeerSender.Tests;

public class TestStore : IEventStore
{
    public List<object> previousEvents = [];
    public List<object> newEvents = [];

    public void AppendEvent(StoredEvent @event)
    {
        newEvents.Add(@event.Payload);
    }

    public void SaveChanges()
    {
        throw new NotImplementedException();
    }

    public EventStream GetEventStream(Guid aggregateId)
    {
        var eventStream = new EventStream(this, aggregateId);
        foreach (var @event in previousEvents)
        {
            eventStream.Append(@event);
            newEvents = [];
        }
        return eventStream;
    }
}