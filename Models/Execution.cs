namespace EasyCiCd.Models;

/// <summary>
/// Represents an execution/run of a pipeline
/// </summary>
public class Execution
{
    public int Id { get; set; }
    
    public int PipelineId { get; set; }
    
    public string Status { get; set; } = "pending"; // pending, running, success, failed, cancelled
    
    public string? TriggerSource { get; set; } // webhook, manual, scheduled
    
    public string? TriggerRef { get; set; } // commit hash, branch name, etc.
    
    public DateTime StartedAt { get; set; }
    
    public DateTime? CompletedAt { get; set; }
    
    public int? DurationSeconds { get; set; }
    
    public string? ErrorMessage { get; set; }
    
    public string? Output { get; set; }
    
    // Audit fields
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
    
    public string CreatedBy { get; set; } = "system";
    
    public string? UpdatedBy { get; set; }
    
    // Navigation properties
    public Pipeline? Pipeline { get; set; }
    
    public ICollection<TaskExecution> TaskExecutions { get; set; } = new List<TaskExecution>();
}
