using Microsoft.AspNetCore.Mvc;
using Supplychain.Dtos.Warehouse;
using Supplychain.Models.Warehouse;
using Supplychain.Services.Warehouse;
using System.Threading.Tasks;

namespace Supplychain.Controllers.Warehouse
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        // POST /api/inventory
        [HttpPost]
public async Task<IActionResult> AddInventory([FromBody] CreateInventoryDto dto)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    var inventory = new Inventory
    {
        SKU = dto.SKU,
        ProductName = dto.ProductName,
        QuantityOnHand = dto.QuantityOnHand,
        StorageLocation = dto.StorageLocation,
        LastUpdated = DateTime.UtcNow // system-generated
    };

    var created = await _inventoryService.AddInventoryAsync(inventory);
    return Ok(created);
}


        // GET /api/inventory/summary
        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            var summary = await _inventoryService.GetInventorySummaryAsync();
            return Ok(summary);
        }

        // GET /api/inventory/{sku}
        [HttpGet("{sku}")]
        public async Task<IActionResult> GetStockBySku(string sku)
        {
            var stock = await _inventoryService.GetStockBySkuAsync(sku);
            if (stock == null) return NotFound();
            return Ok(stock);
        }

        // PUT /api/inventory/update/{sku}
        [HttpPut("update/{sku}")]
        public async Task<IActionResult> UpdateStock(string sku, [FromBody] int quantity)
        {
            var success = await _inventoryService.UpdateStockAsync(sku, quantity);
            if (!success) return NotFound();
            return Ok(new { message = "Stock updated successfully" });
        }
        [HttpGet("safety")]
        public async Task<IActionResult> GetCriticalStocks()
        {
            var criticalStocks = await _inventoryService.GetCriticalStocksAsync();
            return Ok(criticalStocks);
        }

        [HttpPut("safety/{sku}")]
        public async Task<IActionResult> UpdateSafetyStock(string sku, [FromBody] SafetyStockDto dto)
        {
            try
            {
                await _inventoryService.UpdateSafetyStockAsync(sku, dto.SafetyStock);
                return Ok(new { message = $"Safety stock updated for {sku}" });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        public class SafetyStockDto
        {
            public int SafetyStock { get; set; }
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAllInventories()
        {
            var inventories = await _inventoryService.GetAllAsync();
            return Ok(inventories);
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchInventory([FromQuery] string sku)
        {
            var inventory = await _inventoryService.GetBySkuAsync(sku);

            if (inventory == null)
                return NotFound(new { error = $"SKU {sku} not found." });

            return Ok(inventory);
        }
        [HttpGet("reconciliation")]
        public async Task<IActionResult> GetReconciliation()
        {
            var items = await _inventoryService.GetReconciliationAsync();
            return Ok(items);
        }

        [HttpPut("reconciliation/{sku}/{physicalCount}")]
        public async Task<IActionResult> UpdateReconciliation(string sku, int physicalCount)
        {
            try
            {
                await _inventoryService.UpdateReconciliationAsync(sku, physicalCount);
                return Ok(new { message = $"Reconciliation updated for {sku} with count {physicalCount}" });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        public class ReconciliationDto
        {
            public int QuantityOnHand { get; set; }
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateInventory([FromBody] Inventory inventory)
        {
            try
            {
                await _inventoryService.UpdateInventoryAsync(inventory);
                return Ok(new { message = $"Inventory updated for {inventory.SKU}" });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

    }
}
