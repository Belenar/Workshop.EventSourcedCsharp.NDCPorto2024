namespace BeerSender.Domain;

public class EventStream<TAggregate>(IEventStore eventStore, Guid aggregateId)
    where TAggregate : Aggregate, new()
{
    public int LastSequenceNumber { get; set; } = 0;

    public TAggregate GetAggregate()
    {
        var events = eventStore
            .GetEvents(aggregateId);

        TAggregate aggregate = new TAggregate();
        foreach (var @event in events)
        {
            aggregate.Apply((dynamic)@event.Payload);
            LastSequenceNumber = @event.SequenceNumber;
        }

        return aggregate;
    }

    public object Append(object @event)
    {
        LastSequenceNumber++;

        var storedEvent = new StoredEvent(
            aggregateId,
            LastSequenceNumber,
            DateTime.UtcNow,
            @event
        );
        eventStore.AppendEvent(storedEvent);

        return @event;
    }
}