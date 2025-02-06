using Microsoft.Extensions.Configuration;

internal static class _connectionString
{
    private static IConfiguration Configuration { get; }

    static _connectionString()
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
} //Método de conexão com o banco de dados