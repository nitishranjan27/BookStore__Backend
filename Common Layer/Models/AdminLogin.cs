using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Layer.Models
{
    public class AdminLogin
    {
        public int AdminId { get; set; }
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public string Token { get; set; }
    }
}
