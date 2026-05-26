using Microsoft.AspNetCore.Mvc;
using SupplyChain.Services.Procurement.Interfaces;

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

        // ✅ GET: api/v1/inbound-shipments
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var shipments = await _service.GetAll();
            return Ok(shipments);
        }

        // ✅ GET: api/v1/inbound-shipments/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var shipment = await _service.GetById(id);
            return Ok(shipment);
        }

        // ✅ GET: api/v1/purchase-orders/{poId}/inbound-shipments
        [HttpGet("/api/v1/purchase-orders/{poId}/inbound-shipments")]
        public async Task<IActionResult> GetByPO(long poId)
        {
            var shipments = await _service.GetByPO(poId);
            return Ok(shipments);
        }


        // ✅ PUT: api/v1/inbound-shipments/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] ShipmentDTO dto)
        {
            var updatedShipment = await _service.Update(id, dto);
            return Ok(updatedShipment);
        }

        // ✅ PATCH: api/v1/inbound-shipments/{id}/status
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(long id, [FromBody] StatusUpdateDTO dto)
        {
            var updatedShipment = await _service.UpdateStatus(id, dto.Status);
            return Ok(updatedShipment);
        }

        // ✅ PATCH: api/v1/inbound-shipments/{id}/eta
        [HttpPatch("{id}/eta")]
        public async Task<IActionResult> UpdateETA(long id, [FromBody] ETAUpdateDTO dto)
        {
            var updatedShipment = await _service.UpdateETA(id, dto.ExpectedDeliveryDate);
            return Ok(updatedShipment);
        }

        // ✅ DELETE: api/v1/inbound-shipments/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _service.Delete(id);
            return NoContent();
        }
    }
}