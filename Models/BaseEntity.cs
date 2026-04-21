namespace EasyCiCd.Models;

/// <summary>
/// Base entity with audit fields as shadow properties
/// </summary>
public abstract class BaseEntity
{
    // Audit fields are configured as shadow properties in the DbContext
    // CreatedAt, UpdatedAt, CreatedBy, UpdatedBy
}