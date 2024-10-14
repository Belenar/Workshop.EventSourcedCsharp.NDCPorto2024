namespace BeerSender.Domain.Boxes;

public record BeerBottleAdded
(
    BeerBottle BeerBottle
);

public record BoxAdded(BoxCapacity? BoxCapacity);

public record FailedToAddBeerBottle(FailedToAddBeerBottle.FailedReasonType FailedReason)
{
    public enum FailedReasonType
    {
        BoxWasFull
    }
}