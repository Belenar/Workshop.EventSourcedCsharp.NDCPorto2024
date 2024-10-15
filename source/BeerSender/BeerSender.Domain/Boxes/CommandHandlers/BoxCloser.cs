namespace BeerSender.Domain.Boxes.CommandHandlers;

public class BoxCloser(IEventStore eventStore)
    : CommandHandler<CloseBox>()
{
    public override void Handle(CloseBox command)
    {
        var boxAggregate = new Box(eventStore, command.BoxId);

        if (boxAggregate.BeerBottles.Any())
        {
            boxAggregate.AppendEvent(new BoxClosed());
        }
        else
        {
            boxAggregate.AppendEvent(new FailedToCloseBox(FailedToCloseBox.Reason.BoxWasEmpty));
        }
    }
}