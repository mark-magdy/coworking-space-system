using coworking_space.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coworking_space.DAL.Repository.Interfaces
{
    public interface IRoomRepository : IGenericRepository<Room> { 
        public Task<Room >getRoomByIdWithReservations(int id);
    }
}
