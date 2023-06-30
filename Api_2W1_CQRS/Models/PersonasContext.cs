using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Api_2W1_CQRS.Models;

public partial class PersonasContext : DbContext
{
    public PersonasContext()
    {
    }

    public PersonasContext(DbContextOptions<PersonasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Pai> Pais { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Provincium> Provincia { get; set; }

    public virtual DbSet<Sexo> Sexos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost; Database=Personas; Port=5432; User Id=postgres; Password=123456;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pai>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Pais_pkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Pais).HasColumnName("pais");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Persona_pkey");

            entity.ToTable("Persona");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellido).HasColumnName("apellido");
            entity.Property(e => e.Dni).HasColumnName("dni");
            entity.Property(e => e.IdPais).HasColumnName("id_pais");
            entity.Property(e => e.IdProvincia).HasColumnName("id_provincia");
            entity.Property(e => e.IdSexo).HasColumnName("id_sexo");
            entity.Property(e => e.Nombre).HasColumnName("nombre");

            entity.HasOne(d => d.IdPaisNavigation).WithMany(p => p.Personas)
                .HasForeignKey(d => d.IdPais)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pais");

            entity.HasOne(d => d.IdProvinciaNavigation).WithMany(p => p.Personas)
                .HasForeignKey(d => d.IdProvincia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_provincia");

            entity.HasOne(d => d.IdSexoNavigation).WithMany(p => p.Personas)
                .HasForeignKey(d => d.IdSexo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_sexo");
        });

        modelBuilder.Entity<Provincium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Provincia_pkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Provincia).HasColumnName("provincia");
        });

        modelBuilder.Entity<Sexo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Sexo_pkey");

            entity.ToTable("Sexo");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('\"Sexo_Id_seq\"'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Sexo1).HasColumnName("sexo");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Usuario_pkey");

            entity.ToTable("Usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Usuario1).HasColumnName("usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
