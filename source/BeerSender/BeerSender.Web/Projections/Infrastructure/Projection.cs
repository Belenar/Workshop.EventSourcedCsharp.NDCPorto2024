using BeerSender.Domain;

namespace BeerSender.Web.Projections.Infrastructure;

public interface Projection
{
    List<Type> RelevantEventTypes { get; }
    int BatchSize { get; }
    int WaitTime { get; }
    void Project(IEnumerable<StoredEvent> events);
}