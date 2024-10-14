namespace BeerSender.Domain;

public abstract class CommandHandler<TCommand>(IEventStore eventStore)
{
    protected EventStream<TAggregate> GetStream<TAggregate>(Guid aggregateId)
        where TAggregate : Aggregate, new()
    {
        return new EventStream<TAggregate>(eventStore, aggregateId);
    }

    public abstract void Handle(TCommand command);
}