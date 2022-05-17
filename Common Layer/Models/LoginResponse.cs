using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Layer.Models
{
    public class LoginResponse
    {
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public long MobileNumber { get; set; }
        public string Token { get; set; }
    }
}
