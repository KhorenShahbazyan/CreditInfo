using KhorenTest.CreditInfo.DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KhorenTest.CreditInfo.Business.Extensions
{
    public static class DiExtension
    {
        //[System.Obsolete]
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var defaultConnectionString = configuration.GetConnectionString("DefaultConnection"); 
              _ = services.AddDbContext<KhorenTestDbContext>(options => { options.UseSqlServer(defaultConnectionString, builder => builder.UseRowNumberForPaging()); });

        }
    }
}
