using GabrieleMessina.AppHost.Extensions;

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("rediscache");

var postgres = builder.AddPostgres("postgres")
    // .WithDbGate()
    // .WithExternalHttpEndpoints()
    .WithPersistentStorage();

var postgresdb = postgres.AddDatabase("postgresdb");

var apiService = builder.AddProject<Projects.GabrieleMessina_ApiService>("apiservice")
    .WithReference(postgresdb)
    .WaitFor(postgresdb);

builder.AddProject<Projects.GabrieleMessina_Web>("webfrontend")
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();