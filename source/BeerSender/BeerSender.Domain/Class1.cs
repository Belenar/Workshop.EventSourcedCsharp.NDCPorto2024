namespace BeerSender.Domain
{
    public class BeerBottle
    {
        public string Name { get; set; }
        public double AlcoholPercentage { get; set; }
        public BeerType BeerType { get; set; }
        public string Brewery { get; set; }
    }
    
    public enum BeerType
    {
        Ipa,
        Stout,
        Sour
    }

    public record AddBeerBottle
    (
        Guid BoxId,
        BeerBottle BeerBottle
    );
    
    public record BeerBottleAdded
    (
        BeerBottle BeerBottle
    );

    public record FailedToAddBeerBottle(FailedToAddBeerBottle.FailedReasonType FailedReason)
    {
        public enum FailedReasonType
        {
            BoxWasFull
        }
    }


    public class Box : Aggregate
    {
        public List<BeerBottle> BeerBottles { get; } = new();

        public void Apply(BeerBottleAdded @event)
        {
            this.BeerBottles.Add(@event.BeerBottle);
        }
    }

    public record AddBox(int DesiredCapacity);

    public abstract class Aggregate
    {
        public void Apply(object @event) {}
    }
    

}