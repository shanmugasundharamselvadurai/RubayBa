namespace Walkland.Core.Models
{
    public class Company
    {
        public long Id { get; set; }

        public string BrandName { get; set; }

        public string Date { get; set; }

        public double WalletAmount { get; set; }

        public string LogoStoragePath { get; set; }

        public string LegalName { get; set; }

        public decimal Amount { get; set; }

        public string Reason { get; set; }

        public decimal Rating { get; set; }

        public string MobileNumber { get; set; }

        public string AccountNumber { get; set; }

        public string Website { get; set; }

        public long CompanyCategoryId { get; set; }

        public  CompanyWallet CompanyWallet { get; set; }

    }
}