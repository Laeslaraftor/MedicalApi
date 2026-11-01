using MedicalApi.Database;
using MedicalApi.Database.Tables;
using Microsoft.AspNetCore.Mvc;

namespace MedicalApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class IllnessController(MedicalDbContext db) : ControllerBase
    {
        [HttpGet(Name = "GetIllnessesList")]
        public IEnumerable<IllnessDto> GetAll()
        {
            return db.Illnesses.Select(i => new IllnessDto(i));
        }

        public record struct IllnessDto(int Id, string Code, string Name)
        {
            public IllnessDto(Illness illness) 
                : this(illness.Id, illness.Code, illness.Name)
            {
            }
        }
    }
}
