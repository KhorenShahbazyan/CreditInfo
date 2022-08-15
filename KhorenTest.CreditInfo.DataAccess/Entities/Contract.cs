using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KhorenTest.CreditInfo.DataAccess.Entities
{
    public class Contract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ContractCode { get; set; }

        public DateTime DateOfCreate { get; set; }

        public decimal OrigialAmountValue { get; set; }
        public int OrigialAmountCurrencyId { get; set; }
        public Currency OrigialAmountCurrency { get; set; }

        public decimal InstallmentAmountValue { get; set; }
        public int InstallmentAmountCurrencyId { get; set; }
        public Currency InstallmentAmountCurrency { get; set; }
    }
}
