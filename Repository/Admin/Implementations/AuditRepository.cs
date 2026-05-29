using Supplychain.Data;

public class AuditRepository : IAuditRepository
{
    private readonly SupplyChainContext _context;

    public AuditRepository(SupplyChainContext context)
    {
        _context = context;
    }

    public List<AuditLog> GetAllAuditLogs() => _context.AuditLogs.ToList();

    public List<AuditLog> GetFilteredAuditLogs(
        int? performedByUserId,
        string actionName,
        DateTime? fromDate,
        DateTime? toDate)
    {
        var query = _context.AuditLogs.AsQueryable();

        if (performedByUserId.HasValue)
            query = query.Where(x => x.UserId == performedByUserId.Value);

        if (!string.IsNullOrEmpty(actionName))
            query = query.Where(x => x.Action.Contains(actionName));

        if (fromDate.HasValue)
            query = query.Where(x => x.Timestamp >= fromDate.Value);

        if (toDate.HasValue)
            query = query.Where(x => x.Timestamp <= toDate.Value);

        return query.ToList();
    }
}