using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.Entities
{
    public class Income
    {
        public int Id { get; set; }
        [ForeignKey("Balance")]
        public int BalanceId { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public IncomeType Category { get; set; }
    }
}
