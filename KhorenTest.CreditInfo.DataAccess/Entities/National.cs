using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KhorenTest.CreditInfo.DataAccess.Entities
{
    public class National
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string Name { get; set; }

        public virtual IEnumerable<Customer> Customers { get; set; }

        public National()
        {
            Customers = new List<Customer>();
        }
    }
}
