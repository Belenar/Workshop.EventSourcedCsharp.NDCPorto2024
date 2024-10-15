using BeerSender.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeerSender.Web.EventPersistence;

public class EventContext : DbContext
{
    public EventContext(DbContextOptions<EventContext> options) : base(options)
    { }

    public DbSet<PersistedEvent> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EventMapping());
    }
}

public record PersistedEvent(
    Guid AggregateId,
    int SequenceNumber,
    DateTime Timestamp,
    string EventTypeName,
    string EventBody
);

class EventMapping : IEntityTypeConfiguration<PersistedEvent>
{
    public void Configure(EntityTypeBuilder<PersistedEvent> builder)
    {
        builder.HasKey(e => new{e.AggregateId, e.SequenceNumber});
        builder.Property(e => e.AggregateId).IsRequired();
        builder.Property(e => e.SequenceNumber).IsRequired();

        builder.Property(e => e.EventTypeName)
            .HasColumnType("VARCHAR")
            .HasMaxLength(256);
        builder.Property(e => e.EventBody)
            .HasColumnType("NVARCHAR(MAX)");
    }
}