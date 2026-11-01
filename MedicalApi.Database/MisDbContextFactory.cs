using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MedicalApi.Database;

public class MisDbContextFactory : IDesignTimeDbContextFactory<MedicalDbContext>
{
    public MedicalDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<MedicalDbContext>()
            .UseSqlServer($"Server=localhost;Database={DbConstants.DatabaseName};Trusted_Connection=True;TrustServerCertificate=True")
            .Options;

        return new(options);
    }
}
