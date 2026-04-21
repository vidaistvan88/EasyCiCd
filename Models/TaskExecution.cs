namespace EasyCiCd.Models;

/// <summary>
/// Represents the execution record of a specific task within a pipeline execution
/// </summary>
public class TaskExecution
{
    public int Id { get; set; }
    
    public int ExecutionId { get; set; }
    
    public int TaskId { get; set; }
    
    public string Status { get; set; } = "pending"; // pending, running, success, failed, skipped, cancelled
    
    public DateTime StartedAt { get; set; }
    
    public DateTime? CompletedAt { get; set; }
    
    public int? DurationSeconds { get; set; }
    
    public string? Output { get; set; }
    
    public string? ErrorOutput { get; set; }
    
    public int ExitCode { get; set; }
    
    // Audit fields
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
    
    public string CreatedBy { get; set; } = "system";
    
    public string? UpdatedBy { get; set; }
    
    // Navigation properties
    public Execution? Execution { get; set; }
    
    public CiTask? Task { get; set; }
}
