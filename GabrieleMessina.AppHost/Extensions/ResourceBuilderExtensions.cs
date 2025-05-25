namespace GabrieleMessina.AppHost.Extensions;

public static class ResourceBuilderExtensions
{
    public static IResourceBuilder<PostgresServerResource> WithPersistentStorage(this IResourceBuilder<PostgresServerResource> builder, bool isReadOnly = false)
    {
        const string pgDataEnvVar = "/var/lib/postgresql/data/aspirate";
        ArgumentNullException.ThrowIfNull(builder);
        
        builder.WithEnvironment("PGDATA", pgDataEnvVar);
        return builder.WithDataVolume();
    }   
}