namespace Walkland.Core.Models
{
    public class OfferRequestDto
    {
        public string CompanyWalletNo { get; set; }
        public long PlatformOfferID { get; set; }
        public long ApplicationUserID { get; set; }
        public decimal DiscountAmount { get; set; }
        public long CompanyLocationId { get; set; }
        public decimal PayAmount { get; set; }
    }
}
