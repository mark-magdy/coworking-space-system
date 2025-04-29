using coworking_space.BAL.DTOs.RoomDTO;
using coworking_space.DAL.Repository.Interfaces;
using coworking_space.DAL.Data.Models;
using coworking_space.BAL.Interaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coworking_space.BAL.Services {
    public class RoomService : IRoomService {
        private readonly IRoomRepository _roomRepository;
        public RoomService(IRoomRepository roomRepository) {
            _roomRepository = roomRepository;
        }
        public async Task<IEnumerable<RoomReadDto>> GetAllRooms() {
            var rooms = await _roomRepository.GetAllAsync();
            return rooms.Select(room => new RoomReadDto
            {
                ID = room.ID,
                Name = room.Name,
                Description = room.Description,
                PrivatePrice = room.PrivatePrice,
                MinimumPrivateCapacity = room.MinimumPrivateCapacity,
                MinimumBookingTime = room.MinimumBookingTime,
                SharedPricePerPerson = room.SharedPricePerPerson,
                PrivatePricePerPerson = room.PrivatePricePerPerson,
                Capacity = room.Capacity,
                CurrentCapacity = room.CurrentCapacity,
                ImageUrl = room.ImageUrl
            }).ToList();
        }
        public async Task<RoomReadDto> GetRoomById(int id) {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null) {
                throw new Exception("Room not found");
            }
            return new RoomReadDto
            {
                ID = room.ID,
                Name = room.Name,
                Description = room.Description,
                PrivatePrice = room.PrivatePrice,
                MinimumPrivateCapacity = room.MinimumPrivateCapacity,
                MinimumBookingTime = room.MinimumBookingTime,
                SharedPricePerPerson = room.SharedPricePerPerson,
                PrivatePricePerPerson = room.PrivatePricePerPerson,
                Capacity = room.Capacity,
                CurrentCapacity = room.CurrentCapacity,
                ImageUrl = room.ImageUrl
            };
        }
        public async Task<RoomReadDto?> CreateRoomAsync(RoomCreateDto roomCreateDto) {
            // Check if the room already exists

            // TODO: to be implmented 

            //var existingRoom = await _roomRepository.AnyAsync(r => r.Name.ToLower() == roomCreateDto.Name.ToLower());
            //if (existingRoom) {
            //    throw new Exception("A room with this name already exists.");
            //}

            var room = new Room
            {
                Name = roomCreateDto.Name,
                Description = roomCreateDto.Description,
                PrivatePrice = roomCreateDto.PrivatePrice,
                MinimumPrivateCapacity = roomCreateDto.MinimumPrivateCapacity,
                MinimumBookingTime = roomCreateDto.MinimumBookingTime,
                SharedPricePerPerson = roomCreateDto.SharedPricePerPerson,
                PrivatePricePerPerson = roomCreateDto.PrivatePricePerPerson,
                Capacity = roomCreateDto.Capacity,
                CurrentCapacity = roomCreateDto.CurrentCapacity,
                ImageUrl = roomCreateDto.ImageUrl,
                CreatedAt = DateTime.UtcNow // Set the created date to now
            };

            var createdRoom = await _roomRepository.AddAsync(room);

            // Return mapped DTO
            return new RoomReadDto
            {
                ID = createdRoom.ID,
                Name = createdRoom.Name,
                Description = createdRoom.Description,
                PrivatePrice = createdRoom.PrivatePrice,
                MinimumPrivateCapacity = createdRoom.MinimumPrivateCapacity,
                MinimumBookingTime = createdRoom.MinimumBookingTime,
                SharedPricePerPerson = createdRoom.SharedPricePerPerson,
                PrivatePricePerPerson = createdRoom.PrivatePricePerPerson,
                Capacity = createdRoom.Capacity,
                CurrentCapacity = createdRoom.CurrentCapacity,
                ImageUrl = createdRoom.ImageUrl
            };
        }
        public async Task<RoomReadDto> UpdateRoom(int id, RoomUpdateDto roomUpdateDto) {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null) {
                throw new Exception("Room not found");
            }

            // TODO: validate the roomUpdateDto

            room.Name = roomUpdateDto.Name;
            room.Description = roomUpdateDto.Description;
            room.PrivatePrice = roomUpdateDto.PrivatePrice;
            room.MinimumPrivateCapacity = roomUpdateDto.MinimumPrivateCapacity;
            room.MinimumBookingTime = roomUpdateDto.MinimumBookingTime;
            room.SharedPricePerPerson = roomUpdateDto.SharedPricePerPerson;
            room.PrivatePricePerPerson = roomUpdateDto.PrivatePricePerPerson;
            room.Capacity = roomUpdateDto.Capacity;
            room.CurrentCapacity = roomUpdateDto.CurrentCapacity;
            room.ImageUrl = roomUpdateDto.ImageUrl;
            _roomRepository.Update(room);
            return new RoomReadDto
            {
                ID = room.ID,
                Name = room.Name,
                Description = room.Description,
                PrivatePrice = room.PrivatePrice,
                MinimumPrivateCapacity = room.MinimumPrivateCapacity,
                MinimumBookingTime = room.MinimumBookingTime,
                SharedPricePerPerson = room.SharedPricePerPerson,
                PrivatePricePerPerson = room.PrivatePricePerPerson,
                Capacity = room.Capacity,
                CurrentCapacity = room.CurrentCapacity,
                ImageUrl = room.ImageUrl
            };
        }
        public async Task<bool> DeleteRoom(int id) {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null) {
                throw new Exception("Room not found");
            }
            _roomRepository.Delete(room);
            await _roomRepository.SaveAsync();
            return true;
        }
    }
}
