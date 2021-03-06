using Common_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness_Layer.Interface
{
    public interface IUserBL
    {
        public UserReg Registration(UserReg userRegModel);
        public LoginResponse UserLogin(UserLoginModel userLog);
        public string ForgotPassword(ForgotPasswordModel forgotPass);
        public string ResetPassword(ResetPassword resetPassword, string emailId);
    }
}
