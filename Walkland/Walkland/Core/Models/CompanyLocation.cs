namespace Walkland.Core.Models
{
    public class CompanyLocation
    {
        public long Id { get; set; }

        public long CompanyId { get; set; }

        public string LocationName { get; set; }

        public string ContactPerson { get; set; }

        public string MobileNumber { get; set; }

        public string City { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string Landmark { get; set; }

        public string PinCode { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public Company Company { get; set; }
    }
}