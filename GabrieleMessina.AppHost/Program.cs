var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var postgres = builder.AddPostgres("postgres")
                      .WithPgAdmin()
                      .WithDataVolume(isReadOnly: false);

var postgresdb = postgres.AddDatabase("postgresdb");

var apiService = builder.AddProject<Projects.GabrieleMessina_ApiService>("apiservice")
                        .WithReference(postgresdb)
                        .WaitFor(postgresdb);

builder.AddProject<Projects.GabrieleMessina_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
