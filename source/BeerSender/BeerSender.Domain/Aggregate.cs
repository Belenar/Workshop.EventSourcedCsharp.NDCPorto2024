namespace BeerSender.Domain;

public abstract class Aggregate
{
    public void Apply(object @event) { }
}