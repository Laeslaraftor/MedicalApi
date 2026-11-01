using Microsoft.EntityFrameworkCore;

namespace MedicalApi.Db
{
    public class MedicalDbContext(DbContextOptions<MedicalDbContext> options) : DbContext(options)
    {
    }
}
