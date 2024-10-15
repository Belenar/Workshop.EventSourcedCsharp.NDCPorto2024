using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BeerSender.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

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

public class PersistedEvent
{
    public Guid AggregateId { get; set; }

    public int SequenceNumber { get; set; }
    public DateTime Timestamp { get; set; }
    public string EventTypeName { get; set; }
    public string EventBody { get; set; }

    [Timestamp]
    public ulong RowVersion { get; set; }

    private object? _payload;
    [NotMapped]
    public object PayLoad
    {
        get
        {
            if (_payload is null)
            {
                _payload = JsonSerializer.Deserialize(EventBody, Type.GetType($"{EventTypeName}, {typeof(Aggregate).Assembly.FullName}"));
            }

            return _payload;
        }
        set
        {
            _payload = value;
            EventTypeName = value.GetType().FullName;
            EventBody = JsonSerializer.Serialize(value);
        }
    }
};

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