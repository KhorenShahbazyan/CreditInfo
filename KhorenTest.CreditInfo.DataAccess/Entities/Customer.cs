using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KhorenTest.CreditInfo.DataAccess.Entities
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CustomerCode { get; set; }

        public int NationalId { get; set; }
        public National National { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
