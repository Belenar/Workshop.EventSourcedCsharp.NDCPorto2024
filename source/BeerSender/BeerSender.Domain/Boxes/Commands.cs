namespace BeerSender.Domain.Boxes;

public record AddBox(
    Guid BoxId,
    int DesiredCapacity
);

public record AddBeerBottle
(
    Guid BoxId,
    BeerBottle BeerBottle
);