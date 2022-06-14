namespace Walkland.Core.Services.Interfaces
{
    public class ChangePasswordRequestDto
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}