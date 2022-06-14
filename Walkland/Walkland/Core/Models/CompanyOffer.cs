using System;

namespace Walkland.Core.Models
{
    public class CompanyOffer
    {
        public long Id { get; set; }      

        public string Code { get; set; }

        public string Description { get; set; }

        public int PerUserLimit { get; set; }

        public decimal FixedDiscountAmount { get; set; }

        public int PercentageDiscount { get; set; }

        public decimal MinimumTransactionAmount { get; set; }

        public decimal MaximumDiscountAmount { get; set; }

        public DateTime ValidUpToDateTimeUtc { get; set; }
    }
}