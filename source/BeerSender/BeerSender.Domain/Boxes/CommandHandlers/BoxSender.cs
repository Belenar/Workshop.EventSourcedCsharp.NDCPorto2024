namespace BeerSender.Domain.Boxes.CommandHandlers;

public class BoxSender(IEventStore eventStore)
    : CommandHandler<SendBox>(eventStore)
{
    public override void Handle(SendBox command)
    {
        var stream = GetStream<Box>(command.BoxId);
        var boxAggregate = stream.GetAggregate();

        var success = true;
        if (!boxAggregate.IsClosed)
        {
            stream.Append(new FailedToSendBox(FailedToSendBox.Reason.BoxWasNotClosed));
            success = false;
        }

        if (boxAggregate.ShippingLabel is null)
        {
            stream.Append(new FailedToSendBox(FailedToSendBox.Reason.BoxHadNoLabel));
            success = false;
        }
        
        if(success)
        {
            stream.Append(new BoxSent());
        }
    }
}