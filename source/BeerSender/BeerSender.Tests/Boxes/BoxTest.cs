using BeerSender.Domain;

namespace BeerSender.Tests.Boxes;

public abstract class BoxTest<TCommand> : CommandHandlerTest<TCommand>
{
    // Commands
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

    protected BeerBottle gouden_carolus = new()
    {
        Brewery = "Gouden Carolus",
        Name = "Quadrupel Whisky Infused",
        AlcoholPercentage = 12.7,
        BeerType = BeerType.Stout
    };
}