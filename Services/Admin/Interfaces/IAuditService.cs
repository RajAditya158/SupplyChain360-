public interface IAuditService
{
    List<AuditLog> GetAllAuditLogs();

    List<AuditLog> GetFilteredAuditLogs(
        int? performedByUserId,
        string actionName,
        DateTime? fromDate,
        DateTime? toDate
    );

    void Log(int? performedByUserId, string actionName);
}