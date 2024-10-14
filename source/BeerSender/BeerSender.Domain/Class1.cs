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
        public BoxCapacity BoxCapacity { get; set; }

        public void Apply(BeerBottleAdded @event)
        {
            this.BeerBottles.Add(@event.BeerBottle);
        }

        public void Apply(BoxAdded @event)
        {
            BoxCapacity = @event.boxCapacity;
        }
    }

    public record AddBox(int DesiredCapacity);

    public record BoxAdded(BoxCapacity boxCapacity);

    public record BoxCapacity
        (int NumberOfSpots)
    {
        public static BoxCapacity Create(int desiredCapacity) 
            => desiredCapacity switch
                {
                    <= 6 => new BoxCapacity(6),
                    <= 12 => new BoxCapacity(12),
                    <= 24 => new BoxCapacity(24),
                    _ => throw new InvalidOperationException("Box capacity can't be bigger than 24")
                };

    }

    public abstract class Aggregate
    {
        public void Apply(object @event) {}
    }
    

}