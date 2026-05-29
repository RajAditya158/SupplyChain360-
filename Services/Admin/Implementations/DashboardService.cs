using Supplychain.Data;

public class DashboardService : IDashboardService
{
    private readonly SupplyChainContext _context;

    public DashboardService(SupplyChainContext context)
    {
        _context = context;
    }

    public DashboardDto GetDashboardData()
    {
        var totalUsers = _context.Users.Count();

        var activeUsers = _context.Users
            .Count(u => u.Status == UserStatus.Active);

        var dashboard = new DashboardDto
        {
            TotalUsers = totalUsers,

            ActiveSessions = _context.AuditLogs
                .Count(a => a.Action.Contains("Login Success") &&
                            a.Timestamp >= DateTime.Now.AddMinutes(-30)),

            FailedLogs = _context.AuditLogs
                .Count(a => a.Action.Contains("Login Failed")),

            ActiveRoles = totalUsers == 0
                ? "0%"
                : ((activeUsers * 100) / totalUsers) + "%"
        };

        return dashboard;
    }
}