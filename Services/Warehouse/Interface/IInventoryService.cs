using Supplychain.Models.Warehouse;
using System.Threading.Tasks;

namespace Supplychain.Services.Warehouse
{
    public interface IInventoryService
    {
        Task<object> GetInventorySummaryAsync();
        Task<Inventory?> GetStockBySkuAsync(string sku);
        Task<bool> UpdateStockAsync(string sku, int quantity);
        Task<Inventory> AddInventoryAsync(Inventory inventory);
        Task<IEnumerable<Inventory>> GetCriticalStocksAsync();
        Task UpdateSafetyStockAsync(string sku, int safetyStock);
        Task<IEnumerable<Inventory>> GetAllAsync();
        Task<Inventory> GetBySkuAsync(string sku);

        Task UpdateInventoryAsync(Inventory inventory);
        Task<IEnumerable<Inventory>> GetReconciliationAsync();
        Task UpdateReconciliationAsync(string sku, int quantityOnHand);
    }
}
