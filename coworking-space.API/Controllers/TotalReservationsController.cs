using coworking_space.BAL.Dtos.TotalReservationsDTo;
using coworking_space.BAL.Interaces;
using coworking_space.BAL.Services;
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

        [HttpGet]
        public async Task<IActionResult> GetAllTotalReservations()
        {
            try
            {
                var totalReservations = await _reservationService.GetAllTotalReservationsAsync();
                if (totalReservations == null || !totalReservations.Any())
                {
                    return NotFound("No total reservations found.");
                }
                return Ok(totalReservations);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetTotalReservation(int id)//need to be asynchronous
        {

            var dto = _reservationService.GetTotalReservations(id);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        [HttpGet("{totalReservationId}/reservations/{reservationId}")]  //need to be asynchronous
        public IActionResult GetReservation(int totalReservationId, int reservationId)
        {
            try
            {
                var reservation = _reservationService.GetReservationFromTotalReservation(totalReservationId, reservationId);
                if (reservation == null)
                {
                    return NotFound("Reservation not found or does not belong to the specified total reservation.");
                }
                return Ok(reservation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{userId}")] //need to be asynchronous
        public IActionResult AddReservation([FromBody] ReservationCreateDto dto, int userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var reservation = _reservationService.AddReservation(dto, userId);
                return Created(string.Empty, reservation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }





        }


        //[HttpPost]
        //public async Task<IActionResult> MakeTotalReservation([FromBody] TotalReservationCreateDto dto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    try
        //    {
        //        var totalReservation = await _reservationService.MakeTotalReservation(dto);
        //        return Created(string.Empty, totalReservation);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }


        //}

        [HttpPut("{id}")] //total reservation id 
        public async Task<IActionResult> UpdateReservation(int id, [FromBody] ReservationUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var reservation = await _reservationService.UpdateReservation(id, dto);
                return Ok(reservation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete("{id}")]//total reservation id
        public async Task<IActionResult> DeleteTotalReservation(int id)
        {
            try
            {
                var isDeleted = await _reservationService.DeleteTotalReservationAsync(id);
                if (!isDeleted)
                {
                    return NotFound("Total reservation not found.");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{totalReservationId}/reservations/{reservationId}")]
        public async Task<IActionResult> DeleteReservation(int totalReservationId, int reservationId)
        {
            try
            {
                var isDeleted = await _reservationService.DeleteReservationAsync(totalReservationId, reservationId);
                if (!isDeleted)
                {
                    return NotFound("Reservation not found or does not belong to the specified total reservation.");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{roomId}/upcoming-reservations")]
        public async Task<IActionResult> GetUpcomingReservations(int roomId)
        {
            try
            {
                var reservations = await _reservationService.GetUpcomingReservationsAsync(roomId);

                if (reservations == null || !reservations.Any())
                {
                    return NotFound($"No upcoming reservations found for room ID {roomId}.");
                }

                return Ok(reservations);
            }
            catch (Exception ex)
            {
                // Optionally log the exception
                return StatusCode(500, $"An error occurred while retrieving reservations: {ex.Message}");
            }
        }

        //[HttpGet("/upcoming")]
        //public async Task<IActionResult> GetAllUpcomingReservations()
        //{
        //    try
        //    {
        //        var upcomingReservations = await _reservationService.GetAllTotalReservationsAsync();
        //        if (totalReservations == null || !totalReservations.Any())
        //        {
        //            return NotFound("No total reservations found.");
        //        }
        //        return Ok(upcomingReservations);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //}

    }
}
