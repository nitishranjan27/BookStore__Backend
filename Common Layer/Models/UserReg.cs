using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common_Layer.Models
{
    public class UserReg
    {
        //[Required]
        //[RegularExpression(@"(?=^.{0,40}$)^[a-zA-Z]{3,}\s?[a-zA-Z]*$")]
        public string FullName { get; set; }

        //[Required]
        //[RegularExpression(@"^[a-z]+([._+-][0-9A-Za-z]+)*[@][0-9A-Za-z]+.[a-zA-Z]{2,3}(.[a-zA-Z]{2,3})?$")]
        public string EmailId { get; set; }

        //[Required]
        //[RegularExpression(@"^[A-Z]{1}[A-Z a-z]{3,}[!*@#$%^&+=]?[0-9]{1,}$")]
        public string Password { get; set; }
        //public string confirmPassword { get; set; }

        //[Required]
        //[RegularExpression(@"^[1-9][0-9]{9}$")]
        public string MobileNumber { get; set; }

        
    }
}
