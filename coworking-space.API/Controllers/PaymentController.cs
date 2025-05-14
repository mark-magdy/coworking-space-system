using coworking_space.BAL.DTOs.PaymentDTO;
using coworking_space.BAL.Interaces;
using coworking_space.DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace coworking_space.API

{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;

        }
        [HttpPost("create")]
        public async Task<IActionResult> CreatePayment([FromBody] CreatPaymentDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _paymentService.ProcessPaymentAsync(dto); // You no longer need the returned payment

                return Ok(new
                {
                    message = "Payment processed successfully."
                });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}