using System;
using System.Collections.Generic;
using Demo06.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo06.Context;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Container> Containers { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Database=postgres;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("client_pkey");

            entity.ToTable("client", "demo06");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(70)
                .HasColumnName("email");
            entity.Property(e => e.Family)
                .HasMaxLength(70)
                .HasColumnName("family");
            entity.Property(e => e.Name)
                .HasMaxLength(70)
                .HasColumnName("name");
            entity.Property(e => e.Patronomic)
                .HasMaxLength(70)
                .HasColumnName("patronomic");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Container>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("container_pkey");

            entity.ToTable("container", "demo06");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(70)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("material_pkey");

            entity.ToTable("material", "demo06");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(70)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("orders_pkey");

            entity.ToTable("orders", "demo06");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContainerId).HasColumnName("container_id");
            entity.Property(e => e.Datecreate).HasColumnName("datecreate");
            entity.Property(e => e.MaterialId).HasColumnName("material_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Container).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ContainerId)
                .HasConstraintName("orders_container_id_fkey");

            entity.HasOne(d => d.Material).WithMany(p => p.Orders)
                .HasForeignKey(d => d.MaterialId)
                .HasConstraintName("orders_material_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("orders_user_id_fkey");

            entity.HasMany(d => d.Clients).WithMany(p => p.Orders)
                .UsingEntity<Dictionary<string, object>>(
                    "OrderClient",
                    r => r.HasOne<Client>().WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("order_client_client_id_fkey"),
                    l => l.HasOne<Order>().WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("order_client_order_id_fkey"),
                    j =>
                    {
                        j.HasKey("OrderId", "ClientId").HasName("order_client_pkey");
                        j.ToTable("order_client", "demo06");
                        j.IndexerProperty<int>("OrderId").HasColumnName("order_id");
                        j.IndexerProperty<int>("ClientId").HasColumnName("client_id");
                    });

            entity.HasMany(d => d.Services).WithMany(p => p.Orders)
                .UsingEntity<Dictionary<string, object>>(
                    "OrderService",
                    r => r.HasOne<Service>().WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("order_services_service_id_fkey"),
                    l => l.HasOne<Order>().WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("order_services_order_id_fkey"),
                    j =>
                    {
                        j.HasKey("OrderId", "ServiceId").HasName("order_services_pkey");
                        j.ToTable("order_services", "demo06");
                        j.IndexerProperty<int>("OrderId").HasColumnName("order_id");
                        j.IndexerProperty<int>("ServiceId").HasColumnName("service_id");
                    });
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_pkey");

            entity.ToTable("role", "demo06");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(70)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("services_pkey");

            entity.ToTable("services", "demo06");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cost)
                .HasPrecision(10, 2)
                .HasColumnName("cost");
            entity.Property(e => e.Name)
                .HasMaxLength(70)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pkey");

            entity.ToTable("user", "demo06");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Family)
                .HasMaxLength(70)
                .HasColumnName("family");
            entity.Property(e => e.Login)
                .HasMaxLength(70)
                .HasColumnName("login");
            entity.Property(e => e.Name)
                .HasMaxLength(70)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(70)
                .HasColumnName("password");
            entity.Property(e => e.Patronomic)
                .HasMaxLength(70)
                .HasColumnName("patronomic");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("user_role_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
