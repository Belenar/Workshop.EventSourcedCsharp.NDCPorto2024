using BeerSender.Domain;
using BeerSender.Domain.Boxes;
using BeerSender.Domain.Boxes.CommandHandlers;

namespace BeerSender.Tests.Boxes;

public class CloseBoxTest : BoxTest<CloseBox>
{
    [Fact]
    public void WhenBoxIsNotEmpty_ShouldSucceed()
    {
        Given(
            Box_created_with_capacity(24),
            Beer_bottle_added(gouden_carolus)
        );

        When(
            Close_box()
        );

        Then(
            Box_was_closed()
        );
    }

    [Fact]
    public void WhenBoxIsEmpty_ShouldFail()
    {
        Given(
            Box_created_with_capacity(24)
        );

        When(
            Close_box()
        );

        Then(
            Box_was_empty()
        );
    }

    protected override CommandHandler<CloseBox> Handler
        => new BoxCloser(eventStore);
}