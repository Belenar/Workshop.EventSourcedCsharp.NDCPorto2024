using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

public class ReadContext : DbContext
{
    public ReadContext(DbContextOptions<ReadContext> options) : base(options)
    { }

    public DbSet<BoxStatus> BoxStatusses { get; set; }
    public DbSet<ProjectionCheckpoint> ProjectionCheckpoints { get; set; }
}

public class BoxStatus
{
    [Key]
    public Guid BoxId { get; set; }
    public int BottleCapacity { get; set; }
    public int BottleCount { get; set; }
}

public class ProjectionCheckpoint
{
    [Key]
    [MaxLength(256)]
    public required string ProjectionName { get; set; }
    public ulong EventVersion { get; set; }
}
