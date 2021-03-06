﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DataAccess.Models;

#nullable disable

namespace DataAccess
{
    public partial class RestaurantContext : DbContext
    {
        public RestaurantContext()
        {
        }

        public RestaurantContext(DbContextOptions<RestaurantContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeTitle> EmployeeTitle { get; set; }
        public virtual DbSet<Food> Food { get; set; }
        public virtual DbSet<FoodCategory> FoodCategory { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<OrderLine> OrderLine { get; set; }
        public virtual DbSet<PaymentCondition> PaymentCondition { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Price> Price { get; set; }
        public virtual DbSet<Reservation> Reservation { get; set; }
        public virtual DbSet<ReservationsTables> ReservationsTables { get; set; }
        public virtual DbSet<RestaurantOrder> RestaurantOrder { get; set; }
        public virtual DbSet<RestaurantTables> RestaurantTables { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<ZipId> ZipId { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__Customer__person__403A8C7D");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Salary).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__Employee__person__3D5E1FD2");

                entity.HasOne(d => d.Title)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.TitleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Employee__title___3C69FB99");
            });

            modelBuilder.Entity<Food>(entity =>
            {
                entity.HasOne(d => d.FoodCategory)
                    .WithMany(p => p.Food)
                    .HasForeignKey(d => d.FoodCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Food__food_categ__29572725");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasOne(d => d.ZipCodeNavigation)
                    .WithMany(p => p.Location)
                    .HasForeignKey(d => d.ZipCode)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__Location__zip_co__31EC6D26");
            });

            modelBuilder.Entity<OrderLine>(entity =>
            {
                entity.HasKey(e => new { e.FoodId, e.OrderNumber })
                    .HasName("PK__OrderLin__013243EC87AE5D33");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.OrderLine)
                    .HasForeignKey(d => d.FoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderLine__food___52593CB8");

                entity.HasOne(d => d.OrderNumberNavigation)
                    .WithMany(p => p.OrderLine)
                    .HasForeignKey(d => d.OrderNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderLine__order__534D60F1");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Person__location__35BCFE0A");
            });

            modelBuilder.Entity<Price>(entity =>
            {
                entity.HasOne(d => d.Food)
                    .WithMany(p => p.Price)
                    .HasForeignKey(d => d.FoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Price__food_id__2C3393D0");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.Property(e => e.Deposit).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Reservation)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reservati__custo__440B1D61");
            });

            modelBuilder.Entity<ReservationsTables>(entity =>
            {
                entity.HasKey(e => new { e.ReservationId, e.RestaurantTablesId })
                    .HasName("PK__Reservat__55FAD9FAF5101936");

                entity.HasOne(d => d.Reservation)
                    .WithMany(p => p.ReservationsTables)
                    .HasForeignKey(d => d.ReservationId)
                    .HasConstraintName("FK__Reservati__reser__47DBAE45");

                entity.HasOne(d => d.RestaurantTables)
                    .WithMany(p => p.ReservationsTables)
                    .HasForeignKey(d => d.RestaurantTablesId)
                    .HasConstraintName("FK__Reservati__resta__46E78A0C");
            });

            modelBuilder.Entity<RestaurantOrder>(entity =>
            {
                entity.HasKey(e => e.OrderNo)
                    .HasName("PK__Restaura__0809CA8603716EA6");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.RestaurantOrder)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Restauran__emplo__4D94879B");

                entity.HasOne(d => d.PaymentCondition)
                    .WithMany(p => p.RestaurantOrder)
                    .HasForeignKey(d => d.PaymentConditionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Restauran__payme__4F7CD00D");

                entity.HasOne(d => d.Reservation)
                    .WithMany(p => p.RestaurantOrder)
                    .HasForeignKey(d => d.ReservationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Restauran__reser__4E88ABD4");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.PasswordHash).IsFixedLength(true);

                entity.Property(e => e.Salt).IsFixedLength(true);

                entity.HasOne(d => d.Person)
                    .WithOne(p => p.User)
                    .HasForeignKey<User>(d => d.PersonId)
                    .HasConstraintName("FK__User__PersonId__5812160E");
            });

            modelBuilder.Entity<ZipId>(entity =>
            {
                entity.HasKey(e => e.ZipCode)
                    .HasName("PK__Zip_id__FA8EDA75EF9D28B3");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}