namespace BeerSender.Domain.Boxes;

public record CreateBox(
    Guid BoxId,
    int DesiredCapacity
);

public record AddBeerBottle
(
    Guid BoxId,
    BeerBottle BeerBottle
);

public record AddShippingLabel
(
    Guid BoxId,
    ShippingLabel ShippingLabel
);