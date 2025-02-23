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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MedicoCLS>()
                .HasOne<EspecialidadCLS>()
                .WithMany()
                .HasForeignKey(m => m.EspecialidadId);

            modelBuilder.Entity<CitaCLS>()
                .HasOne<PacienteCLS>()
                .WithMany()
                .HasForeignKey(c => c.PacienteId);

            modelBuilder.Entity<CitaCLS>()
                .HasOne<MedicoCLS>()
                .WithMany()
                .HasForeignKey(c => c.MedicoId);

            modelBuilder.Entity<TratamientoCLS>()
                .HasOne<PacienteCLS>()
                .WithMany()
                .HasForeignKey(t => t.PacienteId);

            modelBuilder.Entity<FacturacionCLS>()
                .HasOne<PacienteCLS>()
                .WithMany()
                .HasForeignKey(f => f.PacienteId);
        }
    }
}
