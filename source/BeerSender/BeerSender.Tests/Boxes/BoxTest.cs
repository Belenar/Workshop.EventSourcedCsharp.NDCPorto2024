using BeerSender.Domain.Boxes;

namespace BeerSender.Tests.Boxes;

public abstract class BoxTest<TCommand> : CommandHandlerTest<TCommand>
{
    // Commands
    protected AddBeerBottle Add_beer_bottle_to_box(BeerBottle bottle)
    {
        return new AddBeerBottle(boxId, bottle);
    }
    
    protected CreateBox Create_box_for_capacity(int desiredNumberOfSpots)
    {
        return new CreateBox(boxId, desiredNumberOfSpots);
    }

    // Events
    protected BoxCreated Box_created_with_capacity(int capacity)
    {
        return new BoxCreated(new BoxCapacity(capacity));
    }

    protected FailedToCreateBox Box_was_already_created()
    {
        return new FailedToCreateBox(FailedToCreateBox.Reason.BoxAlreadyCreated);
    }

    protected FailedToCreateBox Invalid_desired_capacity()
    {
        return new FailedToCreateBox(FailedToCreateBox.Reason.InvalidCapacity);
    }

    protected BeerBottleAdded Beer_bottle_added(BeerBottle bottle)
    {
        return new BeerBottleAdded(bottle);
    }

    protected FailedToAddBeerBottle Box_was_full()
    {
        return new FailedToAddBeerBottle(FailedToAddBeerBottle.Reason.BoxWasFull);
    }

    // Content
    protected Guid boxId = Guid.NewGuid();

    protected BeerBottle gouden_carolus = new(
        "Gouden Carolus",
        "Quadrupel Whisky Infused",
        12.7,
        BeerType.Quadruple
    );

    protected BeerBottle carte_blanche = new(
        "Wolf",
        "Carte Blanche",
        8.5,
        BeerType.Triple
    );
}