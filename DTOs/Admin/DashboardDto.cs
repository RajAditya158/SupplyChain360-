public class DashboardDto
{
    public int TotalUsers { get; set; }
    public int ActiveSessions { get; set; }
    public int FailedLogs { get; set; }
    public string? ActiveRoles { get; set; }
}
namespace Supplychain.DTOs.Admin
{
    public class DashboardDto
    {
        public int TotalUsers { get; set; }
        public int TotalSuppliers { get; set; }
        public int TotalWarehouses { get; set; }
        public int ActiveSessions { get; set; }
        public int FailedLogs { get; set; }
        public string? ActiveRoles { get; set; }
    }
}
