using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TMS.Models;

public partial class TicketMngDbContext : DbContext
{
    public TicketMngDbContext()
    {
    }

    public TicketMngDbContext(DbContextOptions<TicketMngDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventType> EventTypes { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<TicketCategory> TicketCategories { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-EV3OH7I;Initial Catalog=TicketMngDB;User ID=stanbydrew;Password=stanbydrew;TrustServerCertificate=True;encrypt=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__customer__CD65CB85A194F681");

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
            entity.HasKey(e => e.EventId).HasName("PK__event__2370F727F8314313");

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

            entity.HasOne(d => d.Eventtype).WithMany(p => p.Events)
                .HasForeignKey(d => d.EventtypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_event_event_type");

            entity.HasOne(d => d.Location).WithMany(p => p.Events)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_event_location");
        });

        modelBuilder.Entity<EventType>(entity =>
        {
            entity.HasKey(e => e.EventtypeId).HasName("PK__event_ty__9B0276A456B65B5A");

            entity.ToTable("event_type");

            entity.HasIndex(e => e.Name, "UQ__event_ty__72E12F1BB81C9A60").IsUnique();

            entity.Property(e => e.EventtypeId).HasColumnName("eventtype_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK__location__771831EA272170DA");

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
            entity.HasKey(e => e.OrderId).HasName("PK__order__46596229CE28256A");

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

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_order_customer");

            entity.HasOne(d => d.Ticketcategory).WithMany(p => p.Orders)
                .HasForeignKey(d => d.TicketcategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_order_ticket_category");
        });

        modelBuilder.Entity<TicketCategory>(entity =>
        {
            entity.HasKey(e => e.TicketcategoryId).HasName("PK__ticket_c__1617D62A4BECB617");

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

            entity.HasOne(d => d.Event).WithMany(p => p.TicketCategories)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ticket_category_event");
        });

    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
