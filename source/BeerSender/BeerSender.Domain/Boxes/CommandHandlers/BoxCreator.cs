namespace BeerSender.Domain.Boxes.CommandHandlers;

public class BoxCreator(IEventStore eventStore)
    : CommandHandler<CreateBox>(eventStore)
{
    public override void Handle(CreateBox command)
    {
        var stream = GetStream<Box>(command.BoxId);
        var boxAggregate = stream.GetAggregate();

        if (boxAggregate.Capacity is not null)
        {
            stream.Append(new FailedToCreateBox(FailedToCreateBox.Reason.BoxAlreadyCreated));
            return;
        }

        try
        {
            var capacity = BoxCapacity.Create(command.DesiredCapacity);
            stream.Append(new BoxCreated(capacity));
        }
        catch (Exception)
        {
            stream.Append(new FailedToCreateBox(FailedToCreateBox.Reason.InvalidCapacity));
        }
    }
}