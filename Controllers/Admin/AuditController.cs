using Microsoft.AspNetCore.Mvc;
using Supplychain.Data;
using System.Linq;

[ApiController]
[Route("api/v1/audit-logs")]
public class AuditController : ControllerBase
{
    private readonly SupplyChainContext _context;

    public AuditController(SupplyChainContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get(
        [FromQuery] int? userId,
        [FromQuery] string action,
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo)
    {
        var query = _context.AuditLogs.AsQueryable();

        if (userId.HasValue)
            query = query.Where(x => x.UserId == userId.Value);

        if (!string.IsNullOrEmpty(action))
            query = query.Where(x => x.Action.Contains(action));

        if (dateFrom.HasValue)
            query = query.Where(x => x.Timestamp >= dateFrom.Value);

        if (dateTo.HasValue)
            query = query.Where(x => x.Timestamp <= dateTo.Value);

        var result = query.ToList();

        return Ok(result);
    }

    [HttpGet("recent")]

    public IActionResult GetRecentLogs()
    {
        var recentLogs = _context.AuditLogs
            .OrderByDescending(x => x.Timestamp)
            .Take(10)
            .ToList();

        return Ok(recentLogs);
    }
}