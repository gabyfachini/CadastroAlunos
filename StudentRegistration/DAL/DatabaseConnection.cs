using Microsoft.Extensions.Configuration;

internal static class DatabaseConnection
{
    private static IConfiguration Configuration { get; }

    static DatabaseConnection()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json");
        Configuration = builder.Build();
    }

    public static string GetConnectionString(string name)
    {
        return Configuration.GetConnectionString(name);
    }
}