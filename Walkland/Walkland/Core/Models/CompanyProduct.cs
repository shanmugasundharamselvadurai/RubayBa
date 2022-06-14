namespace Walkland.Core.Models
{
    public class CompanyProduct
    {
        public long Id { get; set; }

        public long CompanyProductId { get; set; }

        public string Name { get; set; }

        public string ProductDescription { get; set; }    

        public string ProductStoragePath { get; set; }

        public string ProductLink { get; set; }

        public Company Company { get; set; }

        private int? _offerPrice;
        public int? OfferPrice
        {
            get
            {
                if (_offerPrice != null)
                    return _offerPrice;

                if (_originalPrice != null)
                    return _originalPrice;

                return null;
            }
            set
            {
                _offerPrice = value;
            }
        }

        public string NewPrice
        {
            get
            {
                return OfferPrice?.ToString();
            }
        }

        private int? _originalPrice;
        public int? OriginalPrice
        {
            get
            {
                if (_offerPrice != null)
                    return _originalPrice;

                return null;
            }
            set
            {
                _originalPrice = value;
            }
        }

        public string OldPrice
        {
            get
            {
                return OriginalPrice?.ToString();
            }
        }
    }
}