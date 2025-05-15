using coworking_space.BAL.DTOs.PaymentDTO;
using coworking_space.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coworking_space.BAL.Interaces
{
    public interface IPaymentService
    {

        Task ProcessPaymentAsync(CreatPaymentDTO dto);
        Task ProcessBulkPaymentAsync(CreateBulkPaymentDTO dto);
        Task<decimal> GetMonthlyRevenueAsync(int month, int year);
        Task<decimal> GetDailyRevenueAsync(int day, int month, int year);


    }
}
