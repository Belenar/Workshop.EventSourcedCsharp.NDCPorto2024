namespace BeerSender.Domain.Boxes.CommandHandlers;

public class ShippingLabelAdder(IEventStore eventStore)
    : CommandHandler<AddShippingLabel>()
{
    public override void Handle(AddShippingLabel command)
    {
        var boxAggregate = new Box(eventStore, command.BoxId);

        if (command.ShippingLabel.IsValid())
        {
            boxAggregate.AppendEvent(new ShippingLabelAdded(command.ShippingLabel));
        }
        else
        {
            boxAggregate.AppendEvent(new FailedToAddShippingLabel(FailedToAddShippingLabel.Reason.LabelWasInvalid));
        }
    }

}