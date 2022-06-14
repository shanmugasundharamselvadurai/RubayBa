namespace Walkland
{
    public class LoginRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Code { get; set; }
        public string DeviceID { get; set; }
        public string UserIP { get; set; }
    }
}