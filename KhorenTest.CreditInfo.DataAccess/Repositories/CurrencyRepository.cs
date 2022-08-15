using KhorenTest.CreditInfo.DataAccess.DbContexts;
using KhorenTest.CreditInfo.DataAccess.Entities;
using KhorenTest.CreditInfo.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace KhorenTest.CreditInfo.DataAccess.Repositories
{
    public class CurrencyRepository : Repository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(KhorenTestDbContext dbContext, ILoggerFactory loggerFactory)
           : base(dbContext, loggerFactory.CreateLogger<CurrencyRepository>())
        {
        }
    }
}
