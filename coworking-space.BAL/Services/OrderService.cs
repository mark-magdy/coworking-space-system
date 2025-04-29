using coworking_space.BAL.DTOs.OrderDTO;
using coworking_space.DAL.Data.Models;
using coworking_space.DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

<<<<<<< HEAD
namespace coworking_space.BAL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> _orderRepo;
        private readonly IGenericRepository<OrderItem> _orderItemRepo;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IGenericRepository<Order> orderRepo, IGenericRepository<OrderItem> orderItemRepo, IUnitOfWork unitOfWork)
        {
            _orderRepo = orderRepo;
            _orderItemRepo = orderItemRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Order> AddOrderAsync(OrderCreateDto dto)
{
    var order = new Order
    {
        CreatedAt = dto.CreatedAt,
        Order_Status = dto.Order_Status,
        TotalPrice = dto.TotalPrice,
        OrderDetails = dto.OrderDetails,
        UserId = dto.UserId,
        OrderItems = dto.OrderItems.Select(oi => new OrderItem
        {
            ProductId = oi.ProductId,
            Quantity = oi.Quantity,
            Price = oi.Price
        }).ToList()
    };

    await _orderRepo.AddAsync(order);
    await _unitOfWork.SaveChangesAsync();
    return order;
}

public async Task<IEnumerable<Order>> GetAllOrdersAsync()
{
    return await _orderRepo.GetAllAsync();
}

public async Task<Order> GetOrderByIdAsync(int id)
{
    return await _orderRepo.GetByIdAsync(id);
}

public async Task<bool> DeleteOrderAsync(int id)
{
    return await _orderRepo.DeleteAsync(id);
}
=======
namespace coworking_space.BAL.Services {
    public class OrderService : IOrderService {
        //private readonly IGenericRepository<Order> _orderRepo;
        //private readonly IGenericRepository<OrderItem> _orderItemRepo;
        //private readonly IUnitOfWork _unitOfWork;

        //public OrderService(IGenericRepository<Order> orderRepo, IGenericRepository<OrderItem> orderItemRepo, IUnitOfWork unitOfWork) {
        //    _orderRepo = orderRepo;
        //    _orderItemRepo = orderItemRepo;
        //    _unitOfWork = unitOfWork;
        //}

        //public async Task<Order> AddOrderAsync(OrderCreateDto dto) {
        //    var order = new Order
        //    {
        //        CreatedAt = dto.CreatedAt,
        //        Order_Status = dto.Order_Status,
        //        TotalPrice = dto.TotalPrice,
        //        OrderDetails = dto.OrderDetails,
        //        UserId = dto.UserId,
        //        OrderItems = dto.OrderItems.Select(oi => new OrderItem
        //        {
        //            ProductId = oi.ProductId,
        //            Quantity = oi.Quantity,
        //            Price = oi.Price
        //        }).ToList()
        //    };

        //    await _orderRepo.AddAsync(order);
        //    await _unitOfWork.SaveChangesAsync();
        //    return order;
        //}

        //public async Task<IEnumerable<Order>> GetAllOrdersAsync() {
        //    return await _orderRepo.GetAllAsync();
        //}

        //public async Task<Order> GetOrderByIdAsync(int id) {
        //    return await _orderRepo.GetByIdAsync(id);
        //}

        //public async Task<bool> DeleteOrderAsync(int id) {
        //    return await _orderRepo.DeleteAsync(id);
        //}
>>>>>>> df9c69004d4110000d5eff22561ce8881ab0103c
    }
}
