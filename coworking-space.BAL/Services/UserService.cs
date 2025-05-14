using CO_Working_Space;
using coworking_space.BAL.Dtos.TotalReservationsDTo;
using coworking_space.BAL.Dtos.UserDTO;
using coworking_space.BAL.DTOs.OrderDTO;
using coworking_space.BAL.DTOs.UserDTO;
using coworking_space.BAL.Interaces;
using coworking_space.DAL.Data.Models;
using coworking_space.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coworking_space.BAL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITotalReservationsService _totalReservationsService;
        private readonly IOrderService _orderService;



        public UserService(IUserRepository userRepository, ITotalReservationsService totalReservationsService,IOrderService orderService)


        {
            _userRepository = userRepository;
            _totalReservationsService = totalReservationsService;
            _orderService = orderService;
        }

        public async Task<List<UserReadDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserReadDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                UniversityName = u.UniversityName,
                IsActive = u.IsActive,
                Role = u.Role
            }).ToList();
        }

        public async Task<UserReadDto?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            return new UserReadDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UniversityName = user.UniversityName,
                IsActive = user.IsActive,
                Role = user.Role
            };
        }

        public async Task<UserReadDto> CreateUserAsync(UserCreateDto userCreateDto)
        {
            var user = new User
            {
                Name = userCreateDto.Name,
                Email = userCreateDto.Email,
                Password = userCreateDto.Password,
                PhoneNumber = userCreateDto.PhoneNumber,
                UniversityName = userCreateDto.UniversityName,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,

                IsActive = true,
                Role = "User"
            };

            var createdUser = await _userRepository.AddAsync(user);
            await _userRepository.SaveAsync();

            return new UserReadDto
            {
                Id = createdUser.Id,
                Name = createdUser.Name,
                Email = createdUser.Email,
                PhoneNumber = createdUser.PhoneNumber,
                UniversityName = createdUser.UniversityName,
                IsActive = createdUser.IsActive,
                Role = createdUser.Role
            };
        }

        public async Task<UserReadDto?> UpdateUserAsync(int id, UserUpdateDto userUpdateDto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            user.Name = userUpdateDto.Name ?? user.Name;
            user.Email = userUpdateDto.Email ?? user.Email;
            user.PhoneNumber = userUpdateDto.PhoneNumber ?? user.PhoneNumber;
            user.UniversityName = userUpdateDto.UniversityName ?? user.UniversityName;
            user.IsActive = userUpdateDto.IsActive ?? user.IsActive;

            _userRepository.Update(user);
            await _userRepository.SaveAsync();

            return new UserReadDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UniversityName = user.UniversityName,
                IsActive = user.IsActive,
                Role = user.Role
            };
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            await _userRepository.DeleteByIdAsync(id);
            await _userRepository.SaveAsync();
            return true;
        }
        public async Task<List<OrderReadDto>> GetOrdersByUserId(int id)
        {
            var user = await _userRepository.GetUserWithOrders(id);
            if (user == null) return null;
            if (user.Orders == null || !user.Orders.Any()) return null;
            var orders = new List<OrderReadDto>();
            var userOrders = user.Orders.ToList();
            foreach (var order in user.Orders)
            {
                var ord = await _orderService.GetOrderByIdAsync(order.Id);
                orders.Add(ord);
            }
            return orders;
        }
        public async Task<List<TotalReservationsReadDto>> GetReservationsByUserId(int id)
        {
            var user = await _userRepository.GetUserWithReservations(id);
            if (user == null) return null;
            if (user.TotalReservations == null || !user.TotalReservations.Any()) return null;
            List<TotalReservationsReadDto> reservations = new List<TotalReservationsReadDto>();

            //add firstly the paid total reservations
            //TODO after payments handling

            foreach (var reservation in user.TotalReservations)
            {
                reservations.Add(_totalReservationsService.GetTotalReservations(reservation.Id));
            }
            return reservations;
        }

        public async Task<List<ActiveUserDto>> GetAllactiveUsers()
        {
            var users = await _userRepository.GetAllActiveUsers();
            return users.Select(static u =>
                      {
                          var reservation = u.TotalReservations.Where(t => t.Status == Status.Pending).FirstOrDefault();
                          var start = reservation!=null?reservation.StartDate:default(DateTime);
                          var _reservations = reservation!=null? reservation.Price:0;
                          var order = u.Orders.Where(o => o.Order_Status == Status.Pending).FirstOrDefault();
                          var   _orders= order!=null? order.TotalPrice:0;
                          return new ActiveUserDto
                          {
                              id = u.Id,
                              user = u.Name,
                              date = start,
                              time = (TimeSpan)(DateTime.Now - start),
                              reservations = _reservations,
                              orders=_orders,
                              total = _reservations + _orders,
                          };
                      }).ToList();

        } 

      
    }
}