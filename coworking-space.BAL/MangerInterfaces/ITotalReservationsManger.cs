using coworking_space.BAL.Dtos.TotalReservationsDTo;
using coworking_space.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coworking_space.BAL.MangerInterfaces
{
    public interface ITotalReservationsManger
    {
        public TotalReservationsReadDto GetTotalReservations(int id, Status status);
    }
}
