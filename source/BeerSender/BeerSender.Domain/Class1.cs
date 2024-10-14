using System.Text;

namespace BeerSender.Domain
{
    public class BeerBottle
    {
        public string Name { get; set; }
        public double AlcoholPercentage { get; set; }
        public BeerType BeerType { get; set; }
        public string Brewery { get; set; }
    }

    public enum BeerType
    {
        Ipa,
        Stout,
        Sour
    }

    public record AddBeerBottle
    (
        Guid BoxId,
        BeerBottle BeerBottle
    );

    public record BeerBottleAdded
    (
        BeerBottle BeerBottle
    );

    public record FailedToAddBeerBottle(FailedToAddBeerBottle.FailedReasonType FailedReason)
    {
        public enum FailedReasonType
        {
            BoxWasFull
        }
    }


    public class Box : Aggregate
    {
        public List<BeerBottle> BeerBottles { get; } = [];
        public BoxCapacity BoxCapacity { get; set; }

        public void Apply(BeerBottleAdded @event)
        {
            this.BeerBottles.Add(@event.BeerBottle);
        }

        public void Apply(BoxAdded @event)
        {
            BoxCapacity = @event.boxCapacity;
        }

        public bool IsFull()
        {
            return BeerBottles.Count >= BoxCapacity.NumberOfSpots;
        }
    }

    public record AddBox(int DesiredCapacity);

    public record BoxAdded(BoxCapacity boxCapacity);

    public record BoxCapacity
        (int NumberOfSpots)
    {
        public static BoxCapacity Create(int desiredCapacity)
            => desiredCapacity switch
            {
                <= 6 => new BoxCapacity(6),
                <= 12 => new BoxCapacity(12),
                <= 24 => new BoxCapacity(24),
                _ => throw new InvalidOperationException("Box capacity can't be bigger than 24")
            };

    }

    public abstract class Aggregate
    {
        public void Apply(object @event) { }
    }

    public class CommandHandler<TCommand>(IEventStore EventStore)
    {
        protected EventStream<TAggregate> GetStream<TAggregate>(Guid aggregateId)
            where TAggregate : Aggregate, new()
        {
            return new EventStream<TAggregate>(EventStore, aggregateId);
        }
    }

    public interface IEventStore
    {
        List<object> GetEvents(Guid aggregateId);
        void SaveChanges();
    }

    public class EventStream<TAggregate>(IEventStore eventStore, Guid aggregateId)
        where TAggregate : Aggregate, new()
    {
        public TAggregate GetAggregate()
        {
            var events = eventStore.GetEvents(aggregateId);
            TAggregate aggregate = new TAggregate();
            foreach (var @event in events)
            {
                aggregate.Apply((dynamic)@event);
            }

            return aggregate;
        }

        public object Append(object @event)
        {
            // TODO: append to the store
            return @event;
        }
    }

    public class BeerAdder(IEventStore EventStore)
        : CommandHandler<AddBeerBottle>(EventStore)
    {
        public IEnumerable<object> Handle(AddBeerBottle command)
        {
            var stream = GetStream<Box>(command.BoxId);
            var boxAggregate = stream.GetAggregate();

            if (boxAggregate.IsFull())
            {
                yield return stream.Append(new FailedToAddBeerBottle(FailedToAddBeerBottle.FailedReasonType.BoxWasFull));
            }

            yield return stream.Append(new BeerBottleAdded(command.BeerBottle));
        }
    }
}