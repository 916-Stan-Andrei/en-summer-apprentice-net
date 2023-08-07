using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TMS.Models
{
    public partial class TicketMngDBContext : DbContext
    {
        public TicketMngDBContext()
        {        
        }

        public TicketMngDBContext(DbContextOptions<TicketMngDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<EventType> EventTypes { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<TicketCategory> TicketCategories { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlServer("Data Source=DESKTOP-EV3OH7I;Initial Catalog=TicketMngDB;User ID=stanbydrew;Password=stanbydrew;TrustServerCertificate=True;encrypt=false;");
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("event");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.EndDate)
                    .HasPrecision(6)
                    .HasColumnName("end_date");

                entity.Property(e => e.EventtypeId).HasColumnName("eventtype_id");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.StartDate)
                    .HasPrecision(6)
                    .HasColumnName("start_date");

                entity.HasOne(d => d.Eventtype)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.EventtypeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_event_event_type");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_event_location");
            });

            modelBuilder.Entity<EventType>(entity =>
            {
                entity.ToTable("event_type");

                entity.HasIndex(e => e.Name, "UQ__event_ty__72E12F1BB81C9A60")
                    .IsUnique();

                entity.Property(e => e.EventtypeId).HasColumnName("eventtype_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("location");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.Capacity).HasColumnName("capacity");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Type)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.NumberOfTickets).HasColumnName("number_of_tickets");

                entity.Property(e => e.OrderedAt)
                    .HasPrecision(6)
                    .HasColumnName("ordered_at");

                entity.Property(e => e.TicketcategoryId).HasColumnName("ticketcategory_id");

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("total_price");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_order_customer");

                entity.HasOne(d => d.Ticketcategory)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.TicketcategoryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_order_ticket_category");
            });

            modelBuilder.Entity<TicketCategory>(entity =>
            {
                entity.ToTable("ticket_category");

                entity.Property(e => e.TicketcategoryId).HasColumnName("ticketcategory_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.TicketCategories)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ticket_category_event");
            });


            modelBuilder.HasSequence("event_seq").IncrementsBy(50);

            modelBuilder.HasSequence("event_type_seq").IncrementsBy(50);

            modelBuilder.HasSequence("location_seq").IncrementsBy(50);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
