namespace EasyCiCd.Models;

/// <summary>
/// Represents a CI/CD pipeline configuration
/// </summary>
public class Pipeline
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public string Repository { get; set; } = string.Empty;
    
    public string Branch { get; set; } = "main";
    
    public bool IsActive { get; set; } = true;
    
    public string TriggerEvent { get; set; } = "push"; // push, pull_request, manual
    
    // Audit fields
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
    
    public string CreatedBy { get; set; } = "system";
    
    public string? UpdatedBy { get; set; }
    
    // Navigation property
    public ICollection<CiTask> Tasks { get; set; } = new List<CiTask>();
    
    public ICollection<Execution> Executions { get; set; } = new List<Execution>();
}
