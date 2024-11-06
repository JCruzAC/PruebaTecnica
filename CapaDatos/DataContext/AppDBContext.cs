using CapaEntidades;
using Microsoft.EntityFrameworkCore;

namespace CapaDatos.DataContext;

public partial class AppDBContext : DbContext
{
    public AppDBContext()
    {
    }

    public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cargo> Cargos { get; set; }

    public virtual DbSet<Personal> Personals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.IdCargo);

            entity.ToTable("CARGO");

            entity.HasIndex(e => e.Descripcion, "IDX_CARGODESCRIPCION")
                .IsUnique()
                .HasFilter("([RegistroEliminado]=(0))");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.EstadoRegistro).HasDefaultValue((byte)1);
            entity.Property(e => e.Fyhcreacion)
                .HasColumnType("datetime")
                .HasColumnName("FYHCreacion");
        });

        modelBuilder.Entity<Personal>(entity =>
        {
            entity.HasKey(e => e.IdPersonal);

            entity.ToTable("PERSONAL");

            entity.HasIndex(e => e.Dni, "IDX_PERSONALDNI")
                .IsUnique()
                .HasFilter("([RegistroEliminado]=(0))");

            entity.HasIndex(e => new { e.Nombres, e.Apellidos }, "IDX_PERSONAL_NOMBRESAPELLIDOS")
                .IsUnique()
                .HasFilter("([RegistroEliminado]=(0))");

            entity.Property(e => e.Apellidos)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Dni)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.EstadoRegistro).HasDefaultValue((byte)1);
            entity.Property(e => e.Fyhcreacion)
                .HasColumnType("datetime")
                .HasColumnName("FYHCreacion");
            entity.Property(e => e.Nombres)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCargoNavigation).WithMany(p => p.Personals)
                .HasForeignKey(d => d.IdCargo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PERSONAL_CARGO");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
