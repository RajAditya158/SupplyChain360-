using Supplychain.Data;
using Supplychain.Models.Warehouse;
using Supplychain.Repository.Warehouse.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Supplychain.Repository.Warehouse.Implementations
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly SupplyChainContext _context;

        public InventoryRepository(SupplyChainContext context)
        {
            _context = context;
        }

        public async Task<object> GetInventorySummaryAsync()
        {
            var totalCapacity = 1000; // Example static capacity
            var currentStocks = await _context.Inventory.SumAsync(i => i.QuantityOnHand);
            var availableSpace = totalCapacity - currentStocks;

            return new
            {
                Capacity = totalCapacity,
                CurrentStocks = currentStocks,
                AvailableSpace = availableSpace
            };
        }

        public async Task<Inventory?> GetStockBySkuAsync(string sku)
        {
            return await _context.Inventory.FirstOrDefaultAsync(i => i.SKU == sku);
        }

        public async Task<bool> UpdateStockAsync(string sku, int quantity)
        {
            var stock = await _context.Inventory.FirstOrDefaultAsync(i => i.SKU == sku);
            if (stock == null) return false;

            stock.QuantityOnHand = quantity;
            stock.LastUpdated = System.DateTime.UtcNow;

            _context.Inventory.Update(stock);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Inventory> GetBySkuAsync(string sku)
        {
            return await _context.Inventory.FirstOrDefaultAsync(i => i.SKU == sku);
        }

       

        Task<Inventory> IInventoryRepository.GetBySkuAsync(string sku)
        {
            return GetBySkuAsync(sku);
        }
        public async Task<IEnumerable<Inventory>> GetCriticalStocksAsync()
        {
            return await _context.Inventory
                .Where(i => i.QuantityOnHand < i.SafetyStock)
                .ToListAsync();
        }

        public async Task UpdateSafetyStockAsync(string sku, int safetyStock)
        {
            var inventory = await _context.Inventory.FirstOrDefaultAsync(i => i.SKU == sku);
            if (inventory == null)
                throw new InvalidOperationException($"SKU {sku} not found.");

            inventory.SafetyStock = safetyStock;
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Inventory>> GetAllAsync()
        {
            return await _context.Inventory.ToListAsync();
        }


        public async Task<IEnumerable<Inventory>> GetReconciliationAsync()
        {
            // Example condition: items where QuantityOnHand < SafetyStock
            return await _context.Inventory
                .Where(i => i.QuantityOnHand != i.PhysicalCount)
                .ToListAsync();
        }

        public async Task UpdateReconciliationAsync(string sku, int physicalCount)
        {
            var inventory = await _context.Inventory.FirstOrDefaultAsync(i => i.SKU == sku);
            if (inventory == null)
                throw new InvalidOperationException($"SKU {sku} not found.");

            inventory.PhysicalCount = physicalCount;   // store separately
            inventory.LastUpdated = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateInventoryAsync(Inventory inventory)
        {
            _context.Inventory.Update(inventory);
            await _context.SaveChangesAsync();
        }
        
    }
}