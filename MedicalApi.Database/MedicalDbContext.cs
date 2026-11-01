using Microsoft.EntityFrameworkCore;
using MedicalApi.Database.Tables;

namespace MedicalApi.Database;

public class MedicalDbContext(DbContextOptions<MedicalDbContext> options) : DbContext(options)
{
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Illness> Illnesses => Set<Illness>();
    public DbSet<PatientIllness> PatientIllnesses => Set<PatientIllness>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Illness>()
            .HasIndex(d => d.Code).IsUnique();

        modelBuilder.Entity<PatientIllness>()
            .HasOne(pd => pd.Patient)
            .WithMany(p => p.Illnesses)
            .HasForeignKey(pd => pd.PatientId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<PatientIllness>()
            .HasOne(pd => pd.Doctor)
            .WithMany(d => d.Diagnoses)
            .HasForeignKey(pd => pd.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<PatientIllness>()
            .HasOne(pd => pd.Disease)
            .WithMany(d => d.PatientDiagnoses)
            .HasForeignKey(pd => pd.DiseaseId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
