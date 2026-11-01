using MedicalApi.Database;
using MedicalApi.Migration;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<MigrationWorker>();
builder.AddMedicalDatabase();

var host = builder.Build();
host.Run();
