using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Data.EF;

public partial class AzureListDbContext : DbContext
{
    public AzureListDbContext()
    {
    }

    public AzureListDbContext(DbContextOptions<AzureListDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Prioridad> Prioridads { get; set; }

    public virtual DbSet<Tablero> Tableros { get; set; }

    public virtual DbSet<Tarea> Tareas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=azurelist.database.windows.net;Database=AzureListDB;User Id=adminazurelist;Password=Azurelist3493;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Prioridad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Priorida__3214EC07B4A0C6F7");

            entity.ToTable("Prioridad");

            entity.Property(e => e.Nombre).HasMaxLength(255);
        });

        modelBuilder.Entity<Tablero>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tablero__3214EC07A64D731C");

            entity.ToTable("Tablero");

            entity.Property(e => e.Nombre).HasMaxLength(255);
        });

        modelBuilder.Entity<Tarea>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tarea__3214EC0714F79F34");

            entity.ToTable("Tarea");

            entity.Property(e => e.Titulo).HasMaxLength(255);

            entity.HasOne(d => d.IdPrioridadNavigation).WithMany(p => p.Tareas)
                .HasForeignKey(d => d.IdPrioridad)
                .HasConstraintName("FK__Tarea__IdEstado__3B75D760");

            entity.HasOne(d => d.IdTableroNavigation).WithMany(p => p.Tareas)
                .HasForeignKey(d => d.IdTablero)
                .HasConstraintName("FK__Tarea__IdTablero__6383C8BA");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
