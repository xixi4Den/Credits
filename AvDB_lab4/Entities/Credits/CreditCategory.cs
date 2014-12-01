using System;
using System.Collections;
using System.Collections.Generic;
using AvDB_lab4.Entities.Clients;
using AvDB_lab4.Entities.Currencies;

namespace AvDB_lab4.Entities.Credits
{
    public class CreditCategory : BaseDbEntity
    {
        public string Name { get; set; }
        public string DisplayText { get; set; }
        public int Span { get; set; }
        public double Rate { get; set; }
        public RepaymentScheme RepaymentScheme { get; set; }
        public bool IsEarlyRepayment { get; set; }
        public decimal MaxAmount { get; set; }
        public ClientGroup ClientGroup { get; set; }

        public Guid CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }      
    }
}