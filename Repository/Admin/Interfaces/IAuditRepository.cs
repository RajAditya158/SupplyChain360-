public interface IAuditRepository
{
    List<AuditLog> GetAllAuditLogs();

    List<AuditLog> GetFilteredAuditLogs(
        int? performedByUserId,
        string actionName,
        DateTime? fromDate,
        DateTime? toDate);
}