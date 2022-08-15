using KhorenTest.CreditInfo.Entities.EntityDbConfigurations;
using Microsoft.EntityFrameworkCore;

namespace KhorenTest.CreditInfo.DataAccess.DbContexts
{
    public class KhorenTestDbContext : DbContext
    {
        public KhorenTestDbContext(DbContextOptions<KhorenTestDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new NationalDbConfiguration());
            modelBuilder.ApplyConfiguration(new CurrencyDbConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerDbConfiguration());
            modelBuilder.ApplyConfiguration(new ContractDbConfiguration());


        }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public string FullName()
        {
            return nameof(KhorenTestDbContext);
        }
    }
}
