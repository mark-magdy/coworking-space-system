using Microsoft.AspNetCore.Mvc;
using coworking_space.BAL.Interaces;
using coworking_space.BAL.DTOs.RoomDTO;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace coworking_space.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
<<<<<<< HEAD
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        public RoomController(IRoomService roomService)
        {
=======
    public class RoomController : ControllerBase {
        private  readonly IRoomService _roomService;
        public RoomController(IRoomService roomService) {
>>>>>>> df9c69004d4110000d5eff22561ce8881ab0103c
            _roomService = roomService;
        }

        // GET: api/<RoomController>
        //[HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_roomService.GetAllRooms());
        }

        // GET api/<RoomController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(_roomService.GetRoomById(id));
        }

        // POST api/<RoomController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RoomCreateDto value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdRoom = await _roomService.CreateRoomAsync(value);

            return CreatedAtAction(nameof(Get), new { id = createdRoom.ID }, createdRoom);
        }

        // PUT api/<RoomController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] RoomUpdateDto value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var updated = await _roomService.UpdateRoom(id, value);
            if (updated == null)
                return NotFound();
            return Ok(updated);
        }

        // DELETE api/<RoomController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var room = _roomService.GetRoomById(id);
            if (room == null)
                return NotFound();
            await _roomService.DeleteRoom(id);
            return Ok();
        }
    }
}
