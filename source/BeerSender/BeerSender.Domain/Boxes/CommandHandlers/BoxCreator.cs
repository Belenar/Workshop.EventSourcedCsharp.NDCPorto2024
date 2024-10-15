namespace BeerSender.Domain.Boxes.CommandHandlers;

public class BoxCreator(IEventStore eventStore)
    : CommandHandler<CreateBox>()
{
    public override void Handle(CreateBox command)
    {
        var boxAggregate = new Box(eventStore, command.BoxId);

        if (boxAggregate.Capacity is not null)
        {
            boxAggregate.AppendEvent(new FailedToCreateBox(FailedToCreateBox.Reason.BoxAlreadyCreated));
            return;
        }

        try
        {
            var capacity = BoxCapacity.Create(command.DesiredCapacity);
            boxAggregate.AppendEvent(new BoxCreated(capacity));
        }
        catch (Exception)
        {
            boxAggregate.AppendEvent(new FailedToCreateBox(FailedToCreateBox.Reason.InvalidCapacity));
        }
    }
}