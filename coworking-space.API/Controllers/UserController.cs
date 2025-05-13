using Microsoft.AspNetCore.Mvc;
using coworking_space.BAL.Dtos.UserDTO;

    using coworking_space.BAL.Interaces; // Assuming you have IUserService
using System.Threading.Tasks;

namespace coworking_space.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // GET: api/User/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound("User not found.");
            return Ok(user);
        }

        // POST: api/User
        [HttpPost] 
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto userCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var createdUser = await _userService.CreateUserAsync(userCreateDto);
                return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating user: {ex.Message}");
            }
        }

        // PUT: api/User/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDto userUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedUser = await _userService.UpdateUserAsync(id, userUpdateDto);
            if (updatedUser == null)
                return NotFound("User not found.");

            return Ok(updatedUser);
        }

        // DELETE: api/User/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var isDeleted = await _userService.DeleteUserAsync(id);
            if (!isDeleted)
                return NotFound("User not found.");

            return NoContent();
        }
        [HttpGet("orders/{id}")]//get orders of user by id
        public async Task<IActionResult> GetOrdersByUserId(int id)
        {
            var orders = await _userService.GetOrdersByUserId(id);
            if (orders == null)
                return NotFound("Orders not found.");
            return Ok(orders);
        }
        [HttpGet("reservations/{id}")]//get reservations of user by id
        public async Task<IActionResult> GetReservationsByUserId(int id)
        {
            var reservations = await _userService.GetReservationsByUserId(id);
            if (reservations == null)
                return NotFound("Reservations not found.");
            return Ok(reservations);
        }

        // GET: api/User
        [HttpGet("active")]
        public async Task<IActionResult> GetAllActiveUsers()
        {
            var users = await _userService.GetAllactiveUsers();
            if (users == null || users.Count == 0)
                return NotFound("No active users found.");
            return Ok(users);
        }


    }
}