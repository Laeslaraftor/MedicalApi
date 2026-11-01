using MedicalApi.Database;

var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql").WithDataVolume();
var medicalDb = sql.AddDatabase(DbConstants.DatabaseName);

var migrationWorker = builder.AddProject<Projects.MedicalApi_Migration>("medicalapi-migration")
    .WithReference(medicalDb)
    .WaitFor(medicalDb);

builder.AddProject<Projects.MedicalApi>("medicalapi")
    .WithReference(medicalDb)
    .WaitForCompletion(migrationWorker);

builder.Build().Run();
