using Supplychain.Data;
using Supplychain.Models;
using Microsoft.AspNetCore.Http;

public class AuditService : IAuditService
{
    private readonly SupplyChainContext _context;
    private readonly IHttpContextAccessor _http;

    public AuditService(SupplyChainContext context, IHttpContextAccessor http)
    {
        _context = context;
        _http = http;
    }

    public List<AuditLog> GetAllAuditLogs()
    {
        return _context.AuditLogs
            .OrderByDescending(x => x.Timestamp)
            .ToList();
    }

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

        return query
            .OrderByDescending(x => x.Timestamp)
            .ToList();
    }

    public void Log(int? performedByUserId, string actionName)
    {
        if (!performedByUserId.HasValue || performedByUserId.Value <= 0)
        {
            try
            {
                var ctx = _http?.HttpContext;
                var claim = ctx?.User?.FindFirst("UserId")?.Value;
                if (!string.IsNullOrEmpty(claim) && int.TryParse(claim, out var parsed))
                    performedByUserId = parsed;
            }
            catch
            {
            }
        }
        var log = new AuditLog
        {
            UserId = performedByUserId,
            Action = actionName,
            Timestamp = DateTime.UtcNow
        };

        _context.AuditLogs.Add(log);
        _context.SaveChanges();
    }
}