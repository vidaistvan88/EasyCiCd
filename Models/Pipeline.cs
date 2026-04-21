using System.ComponentModel.DataAnnotations;

namespace EasyCiCd.Models;

/// <summary>
/// Represents a CI/CD pipeline configuration
/// </summary>
public class Pipeline : BaseEntity
{
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; set; }

    [Required]
    [StringLength(50)]
    public string Status { get; set; } = "draft"; // draft, active, inactive, archived

    [Required]
    public string UserId { get; set; } = string.Empty;

    // Navigation property
    public ICollection<PipelineTask> Tasks { get; set; } = new List<PipelineTask>();

    public ICollection<PipelineExecution> Executions { get; set; } = new List<PipelineExecution>();
}
