using CO_Working_Space;
using coworking_space.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coworking_space.DAL.Repository.Interfaces {
    // Interfaces/IClientRepository.cs
    public interface IClientRepository : IGenericRepository<Client> { }

    // Interfaces/IExpensesRepository.cs
    public interface IExpensesRepository : IGenericRepository<Expenses> { }

    // Interfaces/IOrderRepository.cs
    public interface IOrderRepository : IGenericRepository<Order> { }

    // Interfaces/IOrderItemRepository.cs
    public interface IOrderItemRepository : IGenericRepository<OrderItem> { }

    // Interfaces/IPaymentRepository.cs
    public interface IPaymentRepository : IGenericRepository<Payment> { }

    // Interfaces/IPurchasedItemRepository.cs
    public interface IPurchasedItemRepository : IGenericRepository<PurchasedItem> { }

    // Interfaces/IReservationsRepository.cs
    public interface IReservationsRepository : IGenericRepository<Reservations> { }

    // Interfaces/IRoomRepository.cs
    public interface IRoomRepository : IGenericRepository<Room> { }

    // Interfaces/ISuplierRepository.cs
    public interface ISuplierRepository : IGenericRepository<Suplier> { }

}
