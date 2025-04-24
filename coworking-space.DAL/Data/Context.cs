using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using coworking_space.DAL.Data.Models;
using CO_Working_Space;

namespace coworking_space.DAL.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(o => o.Order_Status)
                .HasConversion<string>();
            modelBuilder.Entity<Product>()
                .Property(Product => Product.IsAvailable)
                .HasComputedColumnSql("CASE WHEN Quantity > 0 THEN 1 ELSE 0 END", stored:true );
            //modelBuilder.Entity<Room>()    to be considered
            //    .Property(r => r.IsAvailable)
            //    .HasComputedColumnSql("CASE WHEN CurrentCapacity < Capacity THEN 1 ELSE 0 END", stored: true);
            modelBuilder.Entity<Payment>()
    .HasOne(p => p.Order) // Navigation property in Payment
    .WithOne(o => o.Payment) // Navigation property in Order (make sure Order has a Payment property)
    .HasForeignKey<Payment>(p => p.OrderId)
    .OnDelete(DeleteBehavior.Restrict); // Avoid cascading delete


            modelBuilder.Entity<Payment>()
                      .HasOne(p=>p.TotalReservations)
                      .WithOne(t=>t.Payment)
                      .HasForeignKey<Payment>(p => p.TotalReservationsId)
                      .OnDelete(DeleteBehavior.Restrict);



        }

        public  DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ReservationOfRoom> ReservationOfRooms { get; set; }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Expenses> Expenses { get; set; }
        public DbSet<TotalReservations> TotalReservations { get; set; }
       
    }
}
    
