using BeerSender.Domain;
using BeerSender.Domain.Boxes;
using BeerSender.Domain.Boxes.CommandHandlers;

namespace BeerSender.Tests.Boxes;

public class AddBeerBottleTest : BoxTest<AddBeerBottle>
{
    [Fact]
    public void WhenAddedToEmptyBox_ShouldAddBottle()
    {
        Given(
            Box_created_with_capacity(24)
        );

        When(
            Add_beer_bottle_to_box(gouden_carolus)
        );

        Then(
            Beer_bottle_added(gouden_carolus)
        );
    }

    [Fact]
    public void WhenAddedToBoxWithSpace_ShouldAddBottle()
    {
        Given(
            Box_created_with_capacity(2),
            Beer_bottle_added(gouden_carolus)
        );

        When(
            Add_beer_bottle_to_box(carte_blanche)
        );

        Then(
            Beer_bottle_added(carte_blanche)
        );
    }

    [Fact]
    public void WhenAddedToFullBox_ShouldFail()
    {
        Given(
            Box_created_with_capacity(1),
            Beer_bottle_added(gouden_carolus)
        );

        When(
            Add_beer_bottle_to_box(carte_blanche)
        );

        Then(
            Box_was_full()
        );
    }

    protected override CommandHandler<AddBeerBottle> Handler
        => new BeerBottleAdder(eventStore);
}