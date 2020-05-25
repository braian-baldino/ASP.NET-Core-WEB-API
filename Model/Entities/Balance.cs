using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.Entities
{
    public class Balance
    {
        public int Id { get; set; }
        [ForeignKey ("AnualBalance")]
        public int AnualBalanceId { get; set; }
        public Month Month { get; set; }
        public double? TotalIncomes { get; set; }
        public double? TotalSpendings { get; set; }
        public double? Result { get; set; }
        public bool Positive { get; set; }
        public ICollection<Income> Incomes { get; set; } = new List<Income>();
        public ICollection<Spending> Spendings { get; set; } = new List<Spending>();
    }
}
