namespace BeerSender.Domain;

public class Box : Aggregate
{
    public List<BeerBottle> BeerBottles { get; } = [];
    public BoxCapacity BoxCapacity { get; set; }

    public void Apply(BeerBottleAdded @event)
    {
        this.BeerBottles.Add(@event.BeerBottle);
    }

    public void Apply(BoxAdded @event)
    {
        BoxCapacity = @event.boxCapacity;
    }

    public bool IsFull()
    {
        return BeerBottles.Count >= BoxCapacity.NumberOfSpots;
    }
}