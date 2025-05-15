using coworking_space.BAL.Dtos.TotalReservationsDTo;
using coworking_space.BAL.Dtos.UserDTO;
using coworking_space.BAL.DTOs.OrderDTO;
using coworking_space.BAL.DTOs.UserDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace coworking_space.BAL.Interaces
{
    public interface IUserService
    {
        Task<List<UserReadDto>> GetAllUsersAsync();
        Task<UserReadDto?> GetUserByIdAsync(int id);
     //   Task<UserReadDto> CreateUserAsync(UserCreateDto userCreateDto);
        Task<UserReadDto?> UpdateUserAsync(int id, UserUpdateDto userUpdateDto);
        Task<bool> DeleteUserAsync(int id);
        Task<List<OrderReadDto>> GetOrdersByUserId(string id);
        Task<List<TotalReservationsReadDto>> GetReservationsByUserId(string id);

        Task<List<ActiveUserDto>> GetAllactiveUsers();
    }
}
