using System;

namespace M4WS_sample.Models
{
    public class LoginInfo
    {
        public string User { get; set; }
        public string Password { get; set; }
        public string Company { get; set; }
    }

    public class LogoutInfo
    {
        public string AuthenticationToken { get; set; }
    }
}