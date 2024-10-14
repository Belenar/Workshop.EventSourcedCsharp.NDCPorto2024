using BeerSender.Domain;
using FluentAssertions;

namespace BeerSender.Tests
{
    public class BeerAdderTest : CommandHandlerTest<AddBeerBottle>
    {
        [Fact]
        public void WhenAddedToEmptyBox_ShouldAddBottle()
        {
            Given(
                new BoxAdded(new BoxCapacity(24))
            );

            When(
                new AddBeerBottle(Guid.NewGuid(), new BeerBottle
                    {Brewery = "Gouden Carolus", 
                        Name = "Quadrupel Whisky Infused", 
                        AlcoholPercentage = 12.7, 
                        BeerType = BeerType.Stout})
            );

            Then(
                new BeerBottleAdded(new BeerBottle
                    {Brewery = "Gouden Carolus", 
                        Name = "Quadrupel Whisky Infused", 
                        AlcoholPercentage = 12.7, 
                        BeerType = BeerType.Stout})
            );
        }

        protected override CommandHandler<AddBeerBottle> Handler 
            => new BeerAdder(eventStore);
    }

    public abstract class CommandHandlerTest<TCommand> : BoxTest
    {
        protected abstract CommandHandler<TCommand> Handler { get; }

        protected void When(TCommand command)
        {
            Handler.Handle(command);
        }
    }

    public class BoxTest : TestBase
    {
        // wrap Box events and commands for simpler tests
    }

    public class TestBase
    {
        protected TestStore eventStore = new();

        protected void Given(params object[] events)
        {
            eventStore.PreviousEvents.AddRange(events);
        }

        protected void Then(params object[] expectedEvents)
        {
            var actualEvents = eventStore.NewEvents.ToArray();

            actualEvents.Length.Should().Be(expectedEvents.Length);

            for (var i = 0; i < actualEvents.Length; i++)
            {
                actualEvents[i].GetType().Should().Be(expectedEvents[i].GetType());
                actualEvents[i].Should().BeEquivalentTo(expectedEvents[i]);
            }
        }
    }

    public class TestStore : IEventStore
    {
        public List<object> PreviousEvents = new();
        public List<object> NewEvents = new();
        public Guid AggregateId = Guid.NewGuid();

        public List<StoredEvent> GetEvents(Guid aggregateId)
        {
            return PreviousEvents.Select((e,i) => 
                new StoredEvent(AggregateId, i, DateTime.UtcNow, e))
                .ToList();
        }

        public void AppendEvent(StoredEvent @event)
        {
            NewEvents.Add(@event.Payload);
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}