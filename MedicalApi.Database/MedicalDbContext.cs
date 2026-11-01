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
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSeeding(MedicalDbSeeding);
    }

    // Тестовые данные сгенерированы иишкой!
    private static void MedicalDbSeeding(DbContext context, bool _)
    {
        if (context is not MedicalDbContext db || db.Doctors.Any())
        {
            return;
        }

        List<Doctor> doctors =
        [
            new()
            {
                FullName = "Иванов Иван Иванович",
                Specialty = "Терапевт"
            },
            new()
            {
                FullName = "Петрова Анна Сергеевна",
                Specialty = "Кардиолог"
            },
            new()
            {
                FullName = "Сидоров Петр Алексеевич",
                Specialty = "Невролог"
            }
        ];

        db.Doctors.AddRange(doctors);

        List<Patient> patients =
        [
            new()
            {
                FullName = "Смирнов Алексей Владимирович",
                BirthDate = new DateOnly(1985, 3, 12),
                Gender = Gender.Male
            },
            new()
            {
                FullName = "Кузнецова Мария Игоревна",
                BirthDate = new DateOnly(1990, 7, 25),
                Gender = Gender.Female
            },
            new()
            {
                FullName = "Попов Дмитрий Олегович",
                BirthDate = new DateOnly(1978, 11, 5),
                Gender = Gender.Male
            }
        ];

        db.Patients.AddRange(patients);

        List<Illness> illnesses =
        [
            new()
            {
                Code = "J06.9",
                Name = "Острая инфекция верхних дыхательных путей"
            },
            new()
            {
                Code = "I10",
                Name = "Эссенциальная (первичная) гипертензия"
            },
            new()
            {
                Code = "G43.9",
                Name = "Мигрень неуточненная"
            },
            new()
            {
                Code = "J45.9",
                Name = "Астма неуточненная"
            }
        ];

        db.Illnesses.AddRange(illnesses);

        db.SaveChanges();

        List<PatientIllness> diagnoses =
        [
            new()
            {
                PatientId = patients[0].Id,
                DoctorId = doctors[0].Id,
                DiseaseId = illnesses[0].Id,
                CreatedDate = DateTime.UtcNow.AddDays(-10),
                Description = "Кашель, насморк, температура 37.5"
            },
            new()
            {
                PatientId = patients[1].Id,
                DoctorId = doctors[1].Id,
                DiseaseId = illnesses[1].Id,
                CreatedDate = DateTime.UtcNow.AddDays(-5),
                Description = "Давление 150/95, головные боли"
            },
            new()
            {
                PatientId = patients[2].Id,
                DoctorId = doctors[2].Id,
                DiseaseId = illnesses[2].Id,
                CreatedDate = DateTime.UtcNow.AddDays(-2),
                Description = "Пульсирующая головная боль, светобоязнь"
            }
        ];

        db.PatientIllnesses.AddRange(diagnoses);

        db.SaveChanges();
    }
}
