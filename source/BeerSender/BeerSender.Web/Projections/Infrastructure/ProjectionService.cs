using BeerSender.Domain;
using BeerSender.Web.EventPersistence;
using Microsoft.EntityFrameworkCore;

namespace BeerSender.Web.Projections.Infrastructure;

public class ProjectionService<TProjection>(IServiceProvider serviceProvider) : BackgroundService
    where TProjection : class, Projection
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var checkpoint = await GetCheckPoint();

        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = serviceProvider.CreateScope();
            var eventContext = scope.ServiceProvider.GetRequiredService<EventContext>();
            var readContext = scope.ServiceProvider.GetRequiredService<ReadContext>();

            var transaction = await readContext.Database.BeginTransactionAsync(stoppingToken);

            var projection = scope.ServiceProvider.GetRequiredService<TProjection>();

            var events = await Get_batch(checkpoint, projection, eventContext);

            if (events.Any())
            {
                projection.Project(events.Select(e => new StoredEvent(
                    e.AggregateId,
                    e.SequenceNumber,
                    e.Timestamp,
                    e.PayLoad)));

                checkpoint = events.Last().RowVersion;
                await Write_checkpoint(readContext, checkpoint);
            }
            else
            {
                await Task.Delay(projection.WaitTime);
            }

            await transaction.CommitAsync(stoppingToken);
        }
    }

    private async Task Write_checkpoint(ReadContext readContext, ulong checkpoint)
    {
        var checkpointRecord = await readContext.ProjectionCheckpoints
            .FindAsync(typeof(TProjection).Name);
        checkpointRecord!.EventVersion = checkpoint;
        await readContext.SaveChangesAsync();
    }

    private async Task<ulong> GetCheckPoint()
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ReadContext>();

        var checkpoint = await context.ProjectionCheckpoints.FindAsync(
            typeof(TProjection).Name);

        if (checkpoint == null)
        {
            checkpoint = new ProjectionCheckpoint
            {
                ProjectionName = typeof(TProjection).Name,
                EventVersion = 0
            };
            context.ProjectionCheckpoints.Add(checkpoint);
            await context.SaveChangesAsync();
        }

        return checkpoint.EventVersion;
    }

    private async Task<IList<PersistedEvent>> Get_batch(
        ulong checkpoint,
        TProjection projection,
        EventContext eventContext)
    {
        var type_list = projection.RelevantEventTypes.Select(t => t.FullName).ToList();

        var batch = await eventContext.Events
            .Where(e => type_list.Contains(e.EventTypeName))
            .Where(e => e.RowVersion > checkpoint)
            .OrderBy(e => e.RowVersion)
            .Take(projection.BatchSize)
            .ToListAsync();

        return batch;
    }
}