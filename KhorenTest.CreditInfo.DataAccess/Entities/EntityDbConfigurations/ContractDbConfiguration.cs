using KhorenTest.CreditInfo.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KhorenTest.CreditInfo.Entities.EntityDbConfigurations
{ 
    public class ContractDbConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.HasOne(o => o.InstallmentAmountCurrency)
                   .WithMany(m => m.InstallmentAmountCurrencies)
                   .HasForeignKey(fk => fk.InstallmentAmountCurrencyId)
                   .HasConstraintName("FK_Contract_InstallmentCurrency")
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.OrigialAmountCurrency)
                   .WithMany(m => m.OrigialAmountCurrencies)
                   .HasForeignKey(fk => fk.OrigialAmountCurrencyId)
                   .HasConstraintName("FK_Contract_OrigialCurrency")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
