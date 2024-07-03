using ApiPointOfSales.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Aplikasi_Kota.Data_Model
{
    public static class ServiceExtension
    {
        public static ConfigurationManager Configuration { get; set; }
        public static void AddDomainContext(this IServiceCollection services, ConfigurationManager configuration)
        {
            Configuration = configuration;
            services.AddDbContext<DbTesAuriwanyasperContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DB_Conn"));
            });
        }
    }
}
