using GabrieleMessina.AppHost.Extensions;

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("rediscache");

var dbuser = builder.AddParameter("dbuser", "postgres");
var dbpsw = builder.AddParameter("dbpsw", "1234", false, true);
var postgres = builder.AddPostgres("postgres", dbuser, dbpsw)
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