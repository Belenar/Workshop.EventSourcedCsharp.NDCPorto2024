using BeerSender.Domain.Boxes;

namespace BeerSender.Tests.Boxes;

public abstract class BoxTest<TCommand> : CommandHandlerTest<TCommand>
{
    // AddBeerBottle
    protected AddBeerBottle Add_beer_bottle_to_box(BeerBottle bottle)
    {
        return new AddBeerBottle(boxId, bottle);
    }

    // Events
    protected BoxAdded Box_added_with_capacity(int capacity)
    {
        return new BoxAdded(new BoxCapacity(24));
    }

    protected BeerBottleAdded Beer_bottle_added(BeerBottle bottle)
    {
        return new BeerBottleAdded(bottle);
    }

    // Content
    protected Guid boxId = Guid.NewGuid();

    protected BeerBottle gouden_carolus = new (
        "Gouden Carolus",
        "Quadrupel Whisky Infused",
        12.7,
        BeerType.Stout
    );
}