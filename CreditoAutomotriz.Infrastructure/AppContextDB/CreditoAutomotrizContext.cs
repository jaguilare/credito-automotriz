using System;
using CreditoAutomotriz.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CreditoAutomotriz.Infrastructure.AppContextDB
{
    public partial class CreditoAutomotrizContext : DbContext
    {
        public CreditoAutomotrizContext()
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public CreditoAutomotrizContext(DbContextOptions<CreditoAutomotrizContext> options)
            : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<ClientesPatiosAutos> ClientesPatiosAutos { get; set; }
        public virtual DbSet<Ejecutivos> Ejecutivos { get; set; }
        public virtual DbSet<MarcasVehiculos> MarcasVehiculos { get; set; }
        public virtual DbSet<PatiosAutos> PatiosAutos { get; set; }
        public virtual DbSet<Personas> Personas { get; set; }
        public virtual DbSet<SolicitudCredito> SolicitudCredito { get; set; }
        public virtual DbSet<Vehiculos> Vehiculos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost,1433; Database=CreditoAutomotriz; User=sa; Password=abc123..;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clientes>(entity =>
            {
                entity.HasKey(e => e.IdClientes)
                    .HasName("Clientes_PK");

                entity.Property(e => e.IdClientes).ValueGeneratedNever();

                entity.Property(e => e.EstadoCivil)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IdentificacionConyuge).HasMaxLength(15);

                entity.Property(e => e.NombresConyuge).HasMaxLength(100);

                entity.HasOne(d => d.IdClientesNavigation)
                    .WithOne(p => p.Clientes)
                    .HasForeignKey<Clientes>(d => d.IdClientes)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Clientes_FK");
            });

            modelBuilder.Entity<ClientesPatiosAutos>(entity =>
            {
                entity.HasKey(e => e.IdClientesPatiosAutos)
                    .HasName("Clientes_PatiosAutos_PK");

                entity.ToTable("Clientes_PatiosAutos");

                entity.Property(e => e.FechaAsignacion).HasColumnType("datetime");

                entity.HasOne(d => d.IdClientesNavigation)
                    .WithMany(p => p.ClientesPatiosAutos)
                    .HasForeignKey(d => d.IdClientes)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Clientes_PatiosAutos_FK");

                entity.HasOne(d => d.IdPatiosAutosNavigation)
                    .WithMany(p => p.ClientesPatiosAutos)
                    .HasForeignKey(d => d.IdPatiosAutos)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Clientes_PatiosAutos_FK_1");
            });

            modelBuilder.Entity<Ejecutivos>(entity =>
            {
                entity.HasKey(e => e.IdEjecutivos)
                    .HasName("Ejecutivos_PK");

                entity.HasIndex(e => e.Celular)
                    .HasName("Ejecutivos_UN")
                    .IsUnique();

                entity.Property(e => e.IdEjecutivos).ValueGeneratedNever();

                entity.Property(e => e.Celular)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.IdEjecutivosNavigation)
                    .WithOne(p => p.Ejecutivos)
                    .HasForeignKey<Ejecutivos>(d => d.IdEjecutivos)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Ejecutivos_FK");

                entity.HasOne(d => d.IdPatiosAutosNavigation)
                    .WithMany(p => p.Ejecutivos)
                    .HasForeignKey(d => d.IdPatiosAutos)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Ejecutivos_PatiosAutos_FK");
            });

            modelBuilder.Entity<MarcasVehiculos>(entity =>
            {
                entity.HasKey(e => e.IdMarcasVehiculos)
                    .HasName("MarcasVehiculos_PK");

                entity.HasIndex(e => e.Marca)
                    .HasName("MarcasVehiculos_UN")
                    .IsUnique();

                entity.Property(e => e.Marca)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PatiosAutos>(entity =>
            {
                entity.HasKey(e => e.IdPationsAutos)
                    .HasName("PatiosAutos_PK");

                entity.HasIndex(e => e.NroPuntoVenta)
                    .HasName("PatiosAutos_UN")
                    .IsUnique();

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<Personas>(entity =>
            {
                entity.HasKey(e => e.IdPersonas)
                    .HasName("Personas_PK");

                entity.HasIndex(e => e.Identificacion)
                    .HasName("Personas_UN")
                    .IsUnique();

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.FechaNacimiento).HasColumnType("datetime");

                entity.Property(e => e.Identificacion)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<SolicitudCredito>(entity =>
            {
                entity.HasKey(e => e.IdSolicitudCredito)
                    .HasName("SolicitudCredito_PK");

                entity.Property(e => e.Cuotas).HasColumnType("numeric(38, 0)");

                entity.Property(e => e.Entrada).HasColumnType("numeric(38, 0)");

                entity.Property(e => e.FechaElaboracion).HasColumnType("datetime");

                entity.Property(e => e.Observacion).HasMaxLength(100);

                entity.HasOne(d => d.IdClientesNavigation)
                    .WithMany(p => p.SolicitudCredito)
                    .HasForeignKey(d => d.IdClientes)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SolicitudCredito_FK");

                entity.HasOne(d => d.IdEjecutivosNavigation)
                    .WithMany(p => p.SolicitudCredito)
                    .HasForeignKey(d => d.IdEjecutivos)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SolicitudCredito_Ejecutivos_FK");

                entity.HasOne(d => d.IdPatiosAustosNavigation)
                    .WithMany(p => p.SolicitudCredito)
                    .HasForeignKey(d => d.IdPatiosAustos)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SolicitudCredito_FK_1");

                entity.HasOne(d => d.IdVehiculosNavigation)
                    .WithMany(p => p.SolicitudCredito)
                    .HasForeignKey(d => d.IdVehiculos)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SolicitudCredito_FK_2");
            });

            modelBuilder.Entity<Vehiculos>(entity =>
            {
                entity.HasKey(e => e.IdVehiculos)
                    .HasName("Vehiculos_PK");

                entity.HasIndex(e => e.NroChasis)
                    .HasName("Vehiculos_UN_NroChasis")
                    .IsUnique();

                entity.HasIndex(e => e.Placa)
                    .HasName("Vehiculos_UN")
                    .IsUnique();

                entity.Property(e => e.Avaluo).HasColumnType("numeric(38, 0)");

                entity.Property(e => e.Cilindraje)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Modelo)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.NroChasis)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Placa)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Tipo).HasMaxLength(50);

                entity.HasOne(d => d.IdMarcaVehiculoNavigation)
                    .WithMany(p => p.Vehiculos)
                    .HasForeignKey(d => d.IdMarcaVehiculo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Vehiculos_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
