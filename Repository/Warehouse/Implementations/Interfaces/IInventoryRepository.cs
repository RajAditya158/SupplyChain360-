using Supplychain.Models.Warehouse;
using System.Threading.Tasks;

namespace Supplychain.Repository.Warehouse.Interfaces
{
    public interface IInventoryRepository
    {
        Task<Inventory> GetBySkuAsync(string sku);
        Task UpdateInventoryAsync(Inventory inventory);
        
        Task<object> GetInventorySummaryAsync();
        Task<Inventory?> GetStockBySkuAsync(string sku);
        Task<bool> UpdateStockAsync(string sku, int quantity);
        Task<IEnumerable<Inventory>> GetCriticalStocksAsync();
        Task UpdateSafetyStockAsync(string sku, int safetyStock);
        Task<IEnumerable<Inventory>> GetReconciliationAsync();
        Task UpdateReconciliationAsync(string sku, int quantityOnHand);
        Task<IEnumerable<Inventory>> GetAllAsync();
    }
}