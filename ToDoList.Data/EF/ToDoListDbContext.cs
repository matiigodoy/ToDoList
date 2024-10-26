using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Data.EF;

public partial class ToDoListDbContext : DbContext
{
    public ToDoListDbContext()
    {
    }

    public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Tablero> Tableros { get; set; }

    public virtual DbSet<Tarea> Tareas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:todolistpw3.database.windows.net,1433;Initial Catalog=ToDoListDB;Persist Security Info=False;User ID=adminpw3;Password=Todolistpw3;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Estado__3214EC07A3ED1107");

            entity.ToTable("Estado");

            entity.Property(e => e.Nombre).HasMaxLength(255);
        });

        modelBuilder.Entity<Tablero>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tablero__3214EC07A64D731C");

            entity.ToTable("Tablero");

            entity.Property(e => e.Nombre).HasMaxLength(255);

            entity.HasOne(d => d.PropietarioNavigation).WithMany(p => p.Tableros)
                .HasForeignKey(d => d.Propietario)
                .HasConstraintName("FK__Tablero__Propiet__5EBF139D");
        });

        modelBuilder.Entity<Tarea>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tarea__3214EC07855F1539");

            entity.ToTable("Tarea");

            entity.Property(e => e.Titulo).HasMaxLength(255);

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Tareas)
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("FK__Tarea__IdEstado__6477ECF3");

            entity.HasOne(d => d.IdTableroNavigation).WithMany(p => p.Tareas)
                .HasForeignKey(d => d.IdTablero)
                .HasConstraintName("FK__Tarea__IdTablero__6383C8BA");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3214EC07C94CDA6B");

            entity.ToTable("Usuario");

            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
