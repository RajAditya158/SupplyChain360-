using Supplychain.Data;
using Supplychain.Models.Warehouse;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Supplychain.Repository.Warehouse.Interfaces;
using SupplyChain360.Enums.Warehouse;

namespace Supplychain.Services.Warehouse
{
    public class InventoryService:IInventoryService
    {
        
        private readonly SupplyChainContext _context;
      
        public InventoryService(SupplyChainContext context)
        {
            _context = context;
        }


        // Get warehouse summary
        public async Task<object> GetInventorySummaryAsync()
        {
            var totalCapacity = 1000; // Example static capacity, can be stored in Warehouse table
            var currentStocks = await _context.Inventory.SumAsync(i => i.QuantityOnHand);
            var availableSpace = totalCapacity - currentStocks;

            return new
            {
                Capacity = totalCapacity,
                CurrentStocks = currentStocks,
                AvailableSpace = availableSpace
            };
        }

        // Track stock by SKU
        public async Task<Inventory?> GetStockBySkuAsync(string sku)
        {
            return await _context.Inventory.FirstOrDefaultAsync(i => i.SKU == sku);
        }

        // Update stock quantity
        public async Task<bool> UpdateStockAsync(string sku, int quantity)
        {
            var stock = await _context.Inventory.FirstOrDefaultAsync(i => i.SKU == sku);
            if (stock == null) return false;

            stock.QuantityOnHand = quantity;
            stock.LastUpdated = DateTime.UtcNow;

            _context.Inventory.Update(stock);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Inventory> AddInventoryAsync(Inventory inventory)
        {
            inventory.LastUpdated = DateTime.UtcNow;
            inventory.Status = InventoryStatus.Available;
            _context.Inventory.Add(inventory);
            await _context.SaveChangesAsync();
            return inventory;
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
            if (inventory == null) throw new InvalidOperationException($"Inventory with SKU {sku} not found.");

            inventory.SafetyStock = safetyStock;
            inventory.LastUpdated = DateTime.UtcNow;

            _context.Inventory.Update(inventory);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Inventory>> GetAllAsync()
        {
            return await _context.Inventory.ToListAsync();
        }
        public async Task<Inventory> GetBySkuAsync(string sku)
        {
            return await _context.Inventory.FirstOrDefaultAsync(i => i.SKU == sku);
        }
        

        public async Task<List<Inventory>> GetReconciliationAsync()
        {
            return await _context.Inventory
                .Where(i => i.QuantityOnHand != i.SafetyStock) // Adjust as needed
                .ToListAsync();
        }

        public async Task UpdateReconciliationAsync(string sku, int quantityOnHand)
        {
            var inventory = await _context.Inventory.FirstOrDefaultAsync(i => i.SKU == sku);
            if (inventory != null)
            {
                inventory.PhysicalCount = quantityOnHand;
                inventory.LastUpdated = DateTime.UtcNow; // Optional: update timestamp
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateInventoryAsync(Inventory inventory)
        {
            _context.Inventory.Update(inventory);
            await _context.SaveChangesAsync();
        }

        // Removed duplicate explicit interface implementation for IInventoryService.GetReconciliationAsync
        async Task<IEnumerable<Inventory>> IInventoryService.GetReconciliationAsync()
        {
            return await GetReconciliationAsync();
        }


        // Task<IEnumerable<Inventory>> IInventoryService.GetReconciliationAsync()
        // {
        //     throw new NotImplementedException();
        // }
    }
}
