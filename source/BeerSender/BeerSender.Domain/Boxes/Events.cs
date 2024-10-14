namespace BeerSender.Domain.Boxes;

public record BoxCreated(BoxCapacity BoxCapacity);

public record FailedToCreateBox(FailedToCreateBox.Reason FailReason)
{
    public enum Reason
    {
        BoxAlreadyCreated,
        InvalidCapacity
    }
}

public record BeerBottleAdded(BeerBottle BeerBottle);

public record FailedToAddBeerBottle(FailedToAddBeerBottle.Reason FailReason)
{
    public enum Reason
    {
        BoxWasFull
    }
}

public record ShippingLabelAdded(ShippingLabel ShippingLabel);

public record FailedToAddShippingLabel(FailedToAddShippingLabel.Reason FailReason)
{
    public enum Reason
    {
        LabelWasInvalid
    }
}

public record BoxClosed;

public record FailedToCloseBox(FailedToCloseBox.Reason FailReason)
{
    public enum Reason
    {
        BoxWasEmpty
    }
}


public record BoxSent;

public record FailedToSendBox(FailedToSendBox.Reason FailReason)
{
    public enum Reason
    {
        BoxWasNotClosed,
        BoxHadNoLabel
    }
}