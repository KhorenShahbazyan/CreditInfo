using KhorenTest.CreditInfo.DataAccess.DbContexts;
using KhorenTest.CreditInfo.DataAccess.Entities;
using KhorenTest.CreditInfo.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace KhorenTest.CreditInfo.DataAccess.Repositories
{
    public class ContractRepository : Repository<Contract>, IContractRepository
    {
        public ContractRepository(KhorenTestDbContext dbContext, ILoggerFactory loggerFactory)
           : base(dbContext, loggerFactory.CreateLogger<ContractRepository>())
        {
        }
    }
}
