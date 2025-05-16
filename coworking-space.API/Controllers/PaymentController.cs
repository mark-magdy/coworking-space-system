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
                await _paymentService.ProcessPaymentAsync(dto);

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
        [HttpPost("bulk-create")]
        public async Task<IActionResult> CreateBulkPayment([FromBody] CreateBulkPaymentDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _paymentService.ProcessBulkPaymentAsync(dto);
                return Ok(new { message = "Bulk payment processed successfully." });
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
        [HttpGet("revenue")]
        public async Task<IActionResult> GetMonthlyRevenue([FromQuery] int month, [FromQuery] int year)
        {
            if (month < 1 || month > 12 || year < 1)
                return BadRequest("Invalid month or year.");

            try
            {
                decimal totalRevenue = await _paymentService.GetMonthlyRevenueAsync(month, year);
                return Ok(new { month, year, totalRevenue });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        [HttpGet("revenue/daily")]
        public async Task<IActionResult> GetDailyRevenue([FromQuery] int day, [FromQuery] int month, [FromQuery] int year)
        {
            if (day < 1 || day > 31 || month < 1 || month > 12 || year < 1)
                return BadRequest("Invalid day, month, or year.");

            try
            {
                decimal totalRevenue = await _paymentService.GetDailyRevenueAsync(day, month, year);
                return Ok(new { day, month, year, totalRevenue });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }


    }
}