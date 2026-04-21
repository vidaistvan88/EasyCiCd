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
    public DbSet<CiTask> Tasks { get; set; } = null!;
    public DbSet<Execution> Executions { get; set; } = null!;
    public DbSet<TaskExecution> TaskExecutions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Pipeline entity
        modelBuilder.Entity<Pipeline>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);
            
            entity.Property(e => e.Description)
                .HasMaxLength(1000);
            
            entity.Property(e => e.Repository)
                .IsRequired()
                .HasMaxLength(500);
            
            entity.Property(e => e.Branch)
                .IsRequired()
                .HasMaxLength(255);
            
            entity.Property(e => e.TriggerEvent)
                .IsRequired()
                .HasMaxLength(50);
            
            entity.Property(e => e.CreatedBy)
                .IsRequired()
                .HasMaxLength(255);
            
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255);
            
            entity.HasMany(e => e.Tasks)
                .WithOne(t => t.Pipeline)
                .HasForeignKey(t => t.PipelineId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasMany(e => e.Executions)
                .WithOne(ex => ex.Pipeline)
                .HasForeignKey(ex => ex.PipelineId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.Name);
            entity.HasIndex(e => e.IsActive);
        });

        // Configure CiTask entity
        modelBuilder.Entity<CiTask>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);
            
            entity.Property(e => e.Description)
                .HasMaxLength(1000);
            
            entity.Property(e => e.TaskType)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(e => e.Command)
                .HasMaxLength(5000);
            
            entity.Property(e => e.CreatedBy)
                .IsRequired()
                .HasMaxLength(255);
            
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255);
            
            entity.HasMany(e => e.TaskExecutions)
                .WithOne(te => te.Task)
                .HasForeignKey(te => te.TaskId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(e => e.PipelineId);
            entity.HasIndex(e => e.Order);
        });

        // Configure Execution entity
        modelBuilder.Entity<Execution>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(50);
            
            entity.Property(e => e.TriggerSource)
                .HasMaxLength(100);
            
            entity.Property(e => e.TriggerRef)
                .HasMaxLength(500);
            
            entity.Property(e => e.ErrorMessage)
                .HasMaxLength(2000);
            
            entity.Property(e => e.Output)
                .HasMaxLength(50000);
            
            entity.Property(e => e.CreatedBy)
                .IsRequired()
                .HasMaxLength(255);
            
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255);
            
            entity.HasMany(e => e.TaskExecutions)
                .WithOne(te => te.Execution)
                .HasForeignKey(te => te.ExecutionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.PipelineId);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.CreatedAt);
        });

        // Configure TaskExecution entity
        modelBuilder.Entity<TaskExecution>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(50);
            
            entity.Property(e => e.Output)
                .HasMaxLength(50000);
            
            entity.Property(e => e.ErrorOutput)
                .HasMaxLength(50000);
            
            entity.Property(e => e.CreatedBy)
                .IsRequired()
                .HasMaxLength(255);
            
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255);

            entity.HasIndex(e => e.ExecutionId);
            entity.HasIndex(e => e.TaskId);
            entity.HasIndex(e => e.Status);
        });
    }
}
