using MedicalApi.Database;
using MedicalApi.Database.Tables;
using Microsoft.AspNetCore.Mvc;

namespace MedicalApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DoctorController(MedicalDbContext db) : ControllerBase
    {
        [HttpGet(Name = "GetDoctorsList")]
        public IDictionary<string, IEnumerable<DoctorDto>> GetAll()
        {
            return db.Doctors.GroupBy(d => d.Specialty)
                             .ToDictionary(g => g.Key, 
                                           g => g.Select(i => new DoctorDto(i.Id, i.FullName, i.Specialty)));
        }

        public record struct DoctorDto(int Id, string Name, string Specialty)
        {
            public DoctorDto(Doctor doctor) 
                : this(doctor.Id, doctor.FullName, doctor.Specialty)
            {
            }
        }
    }
}
