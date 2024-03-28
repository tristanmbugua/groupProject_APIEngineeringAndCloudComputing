using System;

namespace bankIt.Models
{
    public class MonthlyCharge
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime ChargeDate { get; set; }
     
    }
}
