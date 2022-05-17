using Buisness_Layer.Interface;
using Common_Layer.Models;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness_Layer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;

        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public UserReg Registration(UserReg userRegModel)
        {
            try
            {
                return this.userRL.Registration(userRegModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public LoginResponse UserLogin(UserLoginModel userLog)
        {
            try
            {
                return this.userRL.UserLogin(userLog);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string ForgotPassword(ForgotPasswordModel forgotPass)
        {
            return this.userRL.ForgotPassword(forgotPass);
        }

        public string ResetPassword(ResetPassword ResetPassword, string emailId)
        {
            try
            {
                return this.userRL.ResetPassword(ResetPassword, emailId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
