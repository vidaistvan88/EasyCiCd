using System.ComponentModel.DataAnnotations;

namespace EasyCiCd.Models;

/// <summary>
/// Represents a task within a pipeline
/// </summary>
public class PipelineTask : BaseEntity
{
    public int Id { get; set; }

    public int PipelineId { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Script { get; set; } = string.Empty;

    public int Timeout { get; set; } = 3600; // seconds, 1 hour default

    public int Order { get; set; }

    public string? Dependencies { get; set; } // JSON array of task IDs or names

    // Navigation property
    public Pipeline? Pipeline { get; set; }
}