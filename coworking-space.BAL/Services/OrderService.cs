using coworking_space.BAL.DTOs.OrderDTO;
using coworking_space.BAL.Interaces;
using coworking_space.DAL.Data.Models;
using coworking_space.DAL.Repository.Implementations;
using coworking_space.DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coworking_space.BAL.Services {
    public class OrderService : IOrderService {
        private readonly IOrderRepository _orderRepo;
        private readonly IOrderItemRepository _orderItemRepo;
        //private readonly IUnitOfWork _unitOfWork;

        public OrderService(IOrderRepository orderRepo, IOrderItemRepository orderItemRepo) {
            _orderRepo = orderRepo;
            _orderItemRepo = orderItemRepo;
            //_unitOfWork = unitOfWork;
        }
        public async Task<OrderReadDto> AddOrderAsync(OrderCreateDto dto) {

            // Map the OrderCreateDto to an Order entity
            var order = new Order
            {
                CreatedAt = dto.CreatedAt,
                Order_Status = dto.Order_Status,
                TotalPrice = dto.TotalPrice,
                OrderDetails = dto.OrderDetails,
                UserId = dto.UserId,
                OrderItems = new List<OrderItem>() // Ensure it's initialized
            };

            // Add items individually to the collection
            foreach (var item in dto.OrderItems) {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                });
            }

            // Add the order to the repository
            var savedOrdered = await _orderRepo.AddAsync(order);
            await _orderRepo.SaveAsync();
            //_orderItemRepo.SaveAsync();

            // Return the created order as a DTO
            return new OrderReadDto
            {
                Id = savedOrdered.Id,
                CreatedAt = savedOrdered.CreatedAt,
                Order_Status = savedOrdered.Order_Status,
                TotalPrice = savedOrdered.TotalPrice,
                OrderDetails = savedOrdered.OrderDetails,
                UserId = savedOrdered.UserId,
                OrderItems = savedOrdered.OrderItems.Select(item => new OrderItemReadDto
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList()
            };
        }
        public async Task<IEnumerable<OrderReadDto>> GetAllOrdersAsync() {
            // Retrieve all orders from the repository  
            var orders = await _orderRepo.GetAllAsync();
            if(orders==null) 
                throw new KeyNotFoundException("No orders found.");
            // Map the orders to a list of OrderReadDto  
            return orders.Select(order => new OrderReadDto
            {
                Id = order.Id,
                CreatedAt = order.CreatedAt,
                Order_Status = order.Order_Status,
                TotalPrice = order.TotalPrice,
                OrderDetails = order.OrderDetails,
                InOut = order.InOut,
                UserId = order.UserId,
                UserName = order.User?.Name,
                OrderItems = order.OrderItems.Select(item => new OrderItemReadDto
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList()
            });
        }
        public async Task<OrderReadDto> GetOrderByIdAsync(int id) {
            // Retrieve the order by ID from the repository  
            var order = await _orderRepo.GetByIdAsync(id);
            if (order == null) {
                throw new KeyNotFoundException($"Order with ID {id} not found.");
            }

            // Map the order to an OrderReadDto  
            return new OrderReadDto
            {
                Id = order.Id,
                CreatedAt = order.CreatedAt,
                Order_Status = order.Order_Status,
                TotalPrice = order.TotalPrice,
                OrderDetails = order.OrderDetails,
                InOut = order.InOut,
                UserId = order.UserId,
                UserName = order.User?.Name,
                OrderItems = order.OrderItems.Select(item => new OrderItemReadDto
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList()
            };
        }
        public async Task<bool> DeleteOrderAsync(int id) {
            await _orderRepo.DeleteByIdAsync(id);
            var deletedItem = _orderRepo.GetByIdAsync(id);
            if (deletedItem == null)
                return true;
            else return false; 
        }
        public async Task<OrderUpdateDto> UpdateOrder(int id, OrderUpdateDto value) {
            // Retrieve the existing order by ID
            var order = await _orderRepo.GetByIdAsync(id);
            if (order == null) {
                throw new KeyNotFoundException($"Order with ID {id} not found.");
            }

            // Update the order's properties with the new values
            order.Order_Status = value.Order_Status;
            order.OrderDetails = value.OrderDetails;
            order.InOut = value.InOut;

            // Update the order in the repository
            _orderRepo.Update(order);

            // Save changes to the database
            await _orderRepo.SaveAsync();

            // Return the updated order as a DTO
            return new OrderUpdateDto
            {
                Order_Status = order.Order_Status,
                OrderDetails = order.OrderDetails,
                InOut = order.InOut
            };
        }
        
    }

}
