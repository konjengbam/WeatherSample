var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.WeatherSample_ApiService>("apiservice");

builder.AddProject<Projects.WeatherSample_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
