using System.ComponentModel.DataAnnotations;

public class AuditLog
{
    [Key]
    public int AuditId { get; set; }

    public int? UserId { get; set; }

    public required string Action { get; set; }

    public DateTime Timestamp { get; set; }
}