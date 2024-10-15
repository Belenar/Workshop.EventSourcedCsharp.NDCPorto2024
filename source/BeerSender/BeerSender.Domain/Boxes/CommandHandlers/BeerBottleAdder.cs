namespace BeerSender.Domain.Boxes.CommandHandlers;

public class BeerBottleAdder(IEventStore eventStore)
    : CommandHandler<AddBeerBottle>
{
    public override void Handle(AddBeerBottle command)
    {
        var boxAggregate = new Box(eventStore, command.BoxId);

        if (boxAggregate.IsFull)
        {
            boxAggregate.AppendEvent(new FailedToAddBeerBottle(FailedToAddBeerBottle.Reason.BoxWasFull));
        }
        else
        {
            boxAggregate.AppendEvent(new BeerBottleAdded(command.BeerBottle));
        }
    }
}