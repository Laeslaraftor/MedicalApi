using MedicalApi.Database;
using MedicalApi.Database.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedicalApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PatientController(ILogger<PatientController> logger, MedicalDbContext db) : ControllerBase
    {
        [HttpGet(Name = "GetPatientInformation")]
        public Result<PatientDto?> GetInformation(int patientId)
        {
            var patients = IncludeIllnesses(db.Patients).Where(p => p.Id == patientId)
                                                        .Select(p => new PatientDto(p));

            if (!patients.Any())
            {
                logger.LogWarning(PatientNotFoundMessageFormat, patientId);
                return Result<PatientDto?>.Fail("Patient not found");
            }

            return Result<PatientDto?>.Ok(patients.First());
        }
        [HttpGet(Name = "GetPatientsList")]
        public IEnumerable<PatientDto> GetAll()
        {
            return IncludeIllnesses(db.Patients).Select(p => new PatientDto(p));
        }

        private static IQueryable<Patient> IncludeIllnesses(DbSet<Patient> patients)
        {
            return patients.Include(p => p.Illnesses)
                           .ThenInclude(i => i.Illness)
                           .Include(p => p.Illnesses)
                           .ThenInclude(i => i.Doctor);
        }

        private const string PatientNotFoundMessageFormat = "Patient with requested id ({PatientId}) not found";

        public record struct PatientDto(int Id, string FullName, DateOnly BirthDate, Gender Gender, DateTime CreatedAt, IEnumerable<PatientIllnessDto> Illnesses)
        {
            public PatientDto(Patient patient)
                : this(patient.Id, patient.FullName, patient.BirthDate, patient.Gender, patient.CreatedAt, patient.Illnesses.Select(i => new PatientIllnessDto(i)))
            {
            }
        }
        public record struct PatientIllnessDto(IllnessController.IllnessDto Illness, DoctorController.DoctorDto Doctor, DateTime Created, string? Description)
        {
            public PatientIllnessDto(PatientIllness patientIllness)
                : this(new(patientIllness.Illness), new(patientIllness.Doctor), patientIllness.CreatedDate, patientIllness.Description)
            {
            }
        }
    }
}
