var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.MedicalApi>("medicalapi");

builder.AddProject<Projects.MigrationService>("migrationservice");

builder.Build().Run();
