namespace BeerSender.Domain;

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