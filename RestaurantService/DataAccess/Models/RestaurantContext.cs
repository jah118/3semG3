using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models
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
        public virtual DbSet<SchemaVersions> SchemaVersions { get; set; }
        public virtual DbSet<ZipId> ZipId { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb; Initial Catalog=Restaurant; Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__Customer__person__403A8C7D");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.Property(e => e.Salary)
                    .HasColumnName("salary")
                    .HasColumnType("decimal(18, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TitleId).HasColumnName("title_id");

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

            modelBuilder.Entity<EmployeeTitle>(entity =>
            {
                entity.ToTable("Employee_Title");

                entity.HasIndex(e => e.Title)
                    .HasName("UQ__Employee__E52A1BB3DF9A1243")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Food>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(64);

                entity.Property(e => e.FoodCategoryId).HasColumnName("food_category_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(32);

                entity.HasOne(d => d.FoodCategory)
                    .WithMany(p => p.Food)
                    .HasForeignKey(d => d.FoodCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Food__food_categ__29572725");
            });

            modelBuilder.Entity<FoodCategory>(entity =>
            {
                entity.ToTable("Food_Category");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(50);

                entity.Property(e => e.ZipCode)
                    .HasColumnName("zip_code")
                    .HasMaxLength(6);

                entity.HasOne(d => d.ZipCodeNavigation)
                    .WithMany(p => p.Location)
                    .HasForeignKey(d => d.ZipCode)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__Location__zip_co__31EC6D26");
            });

            modelBuilder.Entity<OrderLine>(entity =>
            {
                entity.HasKey(e => new { e.FoodId, e.OrderNumber })
                    .HasName("PK__OrderLin__013243EC14D17EA9");

                entity.Property(e => e.FoodId).HasColumnName("food_id");

                entity.Property(e => e.OrderNumber).HasColumnName("order_Number");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

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

            modelBuilder.Entity<PaymentCondition>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Condition)
                    .IsRequired()
                    .HasColumnName("condition")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasIndex(e => e.Phone)
                    .HasName("UQ__Person__B43B145F3298A2D0")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(50);

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasMaxLength(20);

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Person__location__35BCFE0A");
            });

            modelBuilder.Entity<Price>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FoodId).HasColumnName("food_id");

                entity.Property(e => e.PriceValue)
                    .HasColumnName("price_value")
                    .HasColumnType("decimal(19, 4)");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.Price)
                    .HasForeignKey(d => d.FoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Price__food_id__2C3393D0");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.Deposit)
                    .HasColumnName("deposit")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.NoOfPeople).HasColumnName("noOfPeople");

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasMaxLength(200);

                entity.Property(e => e.ReservationDate)
                    .HasColumnName("reservation_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ReservationTime).HasColumnName("reservationTime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Reservation)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reservati__custo__440B1D61");
            });

            modelBuilder.Entity<ReservationsTables>(entity =>
            {
                entity.HasKey(e => new { e.ReservationId, e.RestaurantTablesId })
                    .HasName("PK__Reservat__55FAD9FA2EF9B0E8");

                entity.ToTable("Reservations_Tables");

                entity.Property(e => e.ReservationId).HasColumnName("reservation_id");

                entity.Property(e => e.RestaurantTablesId).HasColumnName("restaurant_tables_id");

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
                    .HasName("PK__Restaura__0809CA86AFE0E0E1");

                entity.ToTable("Restaurant_Order");

                entity.Property(e => e.OrderNo).HasColumnName("orderNo");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.OrderDate)
                    .HasColumnName("orderDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.PaymentConditionId).HasColumnName("paymentCondition_id");

                entity.Property(e => e.ReservationId).HasColumnName("reservation_id");

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

            modelBuilder.Entity<RestaurantTables>(entity =>
            {
                entity.ToTable("Restaurant_Tables");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NoOfSeats).HasColumnName("noOfSeats");

                entity.Property(e => e.TableNumber).HasColumnName("table_number");
            });

            modelBuilder.Entity<SchemaVersions>(entity =>
            {
                entity.Property(e => e.Applied).HasColumnType("datetime");

                entity.Property(e => e.ScriptName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<ZipId>(entity =>
            {
                entity.HasKey(e => e.ZipCode)
                    .HasName("PK__Zip_id__FA8EDA7550072140");

                entity.ToTable("Zip_id");

                entity.Property(e => e.ZipCode)
                    .HasColumnName("zip_code")
                    .HasMaxLength(6);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}