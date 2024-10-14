namespace BeerSender.Domain.Boxes;

public class Box : Aggregate
{
    public List<BeerBottle> BeerBottles { get; } = [];
    public BoxCapacity? Capacity { get; private set; }

    public void Apply(BeerBottleAdded @event)
    {
        BeerBottles.Add(@event.BeerBottle);
    }

    public void Apply(BoxCreated @event)
    {
        Capacity = @event.BoxCapacity;
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