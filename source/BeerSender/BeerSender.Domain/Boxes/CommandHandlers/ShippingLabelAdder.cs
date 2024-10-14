namespace BeerSender.Domain.Boxes.CommandHandlers;

public class ShippingLabelAdder(IEventStore eventStore)
    : CommandHandler<AddShippingLabel>(eventStore)
{
    public override void Handle(AddShippingLabel command)
    {
        var stream = GetStream<Box>(command.BoxId);
        var boxAggregate = stream.GetAggregate();

        if (command.ShippingLabel.IsValid())
        {
            stream.Append(new ShippingLabelAdded(command.ShippingLabel));
        }
        else
        {
            stream.Append(new FailedToAddShippingLabel(FailedToAddShippingLabel.Reason.LabelWasInvalid));
        }
    }

}