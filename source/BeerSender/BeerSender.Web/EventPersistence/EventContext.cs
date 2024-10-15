using BeerSender.Domain;
using Microsoft.EntityFrameworkCore;

namespace BeerSender.Web.EventPersistence;

public class EventContext : DbContext
{
    public EventContext(DbContextOptions<EventContext> options) : base(options)
    { }

    public DbSet<StoredEvent> Events { get; set; }
}