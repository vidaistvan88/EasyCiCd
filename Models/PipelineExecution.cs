using System.ComponentModel.DataAnnotations;

namespace EasyCiCd.Models;

/// <summary>
/// Represents an execution/run of a pipeline
/// </summary>
public class PipelineExecution : BaseEntity
{
    public int Id { get; set; }

    public int PipelineId { get; set; }

    [Required]
    [StringLength(50)]
    public string Status { get; set; } = "pending"; // pending, running, success, failed, cancelled

    [Required]
    public DateTime StartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public string? Output { get; set; }

    // Navigation property
    public Pipeline? Pipeline { get; set; }
}