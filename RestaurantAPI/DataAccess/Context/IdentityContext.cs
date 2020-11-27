using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public partial class RestaurantContext : DbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity => entity.Property(e => e.Id).ValueGeneratedOnAdd());

            modelBuilder.Entity<Employee>(entity => entity.Property(e => e.Id).ValueGeneratedOnAdd());

            modelBuilder.Entity<Food>(entity => entity.Property(e => e.Id).ValueGeneratedOnAdd());

            modelBuilder.Entity<Location>(entity => entity.Property(e => e.Id).ValueGeneratedOnAdd());

            modelBuilder.Entity<Person>(entity => entity.Property(e => e.Id).ValueGeneratedOnAdd());

            modelBuilder.Entity<Price>(entity => entity.Property(e => e.Id).ValueGeneratedOnAdd());

            modelBuilder.Entity<Reservation>(entity => entity.Property(e => e.Id).ValueGeneratedOnAdd());

            modelBuilder.Entity<RestaurantOrder>(entity => entity.Property(e => e.OrderNo).ValueGeneratedOnAdd());

            modelBuilder.Entity<User>(entity => entity.Property(e => e.Id).ValueGeneratedOnAdd());

            modelBuilder.Entity<RestaurantTables>(entity => entity.Property(e => e.Id).ValueGeneratedOnAdd());

            modelBuilder.Entity<EmployeeTitle>(entity => entity.Property(e => e.Id).ValueGeneratedOnAdd());

            modelBuilder.Entity<FoodCategory>(entity => entity.Property(e => e.Id).ValueGeneratedOnAdd());

            modelBuilder.Entity<PaymentCondition>(entity => entity.Property(e => e.Id).ValueGeneratedOnAdd());

        }
    }
}
