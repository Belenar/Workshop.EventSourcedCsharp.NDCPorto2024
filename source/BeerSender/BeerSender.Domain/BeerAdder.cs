namespace BeerSender.Domain;

public class BeerAdder(IEventStore eventStore)
    : CommandHandler<AddBeerBottle>(eventStore)
{
    public override IEnumerable<object> Handle(AddBeerBottle command)
    {
        var stream = GetStream<Box>(command.BoxId);
        var boxAggregate = stream.GetAggregate();

        if (boxAggregate.IsFull())
        {
            yield return stream.Append(new FailedToAddBeerBottle(FailedToAddBeerBottle.FailedReasonType.BoxWasFull));
        }

        yield return stream.Append(new BeerBottleAdded(command.BeerBottle));
    }
}