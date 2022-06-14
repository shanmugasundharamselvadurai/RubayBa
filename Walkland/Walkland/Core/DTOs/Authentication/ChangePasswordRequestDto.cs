namespace Walkland.DTOs.Authentication
{
    public class ChangePasswordRequestDto
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}