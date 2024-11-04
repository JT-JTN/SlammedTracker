var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ST_Api>("st-api");
builder.AddProject<Projects.ST_Ui>("st-ui");

builder.Build().Run();
