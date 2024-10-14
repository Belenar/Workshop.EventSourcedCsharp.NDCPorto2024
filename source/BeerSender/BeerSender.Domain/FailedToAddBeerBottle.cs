namespace BeerSender.Domain;

public record FailedToAddBeerBottle(FailedToAddBeerBottle.FailedReasonType FailedReason)
{
    public enum FailedReasonType
    {
        BoxWasFull
    }
}