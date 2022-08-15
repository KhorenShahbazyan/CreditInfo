using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KhorenTest.CreditInfo.DataAccess.Entities
{
    public class Currency
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(3)")]
        public string Code { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string Name { get; set; }

        public virtual IEnumerable<Contract> OrigialAmountCurrencies { get; set; }
        public virtual IEnumerable<Contract> InstallmentAmountCurrencies { get; set; }

        public Currency()
        {
            OrigialAmountCurrencies = new List<Contract>();
            InstallmentAmountCurrencies = new List<Contract>();
        }
    }
}
