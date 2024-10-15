namespace BeerSender.Domain.Boxes.CommandHandlers;

public class BoxSender(IEventStore eventStore)
    : CommandHandler<SendBox>()
{
    public override void Handle(SendBox command)
    {
        var boxAggregate = new Box(eventStore, command.BoxId);

        var success = true;
        if (!boxAggregate.IsClosed)
        {
            boxAggregate.AppendEvent(new FailedToSendBox(FailedToSendBox.Reason.BoxWasNotClosed));
            success = false;
        }

        if (boxAggregate.ShippingLabel is null)
        {
            boxAggregate.AppendEvent(new FailedToSendBox(FailedToSendBox.Reason.BoxHadNoLabel));
            success = false;
        }
        
        if(success)
        {
            boxAggregate.AppendEvent(new BoxSent());
        }
    }
}