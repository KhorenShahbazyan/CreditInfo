using KhorenTest.CreditInfo.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KhorenTest.CreditInfo.Entities.EntityDbConfigurations
{ 
    public class CustomerDbConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasOne(o => o.National)
                   .WithMany(m => m.Customers)
                   .HasForeignKey(fk => fk.NationalId)
                   .HasConstraintName("FK_Customer_National")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
