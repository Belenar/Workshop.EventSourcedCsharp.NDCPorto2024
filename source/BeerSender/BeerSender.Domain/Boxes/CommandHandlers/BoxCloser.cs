namespace BeerSender.Domain.Boxes.CommandHandlers;

public class BoxCloser(IEventStore eventStore)
    : CommandHandler<CloseBox>(eventStore)
{
    public override void Handle(CloseBox command)
    {
        var stream = GetStream<Box>(command.BoxId);
        var boxAggregate = stream.GetAggregate();

        if (boxAggregate.BeerBottles.Any())
        {
            stream.Append(new BoxClosed());
        }
        else
        {
            stream.Append(new FailedToCloseBox(FailedToCloseBox.Reason.BoxWasEmpty));
        }
    }
}