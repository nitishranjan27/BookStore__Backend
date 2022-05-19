using Common_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness_Layer.Interface
{
    public interface IAdminBL
    {
        public AdminLogin Adminlogin(AdminResponse adminResponse);
    }
}
