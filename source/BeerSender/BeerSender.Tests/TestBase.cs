using FluentAssertions;

namespace BeerSender.Tests;

public class TestBase
{
    protected TestStore eventStore = new();

    protected void Given(params object[] events)
    {
        eventStore.previousEvents.AddRange(events);
    }

    protected void Then(params object[] expectedEvents)
    {
        var actualEvents = eventStore.newEvents.ToArray();

        actualEvents.Length.Should().Be(expectedEvents.Length);

        for (var i = 0; i < actualEvents.Length; i++)
        {
            actualEvents[i].GetType().Should().Be(expectedEvents[i].GetType());
            actualEvents[i].Should().BeEquivalentTo(expectedEvents[i]);
        }
    }
}