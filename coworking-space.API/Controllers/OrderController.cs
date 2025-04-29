using coworking_space.BAL.DTOs.OrderDTO;
using coworking_space.BAL.Interaces;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace coworking_space.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService) {
            _orderService = orderService;
        }

        // GET: api/<OrderController>
        [HttpGet]
        public async Task<IActionResult> Get() {
            return Ok(await _orderService.GetAllOrdersAsync());
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) {
            return Ok(await _orderService.GetOrderByIdAsync(id));
        }

        // POST api/<OrderController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderCreateDto value) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var createdOrder = await _orderService.AddOrderAsync(value);
            return CreatedAtAction(nameof(Get), new { id = createdOrder.Id }, createdOrder);
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] OrderUpdateDto value) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var updated = await _orderService.UpdateOrder(id, value);
            if (updated == null)
                return NotFound();
            return Ok(updated);
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();
            await _orderService.DeleteOrderAsync(id);
            return Ok();
        }
    }
}
