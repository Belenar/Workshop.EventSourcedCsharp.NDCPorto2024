namespace BeerSender.Domain;

public class EventStream(IEventStore eventStore, Guid aggregateId)
{
    public int LastSequenceNumber => StoredEvents.Count;
    public List<StoredEvent> StoredEvents { get; private set; } = [];

    public object Append(object @event)
    {
        var storedEvent = new StoredEvent(
            aggregateId,
            LastSequenceNumber + 1,
            DateTime.UtcNow,
            @event
        );
        eventStore.AppendEvent(storedEvent);

        return @event;
    }
}