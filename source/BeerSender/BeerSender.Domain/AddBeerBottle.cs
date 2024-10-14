namespace BeerSender.Domain;

public record AddBeerBottle
(
    Guid BoxId,
    BeerBottle BeerBottle
);