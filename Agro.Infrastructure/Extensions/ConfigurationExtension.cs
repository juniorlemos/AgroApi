using Microsoft.Extensions.Configuration;

namespace Agro.Infrastructure.Extensions
{
    public static class ConfigurationExtension
    {

        public static string ConnectionString(this IConfiguration configurarion)
        {
            return configurarion.GetConnectionString("ConnectionSQLServer")!;
        }
    }
}