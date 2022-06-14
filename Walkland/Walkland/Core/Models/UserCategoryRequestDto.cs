using System;
namespace Core.Models
{
    public class UserCategoryRequestDto
    {
        public long ApplicationUserId { get; set; }

        public long CategoryId { get; set; }
    }
}
