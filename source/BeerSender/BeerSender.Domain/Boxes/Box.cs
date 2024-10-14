namespace BeerSender.Domain.Boxes;

public class Box : Aggregate
{
    public List<BeerBottle> BeerBottles { get; } = [];
    public BoxCapacity? Capacity { get; private set; }
    public ShippingLabel? ShippingLabel { get; private set; }

    public void Apply(BeerBottleAdded @event)
    {
        BeerBottles.Add(@event.BeerBottle);
    }

    public void Apply(BoxCreated @event)
    {
        Capacity = @event.BoxCapacity;

    }

    public void Apply(ShippingLabelAdded @event)
    {
        ShippingLabel = @event.ShippingLabel;
    }

    public bool IsFull()
    {
        return BeerBottles.Count >= Capacity?.NumberOfSpots;
    }
}

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

public record BeerBottle(

    string Brewery,
    string Name,
    double AlcoholPercentage,
    BeerType BeerType
);

public enum BeerType
{
    Ipa,
    Stout,
    Sour,
    Double,
    Triple, 
    Quadruple
}

public record ShippingLabel(Carrier Carrier, string TrackingCode)
{
    public bool IsValid()
    {
        return Carrier switch
        {
            Carrier.Ups => TrackingCode.StartsWith("ABC"),
            Carrier.FedEx => TrackingCode.StartsWith("DEF"),
            Carrier.Dhl => TrackingCode.StartsWith("GHI"),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

public enum Carrier
{
    Ups,
    FedEx,
    Dhl
}