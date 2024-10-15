namespace BeerSender.Domain;

public abstract class Aggregate
{
    private EventStream EventStream { get; init; }

    public void Apply(object @event) { }

    public Aggregate(IEventStore eventStore, Guid aggregateId) 
    {
        EventStream = eventStore
            .GetEventStream(aggregateId);
        var events = EventStream
            .StoredEvents.Select(x => x.Payload);

        foreach (var @event in events)
        {
            Apply((dynamic)@event);
        }
    }

    public void AppendEvent(object @event)
    {
        EventStream.Append(@event);
    }
}