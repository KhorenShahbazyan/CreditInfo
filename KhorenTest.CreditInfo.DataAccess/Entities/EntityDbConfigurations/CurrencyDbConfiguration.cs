using KhorenTest.CreditInfo.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KhorenTest.CreditInfo.Entities.EntityDbConfigurations
{ 
    public class CurrencyDbConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {

        }
    }
}
