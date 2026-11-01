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
            builder.AddSqlServerDbContext<MedicalDbContext>(DbConstants.DatabaseName);
        }
    }
}
