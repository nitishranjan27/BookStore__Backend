using Common_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface IAdminRL
    {
        public AdminLogin Adminlogin(AdminResponse adminResponse);
    }
}
