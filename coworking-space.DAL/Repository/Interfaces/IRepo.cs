using CO_Working_Space;
using coworking_space.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coworking_space.DAL.Repository.Interfaces {
    // Interfaces/IClientRepository.cs

    // Interfaces/IExpensesRepository.cs
    public interface IExpensesRepository : IGenericRepository<Expenses> { }

    // Interfaces/IOrderRepository.cs
    public interface IOrderRepository : IGenericRepository<Order> { }

    // Interfaces/IOrderItemRepository.cs
    public interface IOrderItemRepository : IGenericRepository<OrderItem> { 
    }

    // Interfaces/IPaymentRepository.cs
    public interface IPaymentRepository : IGenericRepository<Payment> { }

    // Interfaces/IPurchasedItemRepository.cs
    //public interface IPurchasedItemRepository : IGenericRepository<PurchasedItem> { }
    


    // Interfaces/IReservationsRepository.cs
  

    // Interfaces/IRoomRepository.cs
   

    // Interfaces/ISuplierRepository.cs
    public interface ISuplierRepository : IGenericRepository<Supplier> { }
    public interface IProductRepository : IGenericRepository<Product> { }

}
