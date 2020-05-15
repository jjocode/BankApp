using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        [Required]
        public int FromAccount { get; set; }
        [Required]
        public int ToAccount { get; set; }
        public DateTime TransactionDate { get; set; }

        [Range(1,10000)]
        [DataType(DataType.Currency)]
        [Required]
        public decimal TransactionAmount { get; set; }
        public decimal FromBalance { get; set; }
        public decimal ToBalance { get; set; }
    }
}
