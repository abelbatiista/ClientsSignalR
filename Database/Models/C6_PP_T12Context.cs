using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Database.Models
{
    public partial class C6_PP_T12Context : DbContext
    {
        public C6_PP_T12Context()
        {
        }

        public C6_PP_T12Context(DbContextOptions<C6_PP_T12Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Dessert> Desserts { get; set; }
        public virtual DbSet<Drink> Drinks { get; set; }
        public virtual DbSet<MainPlate> MainPlates { get; set; }
        public virtual DbSet<RestaurantOrder> RestaurantOrders { get; set; }
        public virtual DbSet<Starter> Starters { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#pragma warning disable CS1030 // #warning directive
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=C6_PP_T12;Trusted_Connection=True;");
#pragma warning restore CS1030 // #warning directive
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client");

                entity.Property(e => e.ClientId).HasColumnName("clientId");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Dessert>(entity =>
            {
                entity.ToTable("Dessert");

                entity.Property(e => e.DessertId).HasColumnName("dessertId");

                entity.Property(e => e.Cost)
                    .HasColumnType("money")
                    .HasColumnName("cost");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Drink>(entity =>
            {
                entity.ToTable("Drink");

                entity.Property(e => e.DrinkId).HasColumnName("drinkId");

                entity.Property(e => e.Cost)
                    .HasColumnType("money")
                    .HasColumnName("cost");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<MainPlate>(entity =>
            {
                entity.ToTable("MainPlate");

                entity.Property(e => e.MainPlateId).HasColumnName("mainPlateId");

                entity.Property(e => e.Cost)
                    .HasColumnType("money")
                    .HasColumnName("cost");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<RestaurantOrder>(entity =>
            {
                entity.ToTable("RestaurantOrder");

                entity.Property(e => e.RestaurantOrderId).HasColumnName("restaurantOrderId");

                entity.Property(e => e.ClientId).HasColumnName("clientId");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DessertId).HasColumnName("dessertId");

                entity.Property(e => e.DrinkId).HasColumnName("drinkId");

                entity.Property(e => e.Itbis)
                    .HasColumnType("money")
                    .HasColumnName("itbis");

                entity.Property(e => e.MainPlateId).HasColumnName("mainPlateId");

                entity.Property(e => e.StarterId).HasColumnName("starterId");

                entity.Property(e => e.StatusId)
                    .HasColumnName("statusId")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Subtotal)
                    .HasColumnType("money")
                    .HasColumnName("subtotal");

                entity.Property(e => e.Total)
                    .HasColumnType("money")
                    .HasColumnName("total");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.RestaurantOrders)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Restauran__clien__35BCFE0A");

                entity.HasOne(d => d.Dessert)
                    .WithMany(p => p.RestaurantOrders)
                    .HasForeignKey(d => d.DessertId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Restauran__desse__38996AB5");

                entity.HasOne(d => d.Drink)
                    .WithMany(p => p.RestaurantOrders)
                    .HasForeignKey(d => d.DrinkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Restauran__drink__398D8EEE");

                entity.HasOne(d => d.MainPlate)
                    .WithMany(p => p.RestaurantOrders)
                    .HasForeignKey(d => d.MainPlateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Restauran__mainP__37A5467C");

                entity.HasOne(d => d.Starter)
                    .WithMany(p => p.RestaurantOrders)
                    .HasForeignKey(d => d.StarterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Restauran__start__36B12243");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.RestaurantOrders)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Restauran__statu__3A81B327");
            });

            modelBuilder.Entity<Starter>(entity =>
            {
                entity.ToTable("Starter");

                entity.Property(e => e.StarterId).HasColumnName("starterId");

                entity.Property(e => e.Cost)
                    .HasColumnType("money")
                    .HasColumnName("cost");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.Property(e => e.StatusId).HasColumnName("statusId");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
