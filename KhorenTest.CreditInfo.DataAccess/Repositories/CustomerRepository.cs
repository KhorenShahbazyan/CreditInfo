using KhorenTest.CreditInfo.DataAccess.DbContexts;
using KhorenTest.CreditInfo.DataAccess.Entities;
using KhorenTest.CreditInfo.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace KhorenTest.CreditInfo.DataAccess.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(KhorenTestDbContext dbContext, ILoggerFactory loggerFactory)
           : base(dbContext, loggerFactory.CreateLogger<CustomerRepository>())
        {
        }
    }
}
