using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }
    }
}
