namespace Walkland.Core.Models
{
    public class CompanyWallet
    {
        public long Id { get; set; }

        public long CompanyId { get; set; }

        public long? CompanyEmployeeId { get; set; }     

        public decimal MaxAllowedAmount { get; set; }
       
        public decimal Amount { get; set; }

        public virtual Company Company { get; set; }

        public string AccountNumber { get; set; }

    }
}