using coworking_space.BAL.Dtos.TotalReservationsDTo;
using coworking_space.BAL.Interaces;
using coworking_space.DAL.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace coworking_space.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TotalReservationsController : ControllerBase
    {
        private readonly ITotalReservationsService _reservationService;

        public TotalReservationsController(ITotalReservationsService reservationService)
        {
            _reservationService = reservationService;
        }
        [HttpGet("{id}")]
        public IActionResult GetTotalReservation(int id, [FromQuery] Status status)
        {

            var dto = _reservationService.GetTotalReservations(id, status);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }
        [HttpPost("{id}")]
        public IActionResult AddReservation([FromBody] ReservationCreateDto dto, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
               var reservation=_reservationService.AddReservation(dto, id);
                return Created(string.Empty, new 
                {
                    SpecialRequests = reservation.SpecialRequests,
                    Notes = reservation.Notes,
                    IsPrivate = reservation.IsPrivate,
                    RoomId = reservation.RoomId,
                 
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }



            

        }
        [HttpPost]
        public async Task< IActionResult> MakeTotalReservation([FromBody] TotalReservationCreateDto dto)
        {
           if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
               var totalReservation= await _reservationService.MakeTotalReservation(dto);
                return Created(string.Empty, totalReservation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
          

        }
    }
}
