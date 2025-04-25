using coworking_space.BAL.MangerInterfaces;
using coworking_space.DAL.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace coworking_space.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TotalReservationsController : ControllerBase
    {
        private readonly ITotalReservationsManger _reservationManger;
        [HttpGet("{id}")]
        public IActionResult GetTotalReservation(int id, [FromQuery]Status status)
        {

            var dto = _reservationManger.GetTotalReservations(id, status);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }
    }
}
