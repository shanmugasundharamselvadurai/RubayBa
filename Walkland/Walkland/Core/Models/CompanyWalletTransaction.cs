using System;

namespace Walkland.Core.Models
{
    public class CompanyWalletTransaction
    {
        public long Id { get; set; }

        public string TransactionId { get; set; }

        public long FromWalletId { get; set; }

        public long ToWalletId { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreatedDateTimeUtc { get; set; }

        public virtual CompanyWallet FromWallet { get; set; }

        public virtual CompanyWallet ToWallet { get; set; }

        public virtual CompanyWallet CompanyLocation { get; set; }
    }
}