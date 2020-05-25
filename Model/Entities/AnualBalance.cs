using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entities
{
    public class AnualBalance
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public ICollection<Balance> Balances { get; set; } = new List<Balance>();
        public double? Result { get; set; }
        public bool Positive { get; set; }
    }
}
