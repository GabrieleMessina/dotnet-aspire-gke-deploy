var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("rediscache");

var postgres = builder.AddPostgres("postgres")
.WithDbGate()
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

try
{
    builder.Build().Run();
}
catch(Exception ex)
{
    Console.WriteLine(ex);
}
