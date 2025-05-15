using coworking_space.DAL.Data.Models;
using coworking_space.DAL.Data;
using coworking_space.DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace coworking_space.DAL.Repository.Implementations
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(Context context) : base(context) { }

        public async Task<IEnumerable<Payment>> GetPaymentsByMonthAsync(int month, int year)
        {
            return await _context.Payments
                .Where(p => p.PaymentDate.Month == month && p.PaymentDate.Year == year)
                .ToListAsync();
        }
        public async Task<IEnumerable<Payment>> GetPaymentsByDayAsync(int day, int month, int year)
        {
            return await _context.Payments
                .Where(p => p.PaymentDate.Day == day &&
                            p.PaymentDate.Month == month &&
                            p.PaymentDate.Year == year)
                .ToListAsync();
        }


    }

}
