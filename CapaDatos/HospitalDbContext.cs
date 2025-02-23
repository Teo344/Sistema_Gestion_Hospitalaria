using Microsoft.EntityFrameworkCore;
using CapaEntidades;

namespace CapaDatos
{
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }

        public DbSet<PacienteCLS> Pacientes { get; set; }
        public DbSet<MedicoCLS> Medicos { get; set; }
        public DbSet<EspecialidadCLS> Especialidades { get; set; }
        public DbSet<CitaCLS> Citas { get; set; }
        public DbSet<TratamientoCLS> Tratamientos { get; set; }
        public DbSet<FacturacionCLS> Facturaciones { get; set; }
        public DbSet<AdministradorCLS> Administradores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MedicoCLS>()
                .HasOne(m => m.Especialidad)
                .WithMany()
                .HasForeignKey(m => m.EspecialidadId);

            modelBuilder.Entity<CitaCLS>()
                .HasOne(c => c.Paciente)
                .WithMany()
                .HasForeignKey(c => c.PacienteId);

            modelBuilder.Entity<CitaCLS>()
                .HasOne(c => c.Medico)
                .WithMany()
                .HasForeignKey(c => c.MedicoId);

            modelBuilder.Entity<TratamientoCLS>()
                .HasOne(t => t.Paciente)
                .WithMany()
                .HasForeignKey(t => t.PacienteId);

            modelBuilder.Entity<FacturacionCLS>()
                .HasOne(f => f.Paciente)
                .WithMany()
                .HasForeignKey(f => f.PacienteId);

            modelBuilder.Entity<PacienteCLS>()
                .HasIndex(p => p.Email)
                .IsUnique();

            modelBuilder.Entity<MedicoCLS>()
                .HasIndex(m => m.Email)
                .IsUnique();

            modelBuilder.Entity<AdministradorCLS>()
                .HasIndex(a => a.Email)
                .IsUnique();
        }
    }
}
