using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using coworking_space.BAL.DTOs;
using coworking_space.DAL.Data.Models;
using coworking_space.BAL.DTOs.RoomDTO;

namespace coworking_space.BAL.Interaces {
    public interface IRoomService {
        Task<RoomReadDto> GetRoomById(int id);
        Task<IEnumerable<RoomReadDto>> GetAllRooms();
        //Task<RoomCreateDto> RoomCreateDto(RoomCreateDto roomCreateDto);
        Task<RoomReadDto?> CreateRoomAsync(RoomCreateDto roomCreateDto);
        Task<RoomReadDto> UpdateRoom(int id, RoomUpdateDto roomUpdateDto);
        Task<bool> DeleteRoom(int id);
    }
}