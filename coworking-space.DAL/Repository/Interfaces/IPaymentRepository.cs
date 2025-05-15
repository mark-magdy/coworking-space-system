using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using coworking_space.DAL.Data.Models;


namespace coworking_space.DAL.Repository.Interfaces
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        Task<IEnumerable<Payment>> GetPaymentsByMonthAsync(int month, int year);
        Task<IEnumerable<Payment>> GetPaymentsByDayAsync(int day, int month, int year);


    }
}
