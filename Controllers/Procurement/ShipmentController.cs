using Microsoft.AspNetCore.Mvc;
using SupplyChain.Services.Procurement.Interfaces;
using SupplyChain360.Enums.Procurement;

namespace SupplyChain.Procurement.Controllers
{
    [ApiController]
    [Route("api/v1/inbound-shipments")]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentService _service;

        public ShipmentController(IShipmentService service)
        {
            _service = service;
        }

        // Get all
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        // Get by ID
        [HttpGet("{shipmentId}")]
        public async Task<IActionResult> GetById(int shipmentId)
        {
            var shipment = await _service.GetById(shipmentId);

            if (shipment == null)
                return NotFound("Shipment not found");

            return Ok(shipment);
        }
        
        // Update
        [HttpPut("{shipmentId}")]
        public async Task<IActionResult> Update(int shipmentId, [FromBody] ShipmentDTO dto)
        {
            return Ok(await _service.Update(shipmentId, dto));
        }

        // Update Status
        [HttpPatch("{shipmentId}/status")]
        public async Task<IActionResult> UpdateStatus(int shipmentId, [FromBody] StatusUpdateDTO dto)
        {
            return Ok(await _service.UpdateStatus(shipmentId, dto.Status));
        }

        // Update ETA
        [HttpPatch("{shipmentId}/eta")]
        public async Task<IActionResult> UpdateETA(int shipmentId, [FromBody] ETAUpdateDTO dto)
        {
            return Ok(await _service.UpdateETA(shipmentId, dto.ExpectedDeliveryDate));
        }

        // Delete
        [HttpDelete("{shipmentId}")]
        public async Task<IActionResult> Delete(int shipmentId)
        {
            await _service.Delete(shipmentId);
            return NoContent();
        }

        //SEARCH 
        [HttpGet("search")]
        public async Task<IActionResult> Search(
            [FromQuery] int? poId,
            [FromQuery] string? supplierName,
            [FromQuery] ShipmentStatus? status)
        {
            var dto = new SearchInboundShipmentDto
            {
                PoId = poId ?? 0,
                SupplierName = supplierName,
                Status = status
            };

            var result = await _service.Search(dto); 
            return Ok(result);
        }
    }
}
