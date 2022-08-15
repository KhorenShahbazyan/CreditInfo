using KhorenTest.CreditInfo.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KhorenTest.CreditInfo.Entities.EntityDbConfigurations
{ 
    public class NationalDbConfiguration : IEntityTypeConfiguration<National>
    {
        public void Configure(EntityTypeBuilder<National> builder)
        {

        }
    }
}
