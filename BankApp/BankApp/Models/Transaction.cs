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
        public int FromAccountId { get; set; }

        [Display(Name = "From Account")]
        public string FromName { get; set; }
        [Required]
        public int ToAccountId { get; set; }

        [Display(Name = "To Account")]
        public string ToName { get; set; }

        [Display(Name = "Transaction Date/Time")]
        public DateTime TransactionDate { get; set; }

        [Display(Name = "Transaction Amount")]
        [Range(1,10000)]
        [DataType(DataType.Currency)]
        [Required]
        public decimal TransactionAmount { get; set; }

        [Display(Name = "From Account Balance")]
        [DataType(DataType.Currency)]
        public decimal FromBalance { get; set; }

        [Display(Name = "To Account Balance")]
        [DataType(DataType.Currency)]
        public decimal ToBalance { get; set; }
    }
}
