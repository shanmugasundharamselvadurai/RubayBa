namespace Walkland.Core.Models
{
    public class CompanyRating
    {
        public long Id { get; set; }

        public long CompanyId { get; set; }

        public long ApplicationUserId { get; set; }

        public decimal Rating { get; set; }

        public Company Company { get; set; }
    }
}