using System.Text.Json;
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
                Deserialize(e.EventTypeName, e.EventBody)))
            .ToList();
    }

    private static object Deserialize(string eventTypeName, string eventBody)
    {
        return JsonSerializer.Deserialize(eventBody, Type.GetType($"{eventTypeName}, {typeof(Aggregate).Assembly.FullName}"));
    }

    public void AppendEvent(StoredEvent @event)
    {
        dbContext.Events.Add(new PersistedEvent(
            @event.AggregateId,
            @event.SequenceNumber,
            @event.Timestamp,
            @event.Payload.GetType().FullName,
            JsonSerializer.Serialize(@event.Payload)));
    }

    public void SaveChanges()
    {
        dbContext.SaveChanges();
    }
}