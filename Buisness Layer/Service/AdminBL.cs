using Buisness_Layer.Interface;
using Common_Layer.Models;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness_Layer.Service
{
    public class AdminBL : IAdminBL
    {
        private readonly IAdminRL adminRL;

        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }

        public AdminLogin Adminlogin(AdminResponse adminResponse)
        {
            try
            {
                return this.adminRL.Adminlogin(adminResponse);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
