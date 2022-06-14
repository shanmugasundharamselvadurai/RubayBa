using System;
namespace Core.Models
{
    public class UserCompanyRequestDto
    {
        public long ApplicationUserId { get; set; }

        public long CompanyId { get; set; }

        public long CompanyCategoryId { get; set; }
    }
}
