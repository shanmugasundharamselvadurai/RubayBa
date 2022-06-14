using System;

namespace Walkland.Core.Models
{
    public class PlatformOffer
    {
        public long Id { get; set; }

        public long CompanyId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public int PerUserLimit { get; set; }

        public string Type { get; set; }

        public decimal FixedDiscountAmount { get; set; }

        public decimal PercentageDiscount { get; set; }

        public decimal MinimumTransactionAmount { get; set; }

        public decimal MaximumDiscountAmount { get; set; }

        public DateTime ValidUpToDateTimeUtc { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }
    }
}
