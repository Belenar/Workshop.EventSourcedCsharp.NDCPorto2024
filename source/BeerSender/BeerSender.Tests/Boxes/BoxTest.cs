using BeerSender.Domain.Boxes;

namespace BeerSender.Tests.Boxes;

public abstract class BoxTest<TCommand> : CommandHandlerTest<TCommand>
{
    // Commands
    protected CreateBox Create_box_for_capacity(int desiredNumberOfSpots)
    {
        return new CreateBox(boxId, desiredNumberOfSpots);
    }

    protected AddBeerBottle Add_beer_bottle_to_box(BeerBottle bottle)
    {
        return new AddBeerBottle(boxId, bottle);
    }

    protected AddShippingLabel Add_label_with_carrier_and_code(Carrier carrier, string trackingCode)
    {
        return new AddShippingLabel(boxId, new ShippingLabel(carrier, trackingCode));
    }

    protected CloseBox Close_box()
    {
        return new CloseBox(boxId);
    }

    protected SendBox Send_box()
    {
        return new SendBox(boxId);
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

    protected ShippingLabelAdded Shipping_label_added_with_carrier_and_code(Carrier carrier, string trackingCode)
    {
        return new ShippingLabelAdded(new ShippingLabel(carrier, trackingCode));
    }

    protected FailedToAddShippingLabel Label_was_invalid()
    {
        return new FailedToAddShippingLabel(FailedToAddShippingLabel.Reason.LabelWasInvalid);
    }

    protected BoxClosed Box_was_closed()
    {
        return new BoxClosed();
    }

    protected FailedToCloseBox Box_was_empty()
    {
        return new FailedToCloseBox(FailedToCloseBox.Reason.BoxWasEmpty);
    }

    protected BoxSent Box_was_sent()
    {
        return new BoxSent();
    }
    
    protected FailedToSendBox Box_was_not_closed()
    {
        return new FailedToSendBox(FailedToSendBox.Reason.BoxWasNotClosed);
    }

    protected FailedToSendBox Box_has_no_label()
    {
        return new FailedToSendBox(FailedToSendBox.Reason.BoxHadNoLabel);
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