using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using coworking_space.BAL.DTOs.OrderDTO;

namespace coworking_space.BAL.Interaces {
    public interface IOrderService {
        Task<OrderReadDto> AddOrderAsync(OrderCreateDto dto);
        Task<IEnumerable<OrderReadDto>> GetAllOrdersAsync();
        Task<OrderReadDto> GetOrderByIdAsync(int id);
        Task<OrderUpdateDto> UpdateOrder(int id, OrderUpdateDto value);
        Task<bool> DeleteOrderAsync(int id);

    }
}
