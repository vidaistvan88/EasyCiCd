namespace EasyCiCd.Models;

/// <summary>
/// Represents a task within a pipeline
/// </summary>
public class CiTask
{
    public int Id { get; set; }
    
    public int PipelineId { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public string TaskType { get; set; } = string.Empty; // build, test, deploy, etc.
    
    public string? Command { get; set; }
    
    public int Order { get; set; }
    
    public bool IsRequired { get; set; } = true;
    
    public int TimeoutSeconds { get; set; } = 3600; // 1 hour default
    
    public bool IsActive { get; set; } = true;
    
    // Audit fields
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
    
    public string CreatedBy { get; set; } = "system";
    
    public string? UpdatedBy { get; set; }
    
    // Navigation properties
    public Pipeline? Pipeline { get; set; }
    
    public ICollection<TaskExecution> TaskExecutions { get; set; } = new List<TaskExecution>();
}
