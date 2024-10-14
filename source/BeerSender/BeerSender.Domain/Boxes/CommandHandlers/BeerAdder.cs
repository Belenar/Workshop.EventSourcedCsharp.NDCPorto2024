namespace BeerSender.Domain.Boxes.CommandHandlers;

public class BeerAdder(IEventStore eventStore)
    : CommandHandler<AddBeerBottle>(eventStore)
{
    public override void Handle(AddBeerBottle command)
    {
        var stream = GetStream<Box>(command.BoxId);
        var boxAggregate = stream.GetAggregate();

        if (boxAggregate.IsFull())
        {
            stream.Append(new FailedToAddBeerBottle(FailedToAddBeerBottle.FailedReasonType.BoxWasFull));
        }

        stream.Append(new BeerBottleAdded(command.BeerBottle));
    }
}