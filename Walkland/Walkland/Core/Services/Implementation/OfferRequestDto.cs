namespace Walkland.DTOs.Authentication
    public class OfferRequestDto
    {
        public string CompanyWalletNo { get; set; }
        public long PlatformOfferID { get; set; }
        public long ApplicationUserID { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}
