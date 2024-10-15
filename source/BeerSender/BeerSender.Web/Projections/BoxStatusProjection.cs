using BeerSender.Domain;
using BeerSender.Domain.Boxes;
using BeerSender.Web.Projections.Infrastructure;

namespace BeerSender.Web.Projections
{
    public class BoxStatusProjection(ReadContext dbContext) : Projection
    {
        public List<Type> RelevantEventTypes =>
        [
            typeof(BoxCreated),
            typeof(BeerBottleAdded)
        ];

        public int BatchSize => 100;
        public int WaitTime => 1000;

        public void Project(IEnumerable<StoredEvent> events)
        {
            foreach (var @event in events)
            {
                var eventType = @event.Payload.GetType();

                if(eventType == typeof(BoxCreated)){
                    CreateBox(@event.AggregateId, (BoxCreated)@event.Payload);
                }
                else if (eventType == typeof(BeerBottleAdded))
                {
                    AddBottle(@event.AggregateId);
                }
                else
                {
                    throw new InvalidOperationException("Unknown event type");
                }
            }

            dbContext.SaveChanges();
        }

        private void CreateBox(Guid aggregateId, BoxCreated @event)
        {
            dbContext.BoxStatusses
                .Add(new BoxStatus
                {
                    BoxId = aggregateId, 
                    BottleCapacity = @event.BoxCapacity.NumberOfSpots
                });
        }

        private void AddBottle(Guid aggregateId)
        {
            var boxStatus = dbContext.BoxStatusses
                .Find(aggregateId);

            if (boxStatus is not null) 
                boxStatus.BottleCount++;
        }
    }
}
