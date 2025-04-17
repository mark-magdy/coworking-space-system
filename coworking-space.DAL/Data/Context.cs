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
            
        }

        public  DbSet<User> Clients { get; set; }
       public DbSet<Order> Orders { get; set; }
    }
}
    
