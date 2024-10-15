namespace BeerSender.Domain;

public record StoredEvent (
    Guid AggregateId,
    int SequenceNumber,
    DateTime Timestamp,
    object Payload
);