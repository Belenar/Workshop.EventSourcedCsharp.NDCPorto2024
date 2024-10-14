using BeerSender.Domain;
using FluentAssertions;

namespace BeerSender.Tests;

public abstract class CommandHandlerTest<TCommand>
{
    protected abstract CommandHandler<TCommand> Handler { get; }

    protected TestStore eventStore = new();

    protected void Given(params object[] events)
    {
        eventStore.previousEvents.AddRange(events);
    }

    protected void When(TCommand command)
    {
        Handler.Handle(command);
    }

    protected void Then(params object[] expectedEvents)
    {
        var actualEvents = eventStore.newEvents.ToArray();

        actualEvents.Length.Should().Be(expectedEvents.Length);

        for (var i = 0; i < actualEvents.Length; i++)
        {
            actualEvents[i].Should().BeOfType(expectedEvents[i].GetType());
            try
            {
                actualEvents[i].Should().BeEquivalentTo(expectedEvents[i]);
            }
            catch (InvalidOperationException e)
            {
                // Empty event with matching type is OK, swallow exception
                if (!e.Message.StartsWith("No members were found for comparison."))
                    throw;
            }
        }
    }
}