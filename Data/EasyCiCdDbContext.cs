using EasyCiCd.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyCiCd.Data;

/// <summary>
/// Entity Framework Core DbContext for EasyCiCd application
/// </summary>
public class EasyCiCdDbContext : DbContext
{
    public EasyCiCdDbContext(DbContextOptions<EasyCiCdDbContext> options) : base(options)
    {
    }

    public DbSet<Pipeline> Pipelines { get; set; } = null!;
    public DbSet<PipelineTask> PipelineTasks { get; set; } = null!;
    public DbSet<PipelineExecution> PipelineExecutions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure shadow properties for audit fields
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property<DateTime>("CreatedAt")
                    .HasDefaultValueSql("datetime('now')");

                modelBuilder.Entity(entityType.ClrType)
                    .Property<DateTime?>("UpdatedAt");

                modelBuilder.Entity(entityType.ClrType)
                    .Property<string>("CreatedBy")
                    .HasMaxLength(255);

                modelBuilder.Entity(entityType.ClrType)
                    .Property<string>("UpdatedBy")
                    .HasMaxLength(255);
            }
        }

        // Configure Pipeline entity
        modelBuilder.Entity<Pipeline>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.Description)
                .HasMaxLength(1000);

            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(50)
                .HasDefaultValue("draft");

            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(450); // ASP.NET Identity User ID length

            entity.HasMany(e => e.Tasks)
                .WithOne(t => t.Pipeline)
                .HasForeignKey(t => t.PipelineId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.Executions)
                .WithOne(ex => ex.Pipeline)
                .HasForeignKey(ex => ex.PipelineId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.Name);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.UserId);
        });

        // Configure PipelineTask entity
        modelBuilder.Entity<PipelineTask>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.Script)
                .IsRequired();

            entity.Property(e => e.Timeout)
                .HasDefaultValue(3600);

            entity.Property(e => e.Order)
                .HasDefaultValue(0);

            entity.Property(e => e.Dependencies)
                .HasMaxLength(2000);

            entity.HasIndex(e => e.PipelineId);
            entity.HasIndex(e => new { e.PipelineId, e.Order });
        });

        // Configure PipelineExecution entity
        modelBuilder.Entity<PipelineExecution>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(50)
                .HasDefaultValue("pending");

            entity.Property(e => e.StartedAt)
                .IsRequired()
                .HasDefaultValueSql("datetime('now')");

            entity.Property(e => e.Output)
                .HasMaxLength(50000);

            entity.HasIndex(e => e.PipelineId);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.StartedAt);
        });
    }

    public override int SaveChanges()
    {
        UpdateAuditFields();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditFields();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditFields()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is BaseEntity &&
                       (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            var entity = (BaseEntity)entry.Entity;

            if (entry.State == EntityState.Added)
            {
                entry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
                entry.Property("CreatedBy").CurrentValue = "system"; // TODO: Get from current user context
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
                entry.Property("UpdatedBy").CurrentValue = "system"; // TODO: Get from current user context
            }
        }
    }
}
