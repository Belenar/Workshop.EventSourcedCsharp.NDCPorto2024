namespace BeerSender.Domain
{
    public class BeerBottle
    {
        public string Name { get; set; }
        public double Percentage { get; set; }
        public BeerType BeerType { get; set; }
        public string Brewery { get; set; }
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

    public enum BeerType
    {
        Ipa,
        Stout,
        Sour
    }
}