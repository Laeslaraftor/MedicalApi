using MedicalApi.Database.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace MedicalApi.Database;

public static class Extensions
{
    extension(IHostApplicationBuilder builder)
    {
        public void AddMedicalDatabase()
        {
            builder.AddSqlServerDbContext<MedicalDbContext>(DbConstants.DatabaseName, configureDbContextOptions: options =>
            {
                options.UseAsyncSeeding(MedicalDbSeeding);
            });
        }

        private static async Task MedicalDbSeeding(DbContext context, bool _, CancellationToken cancellationToken)
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

            await db.Doctors.AddRangeAsync(doctors, cancellationToken);

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

            await db.Patients.AddRangeAsync(patients, cancellationToken);

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

            await db.Illnesses.AddRangeAsync(illnesses, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);

            List<PatientIllness> patientIllnesses =
            [
                new()
                {
                    PatientId = patients[0].Id,
                    DoctorId = doctors[0].Id,
                    IllnessId = illnesses[0].Id,
                    CreatedDate = DateTime.UtcNow.AddDays(-10),
                    Description = "Кашель, насморк, температура 37.5"
                },
                new()
                {
                    PatientId = patients[1].Id,
                    DoctorId = doctors[1].Id,
                    IllnessId = illnesses[1].Id,
                    CreatedDate = DateTime.UtcNow.AddDays(-5),
                    Description = "Давление 150/95, головные боли"
                },
                new()
                {
                    PatientId = patients[2].Id,
                    DoctorId = doctors[2].Id,
                    IllnessId = illnesses[2].Id,
                    CreatedDate = DateTime.UtcNow.AddDays(-2),
                    Description = "Пульсирующая головная боль, светобоязнь"
                }
            ];

            await db.PatientIllnesses.AddRangeAsync(patientIllnesses, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
        }
    }
}
