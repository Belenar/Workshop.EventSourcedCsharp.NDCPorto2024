namespace BeerSender.Domain.Boxes;

//
public record BoxCreated(BoxCapacity? BoxCapacity);

public record FailedToCreateBox(FailedToCreateBox.Reason FailReason)
{
    public enum Reason
    {
        BoxAlreadyCreated,
        InvalidCapacity
    }
}


// AddBeerBottle
public record BeerBottleAdded
(
    BeerBottle BeerBottle
);

public record FailedToAddBeerBottle(FailedToAddBeerBottle.Reason FailReason)
{
    public enum Reason
    {
        BoxWasFull
    }
}